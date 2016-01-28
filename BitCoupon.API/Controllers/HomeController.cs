using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BitCoupon.DAL.Models;
using System.Data.Entity;

namespace BitCoupon.API.Controllers
{
	public class HomeController : Controller
	{
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Check if user is first signin and if it is
        /// offer him to change password
        /// </summary>
        /// <param name="username">user username</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult FirstSignin(string username)
        {
            ApplicationUser user = db.Users.Where(x => x.UserName == username).SingleOrDefault();
            if (user == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (user.FirstSignin == false && (this.User.IsInRole("Seller") || (this.User.IsInRole("Buyer") && user.IsCreatedBySalesman == true)))
            {
                user.FirstSignin = true;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }

       
    }
}
