using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BitCoupon.DAL.Models;
using BitCoupon.DAL.LogicCollection;

namespace BitCoupon.MVC.Controllers
{
    
    public class CouponsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        /// <summary>
        /// Reactivate coupons by admin
        /// </summary>
        /// <returns>list of coupons which are inactive</returns>
        [Authorize(Roles = "Admin,SuperAdmin")]
        public ActionResult ReactivateCoupons()
        {
            List<Coupon> coupons = db.Coupons.Where(x => x.Acitve == false).OrderByDescending(x => x.CouponId).ToList();
            return View(coupons);
        }

        /// <summary>
        /// Post method for reactivating coupons
        /// </summary>
        /// <param name="required">required no. of coupons</param>
        /// <param name="total">total no. of coupons</param>
        /// <param name="date">expiraton date</param>
        /// <param name="id">id of coupon</param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public ActionResult ReactivateCoupons(string required, string total, string date, int id)
        {
            Coupon coupon = db.Coupons.Find(id);
            int req = 0, tot = 0;
            DateTime dat = new DateTime();
            Int32.TryParse(required, out req);
            Int32.TryParse(total, out tot);
            DateTime.TryParse(date, out dat);

            if (tot % req != 0 || tot < req)
            {
                ModelState.AddModelError("TotalNumberOfCoupons", "Total number of coupons doesn't match required coupon count.");
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            else if (dat < DateTime.Now || dat > DateTime.Now.AddMonths(12))
            {
                ModelState.AddModelError("ExpirationTime", "Date is not valid.");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            coupon.RequiredNumberOfCoupons = req;
            coupon.TotalNumberOfCoupons = tot;
            coupon.ExpirationTime = dat;
            coupon.Acitve = true;
            coupon.Approved = true;

            db.Entry(coupon).State = EntityState.Modified;
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);


        }

        /// <summary>
        /// Get metod for displaying deleted coupons
        /// </summary>
        /// <returns>list of deleted coupons</returns>
        [Authorize(Roles ="Admin,SuperAdmin")]
        public ActionResult Deleted()
        {
            List<Coupon> coupons = db.Coupons.Where(x => x.IsDeleted == true).ToList();
            return View(coupons);
        }

        /// <summary>
        /// Post method to reactivte deleted coupons
        /// </summary>
        /// <param name="required">required no. of coupons</param>
        /// <param name="total">total no. of coupons</param>
        /// <param name="date">expiraton date</param>
        /// <param name="id">id of coupon</param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public ActionResult Deleted(string required, string total, string date, int id)
        {
            Coupon coupon = db.Coupons.Find(id);
            int req = 0, tot = 0;
            DateTime dat = new DateTime();
            Int32.TryParse(required, out req);
            Int32.TryParse(total, out tot);
            DateTime.TryParse(date, out dat);

            if (tot % req != 0 || tot < req)
            {
                ModelState.AddModelError("TotalNumberOfCoupons", "Total number of coupons doesn't match required coupon count.");
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            else if (dat < DateTime.Now || dat > DateTime.Now.AddMonths(12))
            {
                ModelState.AddModelError("ExpirationTime", "Date is not valid.");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            coupon.RequiredNumberOfCoupons = req;
            coupon.TotalNumberOfCoupons = tot;
            coupon.ExpirationTime = dat;
            coupon.IsDeleted = false;
            coupon.Approved = true;
            coupon.Acitve = true;

            db.Entry(coupon).State = EntityState.Modified;
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Get metod for displaying coupons which are not approved
        /// </summary>
        /// <returns>list of unapproved coupons</returns>
        [Authorize(Roles = "Admin,SuperAdmin")]
        public ActionResult Approved()
        {
            List<Coupon> coupons = db.Coupons.Where(x => x.Approved == false).ToList();
            return View(coupons);
        }


        /// <summary>
        /// Post method to approve coupons
        /// </summary>
        /// <param name="required">required no. of coupons</param>
        /// <param name="total">total no. of coupons</param>
        /// <param name="date">expiraton date</param>
        /// <param name="id">id of coupon</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles ="Admin,SuperAdmin")]
        public ActionResult Approved(string required, string total, string date, int id) {
            Coupon coupon = db.Coupons.Find(id);
            int req = 0, tot = 0;
            DateTime dat = new DateTime();
            Int32.TryParse(required, out req);
            Int32.TryParse(total, out tot);
            DateTime.TryParse(date, out dat);

            if (tot % req != 0 || tot < req)
            {
                ModelState.AddModelError("TotalNumberOfCoupons", "Total number of coupons doesn't match required coupon count.");
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            else if (dat < DateTime.Now || dat > DateTime.Now.AddMonths(12))
            {
                ModelState.AddModelError("ExpirationTime", "Date is not valid.");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            coupon.RequiredNumberOfCoupons = req;
            coupon.TotalNumberOfCoupons = tot;
            coupon.ExpirationTime = dat;
            coupon.Approved = true;
            coupon.Acitve = true;
            this.ValidateModel(coupon);
            if (ModelState.IsValid)
            {
                db.Entry(coupon).State = EntityState.Modified;
                db.SaveChanges();

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }


        /// <summary>
        /// Get metod for displaying edited coupons
        /// </summary>
        /// <returns>list of edited coupons</returns>
        [Authorize(Roles = "Admin,SuperAdmin")]
        public ActionResult Edited()
        {
            List<Coupon> coupons = db.Coupons.Where(x => x.IsEdited == true).ToList();
            return View(coupons);
        }

        /// <summary>
        /// Post method to reactivte edited coupons
        /// </summary>
        /// <param name="required">required no. of coupons</param>
        /// <param name="total">total no. of coupons</param>
        /// <param name="date">expiraton date</param>
        /// <param name="id">id of coupon</param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public ActionResult Edited(string required, string total, string date, int id)
        {
            Coupon coupon = db.Coupons.Find(id);
            int req = 0, tot = 0;
            DateTime dat = new DateTime();
            Int32.TryParse(required, out req);
            Int32.TryParse(total, out tot);
            DateTime.TryParse(date, out dat);

            if (tot % req != 0 || tot < req)
            {
                ModelState.AddModelError("TotalNumberOfCoupons", "Total number of coupons doesn't match required coupon count.");
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            else if (dat < DateTime.Now || dat > DateTime.Now.AddMonths(12))
            {
                ModelState.AddModelError("ExpirationTime", "Date is not valid.");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            coupon.RequiredNumberOfCoupons = req;
            coupon.TotalNumberOfCoupons = tot;
            coupon.ExpirationTime = dat;
            coupon.IsEdited = false;
            coupon.Approved = true;
            coupon.Acitve = true;

            db.Entry(coupon).State = EntityState.Modified;
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public ActionResult ActiveCoupons()
        {
            List<Coupon> coupons = db.Coupons
                .Where(x => x.Acitve == true && x.IsDeleted == false && x.IsEdited == false)
                .ToList();

            return View(coupons);
        }


        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        //[ValidateAntiForgeryToken]
        public ActionResult ActiveCoupons(int id)
        {
            Coupon coupon = db.Coupons.Find(id);

            coupon.Acitve = false;
            
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Get action for buy coupon by salesman (it will return a view 
        /// with coupons)
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Salesman")]
        public ActionResult Buy(string userId)
        {
            ViewBag.UserId = userId;
            return View(db.Coupons.Include(c => c.Category).Where(x => x.Category.Approved == true && x.Acitve == true && x.IsDeleted == false && x.IsEdited == false).ToList());
        }

        /// <summary>
        /// Post action for buy coupon by salesman
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Salesman")]
        [HttpPost]
        public ActionResult Buy(string couponId, string quantity, string userId)
        {
            var coupon = db.Coupons.Find(Int32.Parse(couponId));

            coupon.TotalNumberOfCoupons -= coupon.RequiredNumberOfCoupons * Int32.Parse(quantity);  //reduce amount of coupons bought
            if (coupon.TotalNumberOfCoupons < 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            coupon.Purchase++;  //increse purchase coupon

            if (coupon.NuberOfCouponsToOfferManaged > 0)
                coupon.NuberOfCouponsToOfferManaged -= coupon.RequiredNumberOfCoupons;

            Cart cart = new Cart() { ApplicationUserId = userId, Purchased = "Payed", PaymentId = "", TimeOfPurchase = DateTime.Now, TotalPrice = coupon.Price };

            db.Entry(coupon).State = EntityState.Modified;
            db.Carts.Add(cart);
            db.SaveChanges();

            var items = db.Items.ToList();

            Item item = new Item() { CartId = cart.Id, CouponId = coupon.CouponId, Quantity = Int32.Parse(quantity), VerificationId = LogicCoupons.GenerateVerificationId(new List<Item>(items)), TimeOfPurchase = DateTime.Now};
            db.Items.Add(item);
            db.SaveChanges();
            if (coupon.TotalNumberOfCoupons > 0)  //if there is still coupons to buy return OK
                return new HttpStatusCodeResult(HttpStatusCode.OK);

            coupon.Acitve = false;  //else deactivate coupon
            db.Entry(coupon).State = EntityState.Modified;
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Get method for priority, displays all coupons in view
        /// </summary>
        /// <returns>list of coupons</returns>
        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public ActionResult Priority()
        {
            return View(db.Coupons.ToList());
        }


        /// <summary>
        /// Post method for priority, admin can change priority of coupon
        /// </summary>
        /// <param name="couponId">id of coupon</param>
        /// <param name="priority">new priority</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public ActionResult Priority(int couponId, int priority)
        {
            if (priority != 1 && priority != 2 && priority != 3)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var coupon = db.Coupons.Find(couponId);
            if (coupon != null)
            {
                coupon.Priority = priority;
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
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
