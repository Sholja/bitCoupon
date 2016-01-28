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
using Microsoft.AspNet.Identity;
using BitCoupon.API.Models;

namespace BitCoupon.API.Controllers
{

    public class CommentsAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // POST: api/CommentsAPI
        /// <summary>
        /// Creates comment by user, only user which bought
        /// coupon can comment it, also admin and super
        /// admin can post comments
        /// </summary>
        /// <param name="comment">comment to create</param>
        /// <returns>created comment</returns>
        [ResponseType(typeof(Comment))]
        [Authorize(Roles = "Buyer,Admin,SuperAdmin")]
        public IHttpActionResult PostComment(Comment comment)
        {
            
            int id = comment.CouponId;

            string userId = this.User.Identity.GetUserId();
            if (this.User.IsInRole("Buyer"))
            {
                
                int items = db.Items.Where(x => x.CouponId == id && x.Cart.ApplicationUserId == userId).Count();
                if (items == 0)  //if user didn't bought this coupon, don't allow comment coupon
                    return BadRequest();
            }
            var comm = db.Comments.Where(x => x.CouponId == id && x.ApplicationUserId == userId && x.CouponRate > 0).FirstOrDefault();

            if (comm != null)
                comment.CouponRate = 0;

            comment.ApplicationUserId = this.User.Identity.GetUserId();
            comment.TimePosted = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                var viewModelComment = new CommentsViewModel(comment);
                return Ok(viewModelComment);
            }
            return CreatedAtRoute("DefaultApi", new { id = comment.CommentId }, comment);
        }

        // DELETE: api/CommentsAPI/5
        /// <summary>
        /// Deletes selected comment from db, only user which
        /// created comment can delete it, also admin and super admin
        /// ca delete every comment
        /// </summary>
        /// <param name="id">id of comment to delete</param>
        /// <returns></returns>
        [ResponseType(typeof(Comment))]
        [Authorize(Roles ="Buyer,Admin,SuperAdmin")]
        public IHttpActionResult DeleteComment(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }
            var commentViewModel = new CommentsViewModel(comment);
            if (comment.ApplicationUserId == this.User.Identity.GetUserId() || this.User.IsInRole("Admin") || this.User.IsInRole("SuperAdmin"))
            {
                db.Comments.Remove(comment);
                db.SaveChanges();             
                return Ok(commentViewModel);
            }
            else
                return BadRequest();        
        }

        /// <summary>
        /// Gets rate for selected coupon
        /// </summary>
        /// <param name="couponId">id of coupon</param>
        /// <returns>rate of coupon</returns>
        [Route("api/commentsapi/getrate")]
        [HttpGet]
        public IHttpActionResult GetRate(int couponId)
        {
            var rates = db.Comments.Where(x => x.CouponId == couponId && x.CouponRate != 0).Select(x => x.CouponRate).ToList();
            double response = 0;
            if (rates == null || rates.Count == 0)
                response = 0;
            else
                response = rates.Average();
            return Ok(Math.Ceiling(response));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentExists(int id)
        {
            return db.Comments.Count(e => e.CommentId == id) > 0;
        }
    }
}