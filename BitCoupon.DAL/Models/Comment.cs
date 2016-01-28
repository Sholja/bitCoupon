using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoupon.DAL.Models
{
   public class Comment
    {
        
        public int CommentId { get; set; }

        public int CouponId { get; set; }
        public virtual Coupon Coupon { get; set; }
        [Required]
        [MaxLength(200)]
		public string Content { get; set; }

		public string ApplicationUserId { get; set; }

		public virtual ApplicationUser ApplicationUser { get; set; }

        public int CouponRate { get; set; }

        public DateTime TimePosted { get; set; }




    }

}
