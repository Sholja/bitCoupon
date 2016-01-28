using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BitCoupon.DAL.Models;

namespace BitCoupon.MVC.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class FeedBacksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Gets all feedbacks from database
        /// </summary>
        /// <returns>list with feedbacks</returns>
        public ActionResult Index()
        {
            return View(db.FeedBacks.ToList());
        }

        /// <summary>
        /// Gets details about selected feedback
        /// </summary>
        /// <param name="id">id of feedback</param>
        /// <returns>selected feedback</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FeedBack feedback = db.FeedBacks.Find(id);

            if (feedback == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
                return View(feedback);
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
