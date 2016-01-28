using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BitCoupon.DAL.Models;

namespace BitCoupon.API.Controllers
{
    public class FeedBacksAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Action for create new feedback
        /// </summary>
        /// <param name="feedBack">object feedback</param>
        /// <returns></returns>
        [ResponseType(typeof(FeedBack))]
        public IHttpActionResult PostFeedBack(FeedBack feedBack)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FeedBacks.Add(feedBack);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = feedBack.Id }, feedBack);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FeedBackExists(int id)
        {
            return db.FeedBacks.Count(e => e.Id == id) > 0;
        }
    }
}