using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BitCoupon.DAL.Models;
using PayPal.Api;
using System.Net.Mail;
using BitCoupon.DAL.LogicCollection;

namespace BitCoupon.MVC.Controllers
{
    public class PayPalRefundsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Gets refunds which needs approval
        /// </summary>
        /// <returns>list of refunds</returns>
        public ActionResult GetRefunds()
        {
            var refunds = db.Refunds.Include(p => p.ApplicationUser).Where(x => x.Finished == "Not Approved");
            return View(refunds.ToList());
        }

        /// <summary>
        /// Gets all approved refunds
        /// </summary>
        /// <returns>list of refunds</returns>
        public ActionResult GetApprovedRefunds()
        {
            var refunds = db.Refunds.Include(p => p.ApplicationUser).Where(x => x.Finished == "Approved");
            return View(refunds.ToList());
        }

        /// <summary>
        /// Gets all declined refunds
        /// </summary>
        /// <returns>list of refunds</returns>
        public ActionResult GetDeclinedRefunds()
        {
            var refunds = db.Refunds.Include(p => p.ApplicationUser).Where(x => x.Finished == "Declined");
            return View(refunds.ToList());
        }

        /// <summary>
        /// If admin approves refund, send request to paypal for 
        /// refund and mark that refund as Approved
        /// </summary>
        /// <param name="id">id of refund</param>
        /// <returns></returns>
        public ActionResult Approve(int id)
        {
            var apiContext = GetApiContext();
            if (apiContext == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);

            var refundPayment = db.Refunds.Find(id);

            if (refundPayment == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var cart = db.Carts.Where(x => x.PaymentId == refundPayment.PaymentId).SingleOrDefault();
            
            try
            {
                var payment = Payment.Get(apiContext, refundPayment.PaymentId);  //find payment with selected paymenId

                var sale = payment.transactions[0].related_resources[0].sale;

                var refund = new Refund()  //create new refund to be sent to paypal
                {
                    sale_id = refundPayment.PaymentId,
                    amount = new Amount()
                    {
                        currency = "USD",
                        total = cart.TotalPrice.ToString()
                    }
                };
                var response = sale.Refund(apiContext, refund);  //send request for refund

                refundPayment.Finished = "Approved";
                cart.Purchased = "Refunded";
               
                MailBuilder(cart.ApplicationUserId, refundPayment.Finished,cart.PaymentId);
                
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

        }

        /// <summary>
        /// Sets refund as Declined
        /// </summary>
        /// <param name="id">id of refund</param>
        /// <returns></returns>
        public ActionResult Decline(int id)
        {
            var refundPayment = db.Refunds.Find(id);

            if (refundPayment == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            refundPayment.Finished = "Declined";

            MailBuilder(refundPayment.ApplicationUserId, refundPayment.Finished, "");
            db.SaveChanges();
            //send mail

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Help method for sending user email about refund status
        /// </summary>
        /// <param name="userId">id of user to send email</param>
        /// <param name="order"></param>
        /// <param name="paymentId">id of payement</param>
        private void MailBuilder(string userId, string order,string paymentId)
        {
            var user = db.Users.Find(userId);
            var mail = db.Mails.Where(x => x.Subject == "Admin Refund Response").Single();

            var toAddress = new MailAddress(user.Email, user.FirstName);  //user.Email //user.FirstName
            string subject = mail.Subject;
            var body = mail.Body;

            body = body.Replace("%name%", user.FirstName); // replace tag with name 

            body = body.Replace("%order%", order );//replace coupon tag with coupon iformation

            if (order == "Approved")
            {
                body += "<br/> <br/> " + "Your Payment ID is: " + paymentId;
            }

            MailService.SendMail(subject, body, toAddress);
            
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
