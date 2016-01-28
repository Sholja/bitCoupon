using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PayPal.Api;
using BitCoupon.DAL.Models;
using System.Web;
using BitCoupon.API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Net.Mail;
using BitCoupon.DAL.LogicCollection;

namespace BitCoupon.API.Controllers
{

    public class PayPalController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Create payment for paypal
        /// </summary>
        /// <param name="data">json object which contains informations
        /// about selected coupons from shopping cart</param>
        /// <returns>url to paypal</returns>
        [HttpPost]
        [Authorize(Roles = "Buyer")]
        public IHttpActionResult CreatePayment(object data)
        {
            string userId = this.User.Identity.GetUserId();
            RemoveCart(userId);

            var apiContext = GetApiContext();
            if (apiContext == null)
                return StatusCode(HttpStatusCode.NotAcceptable);

            Amount amount = new Amount();
            amount.currency = "USD";
            Transaction transaction = new Transaction();
            transaction.item_list = new ItemList();
            transaction.item_list.items = new List<PayPal.Api.Item>();

            string json = JsonConvert.SerializeObject(data);
            double total = 0;

            RootObject root = JsonConvert.DeserializeObject<RootObject>(json);
            List<BitCoupon.API.Models.Item> items = root.data.items;

            if (items.Count == 0)
                return NotFound();

            for (int i = 0; i < items.Count; i++)
            {
                var coupon = db.Coupons.Find(Int32.Parse(items[i].id));
                //if there are no coupons to buy (qunatity of purchase is bigger then total number of coupons), return BadRequest (don't allow purchase)
                if (items[i].quantity * coupon.RequiredNumberOfCoupons > coupon.TotalNumberOfCoupons)
                    return BadRequest();

                total += items[i].total;

                transaction.item_list.items.Add(new PayPal.Api.Item()  //add items to item list
                {
                    name = items[i].name,
                    description = items[i].data.DescriptionOnCoupon,
                    quantity = items[i].quantity.ToString(),
                    price = items[i].price.ToString(),
                    currency = amount.currency
                });
            }
            amount.total = total.ToString();  //total amount to pay

            transaction.amount = amount;

            Payer payer = new Payer();
            payer.payment_method = "paypal";

            Payment payment = new Payment();
            payment.intent = "sale";
            payment.payer = payer;
            payment.transactions = new List<Transaction>();
            payment.transactions.Add(transaction);

            payment.redirect_urls = new RedirectUrls();  //create redirect urls where paypal will return depending did user finished payment

            payment.redirect_urls.return_url = String.Format("http://{0}{1}", HttpContext.Current.Request.Url.Authority, "/api/PayPal?userId=" + this.User.Identity.GetUserId());

            payment.redirect_urls.cancel_url = String.Format("http://{0}{1}", HttpContext.Current.Request.Url.Authority, "/api/PayPal");

            Payment createdPayment = null;

            try
            {
                createdPayment = payment.Create(apiContext);
            }
            catch
            {
                return StatusCode(HttpStatusCode.NotAcceptable);
            }

            CreateCart(userId, items, double.Parse(amount.total));

            return Json(createdPayment.GetApprovalUrl());
        }

        /// <summary>
        /// If user finished everything with paypal, paypal
        /// redirects user to this action where he will complete transaction
        /// </summary>
        /// <param name="userId">id of user which bought coupons</param>
        /// <param name="paymentId">id of payment</param>
        /// <param name="token">token for accessing paypal</param>
        /// <param name="PayerID">id of payer</param>
        /// <returns></returns>
        [HttpGet]
        // GET: api/PayPal
        public IHttpActionResult PaymentApproved(string userId, string paymentId, string token, string PayerID)
        {
            var apiContext = GetApiContext();
            if (apiContext == null)
                return StatusCode(HttpStatusCode.NotAcceptable);

            Payment exePayment = null;
            try
            {
                Payment payment = Payment.Get(apiContext, paymentId);
                PaymentExecution exe = new PaymentExecution();
                exe.payer_id = PayerID;
                exePayment = payment.Execute(apiContext, exe);  //execute payment
            }
            catch
            {
                return StatusCode(HttpStatusCode.NotAcceptable);
            }

            if (exePayment.state == "approved")  //if payment is approved set cart purchased to true
            {                                    //and all items set to purchased true 
                var cart = EditCart(userId, paymentId);

                Mail mail = db.Mails.Where(x => x.Subject == "Payment").First();

                MailBulider(userId, cart.Id, mail, exePayment.state, paymentId);
                return Redirect(String.Format("http://{0}{1}", HttpContext.Current.Request.Url.Authority, "/#/profile"));
            }

            return Redirect(String.Format("http://{0}", HttpContext.Current.Request.Url.Authority));
        }

        /// <summary>
        /// If user canceled payment return him to this action
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        // GET: api/PayPal
        public IHttpActionResult PaymentCanceled()
        {
            return Redirect(String.Format("http://{0}", HttpContext.Current.Request.Url.Authority));
        }

        /// <summary>
        /// Runds payment with paypal
        /// </summary>
        /// <param name="paymentId">id of payment with paypal</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/paypal/refundpayment")]
        [Authorize(Roles = "Buyer")]
        public IHttpActionResult RefundPayment(string cartId)
        {
            var cart = db.Carts.Find(Int32.Parse(cartId));

            if (cart == null)
                return NotFound();            
            if (cart.TimeOfPurchase.AddDays(7) > DateTime.Now)  //if coupons have been purchased in last 7 
            {                                                   //days then make refund

                if (cart.PaymentId == string.Empty)
                    MakeRefund(cart.Id);

                var apiContext = GetApiContext();

                if (apiContext == null)
                    return StatusCode(HttpStatusCode.NotAcceptable);

                try
                {
                    var payment = Payment.Get(apiContext, cart.PaymentId);  //find payment with selected paymenId

                    var sale = payment.transactions[0].related_resources[0].sale;


                    var refund = new Refund()  //create new refund to be sent to paypal
                    {
                        sale_id = cart.PaymentId,
                        amount = new Amount()
                        {
                            currency = "USD",
                            total = cart.TotalPrice.ToString()
                        }
                    };
                    var response = sale.Refund(apiContext, refund);  //send request for refund

                    var paypalRefund = new PayPalRefund()  //new object refund in our database to be saved
                    {
                        ApplicationUserId = cart.ApplicationUserId,
                        Option = "Automatic Refund Done in 7 Days",
                        Content = "",
                        PaymentId = cart.PaymentId,
                        Finished = "Approved",
                        CartId = cart.Id
                    };

                    cart.Purchased = "Refunded";  //set cart state to Refunded


                    db.Refunds.Add(paypalRefund);
                    db.SaveChanges();
                    Mail mail = db.Mails.Where(x => x.Subject == "Refound").First();
                    MailBulider(cart.ApplicationUserId, cart.Id, mail, paypalRefund.Finished, cart.PaymentId);
                    MakeRefund(cart.Id);
                    return Ok();
                }
                catch
                {
                    return NotFound();
                }
            }
            return BadRequest(cart.Id.ToString() + "," + cart.PaymentId); //if it has passed more then 7 days, user has to 
        }                                   //send request to admin to approve him/her refund

        /// <summary>
        /// Creates refund form from user to be sent
        /// to admins for review
        /// </summary>
        /// <param name="refund">object refund</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/paypal/createrefund")]
        [Authorize(Roles = "Buyer")]
        public IHttpActionResult CreateRefund(PayPalRefund refund)
        {
            if (refund == null)
                return NotFound();

            refund.ApplicationUserId = this.User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                refund.Finished = "Not Approved";
                db.Refunds.Add(refund);
                db.SaveChanges();
                MailBuliderCreateRefund(refund.ApplicationUserId);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Help method for sending mail after refund is done
        /// </summary>
        /// <param name="userId">id of user</param>
        private void MailBuliderCreateRefund(string userId)
        {
            var user = db.Users.Find(userId);
            var toAddres = new MailAddress(user.Email, user.FirstName);
            var mail = db.Mails.Where(x => x.Subject == "Refund Notification").Single();
            var body = mail.Body;

            body = body.Replace("%name%", user.FirstName);

            MailService.SendMail(mail.Subject, body, toAddres);

        }

        /// <summary>
        /// Help method for sending mail after payment is approved
        /// </summary>
        /// <param name="userId">id of user</param>
        /// <param name="cartId">id of cart</param>
        /// <param name="mail">object mail</param>
        /// <param name="subjectStatus">subject status</param>
        /// <param name="paymentId">payment id</param>
        private void MailBulider(string userId, int cartId, Mail mail, string subjectStatus, string paymentId)
        {
            var cart = db.Carts.Include(c => c.Items).Where(x => x.Id == cartId).SingleOrDefault();
            var user = db.Users.Find(userId);

            var toAddress = new MailAddress(user.Email, user.FirstName);  //user.Email //user.FirstName
            string subject = mail.Subject + " " + subjectStatus;
            var body = mail.Body;

            body = body.Replace("%name%", user.FirstName); // replace tag with name 


            string temp = "";
            int counter = 1;
            foreach (var item in cart.Items)
            {
                temp += counter.ToString() + ". " + item.Coupon.Name + " <br /> <br />"
                    + "Price: " + (item.Coupon.NewPrice * item.Quantity).ToString() + " USD" + " <br />"
                    + "Verification ID is: " + item.VerificationId + " <br /><br />";
                counter++;
            }

            body = body.Replace("%coupon%", temp);//replace coupon tag with coupon iformation
            body = body.Replace("%totalPrice%", cart.TotalPrice.ToString());
            body = body.Replace("%paymentId%", paymentId);

            MailService.SendMail(subject, body, toAddress);
        }

        /// <summary>
        /// Help method which decrese total number of coupons and
        /// deactivates coupon if there are no more coupons to buy
        /// </summary>
        /// <param name="id">id of coupon</param>
        /// <param name="quantity">quntity purchased</param>
        /// <returns></returns>
        private void BuyCoupon(int id, int quantity)
        {
            var coupon = db.Coupons.Find(id);

            coupon.TotalNumberOfCoupons -= coupon.RequiredNumberOfCoupons * quantity;  //reduce amount of coupons bought
            coupon.Purchase++;  //increse purchase coupon

            if (coupon.NuberOfCouponsToOfferManaged > 0)
                coupon.NuberOfCouponsToOfferManaged -= coupon.RequiredNumberOfCoupons;

            db.Entry(coupon).State = EntityState.Modified;
            db.SaveChanges();
            if (coupon.TotalNumberOfCoupons > 0)  //if there is still coupons to buy return OK
                return;

            coupon.Acitve = false;  //else deactivate coupon
            db.Entry(coupon).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// Help method which createxs apicontext 
        /// from token which paypal returned us
        /// </summary>
        /// <returns>created api context</returns>
        private APIContext GetApiContext()
        {
            try
            {
                var config = ConfigManager.Instance.GetProperties();
                var accessToken = new OAuthTokenCredential(config).GetAccessToken();
                var apiContext = new APIContext(accessToken);
                apiContext.Config = config;
                return apiContext;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Help method, when payment is started, if there is
        /// a shopping cart connected to this user and purchased is false
        /// remove cart and all items related with that cart
        /// </summary>
        /// <param name="userId">id of user</param>
        private void RemoveCart(string userId)
        {
            var oldCart = db.Carts.Where(x => x.ApplicationUserId == userId && x.Purchased == "Not Payed").SingleOrDefault();
            if (oldCart != null)  //if there is cart with purcahsed = false, related to this user 
            {                       //then delte it along with all items in the cart
                var oldItems = db.Items.Where(x => x.CartId == oldCart.Id).ToList();
                db.Carts.Remove(oldCart);
                for (int i = 0; i < oldItems.Count; i++)
                {
                    db.Items.Remove(oldItems[i]);
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Creates new shopping cart and all items related to that cart
        /// </summary>
        private void CreateCart(string userId, List<BitCoupon.API.Models.Item> items, double total)
        {
            Cart cart = new Cart() { ApplicationUserId = userId, Purchased = "Not Payed", PaymentId = "", TimeOfPurchase = DateTime.Now, TotalPrice = total };
            db.Carts.Add(cart);
            db.SaveChanges();


            var Dbitems = db.Items.ToList();
            // generates verification id 

            for (int i = 0; i < items.Count; i++)
            {
                BitCoupon.DAL.Models.Item item = new BitCoupon.DAL.Models.Item() { CartId = cart.Id, CouponId = Int32.Parse(items[i].id), Quantity = items[i].quantity, VerificationId = LogicCoupons.GenerateVerificationId(Dbitems), TimeOfPurchase = DateTime.Now };
                db.Items.Add(item);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// If payment was approved, set cart and all items related
        /// to the cart to purchased = true
        /// </summary>
        /// <param name="userId">user id</param>
        private Cart EditCart(string userId, string paymentId)
        {
            var cart = db.Carts.Where(x => x.ApplicationUserId == userId && x.Purchased == "Not Payed").SingleOrDefault();
            cart.Purchased = "Payed";
            cart.PaymentId = paymentId;
            cart.TimeOfPurchase = DateTime.Now;
            var items = db.Items.Where(x => x.CartId == cart.Id).ToList();

            for (int i = 0; i < items.Count; i++)
            {
                BuyCoupon(items[i].CouponId, items[i].Quantity);
                db.SaveChanges();
            }
            return cart;
        }

        /// <summary>
        /// When refund is done, return total number of coupons
        /// </summary>
        /// <param name="cartId"></param>
        private void MakeRefund(int cartId)
        {
            var cart = db.Carts.Find(cartId);
            Coupon coupon = null;
            var items = cart.Items;
            for (int i = 0; i < items.Count; i++)
            {
                coupon = db.Coupons.Find(items[i].CouponId);
                coupon.TotalNumberOfCoupons += coupon.RequiredNumberOfCoupons * items[i].Quantity;
            }
            db.SaveChanges();
        }
    }
}
