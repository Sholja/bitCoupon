using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BitCoupon.DAL.Models;
using Microsoft.AspNet.Identity;

namespace BitCoupon.API.Controllers
{
    public class ReportCommentsController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();


        /// <summary>
        /// Reports coupon (increments his report status)
        /// </summary>
        /// <param name="id">id of coupon to report</param>
        /// <returns>status code OK if success or status
        /// code BadRequest if there is error</returns>
        [Authorize(Roles = "Buyer")]
        [HttpGet]
        public IHttpActionResult AddReport(int id)
        {
            Comment comment = db.Comments.Find(id);
            string userId = this.User.Identity.GetUserId();

            if (comment.ApplicationUserId == userId)
                return Unauthorized();

            int buyers = db.Buys.Where(x => x.CouponId == comment.CouponId && x.ApplicationUserId == userId).Count();
            if (buyers == 0)  //if user didn't bought this coupon, don't allow him to report it
                return NotFound();

            Report report = db.Reports.Where(x => x.CommentId == id && x.ApplicationUserId == userId).SingleOrDefault();  //find report of selected comment

            if (report != null)  //if there is report then this user has already reported comment
                return BadRequest();
            else
            {
                Report newReport = new Report() { CommentId = id, ApplicationUserId = userId };  //if there is not report about selected coupon, create new one
                db.Reports.Add(newReport);
            }
            db.SaveChanges();

            int numberOfPurchases = db.Coupons.Find(comment.CouponId).Purchase;
            int counter = db.Reports.Where(x => x.CommentId == id).Count();

            //if there are more then 20% reports on selected comment, send message to admin
            if (((double)counter / (double)numberOfPurchases) > 0.2)
            {
                Message message = new Message() { Sender = "System", Content = "Comment with content: " + comment.Content + " has been reported more than 20%.", IsRead = false, Title = "Report about Comment", Time = DateTime.Now, CommentId = comment.CommentId };
                db.Messages.Add(message);
                db.SaveChanges();
            }

            return Ok();
        }
    }
}
