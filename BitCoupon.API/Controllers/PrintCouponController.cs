using BitCoupon.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitCoupon.API.Controllers
{
    public class PrintCouponController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: PrintCoupon
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Prints selected coupon in new view
        /// </summary>
        /// <param name="uniqueId">unique id of coupon</param>
        /// <returns></returns>
        public ActionResult Print(string uniqueId)
       {
            var item = db.Items.Where(x => x.VerificationId == uniqueId).SingleOrDefault();

            return View(item);
        }
    }
}