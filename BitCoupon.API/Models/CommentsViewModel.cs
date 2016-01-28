using BitCoupon.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoupon.API.Models
{
    public class CommentsViewModel
    {
        public int CommentId { get; set; }

        public int CouponId { get; set; }
        
        public string Content { get; set; }

        public int CouponRate { get; set; }

        public string Name { get; set; }

        public string ApplicationUserId { get; set; }

        public string AvatarUrl { get; set; }

        public DateTime TimePosted { get; set; }

        private ApplicationDbContext db = new ApplicationDbContext();

        public CommentsViewModel(Comment comment)
        {
            var user = db.Users.Find(comment.ApplicationUserId);
            this.CommentId = comment.CommentId;
            this.CouponId = comment.CouponId;
            this.Content = comment.Content;
            this.CouponRate = comment.CouponRate;
            this.Name = user.FirstName;
            this.ApplicationUserId = comment.ApplicationUserId;
            this.TimePosted = comment.TimePosted;
            this.AvatarUrl = user.AvatarUrl;
        }

    }
}
