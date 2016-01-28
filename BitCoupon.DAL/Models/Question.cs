using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoupon.DAL.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string QuestionContent { get; set; }

        [MaxLength(200)]
        public string AnswerContent { get; set; }

        public DateTime TimePosted { get; set; }

        public DateTime? TimeAnswered { get; set; }

        public int CouponId { get; set; }
        public virtual Coupon Coupon { get; set; }

        public string BuyerName { get; set; }
        public string BuyerAvatar { get; set; }
        public string SellerName { get; set; }
        public string SellerAvatar { get; set; }
    }
}
