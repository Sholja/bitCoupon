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
    public class QuestionsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Post action for creating question by user
        /// </summary>
        /// <param name="question">object question</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/questions/createquestion")]
        [Authorize(Roles ="Buyer")]
        public IHttpActionResult CreateQuestion(Question question)
        {
            var userId = this.User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            question.BuyerName = user.FirstName;
            question.BuyerAvatar = user.AvatarUrl;
            question.TimePosted = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return Ok(question);
            }
            return BadRequest(ModelState);
        }


        /// <summary>
        /// Action for adding answer to question
        /// </summary>
        /// <param name="questionId">id of question</param>
        /// <param name="answer">answer to the question</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles ="Seller")]
        [Route("api/questions/answer")]
        public IHttpActionResult Answer(string questionId, string answer)
        {
            int id = Int32.Parse(questionId);
            if (answer == null)
                return BadRequest();

            var question = db.Questions.Find(id);

            if (question == null)
                return NotFound();

            var coupon = db.Coupons.Find(question.CouponId);

            if (coupon.ApplicationUserId != this.User.Identity.GetUserId())
                return Unauthorized();

            question.AnswerContent = answer;
            question.TimeAnswered = DateTime.Now;
            question.SellerName = coupon.ApplicationUser.FirstName;
            question.SellerAvatar = coupon.ApplicationUser.AvatarUrl;
            db.SaveChanges();
            return Ok(question);
        }

        /// <summary>
        /// Deletes question by user seller which created coupon
        /// </summary>
        /// <param name="questionId">id of question to delete</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles ="Seller")]
        [Route("api/questions/delete")]
        public IHttpActionResult Delete(string questionId)
        {
            var id = Int32.Parse(questionId);
            var question = db.Questions.Find(id);
            if (question == null)
                return BadRequest();

            if (question.Coupon.ApplicationUserId != this.User.Identity.GetUserId())
                return Unauthorized();

            db.Questions.Remove(question);
            db.SaveChanges();
            return Ok(question);
        }
    }
}
