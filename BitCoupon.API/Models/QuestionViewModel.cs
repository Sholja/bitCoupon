using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitCoupon.DAL.Models;

namespace BitCoupon.API.Models
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string QuestionContent { get; set; }
        public string AnswerContent { get; set; }
        public DateTime TimePosted { get; set; }
        public DateTime? TimeAnswered { get; set; }
        public int CouponId { get; set; }
        public Coupon Coupon { get; set; }

        public string BuyerName { get; set; }
        public string BuyerAvatar { get; set; }
        public string SellerName { get; set; }
        public string SellerAvatar { get; set; }

        public QuestionViewModel(Question question)
        {
            this.Id = question.Id;
            this.QuestionContent = question.QuestionContent;
            this.AnswerContent = question.AnswerContent;
            this.TimePosted = question.TimePosted;
            this.TimeAnswered = question.TimeAnswered;
            this.CouponId = question.CouponId;
            this.Coupon = question.Coupon;
            this.BuyerName = question.BuyerName;
            this.SellerName = question.SellerName;
            this.BuyerAvatar = question.BuyerAvatar;
            this.SellerAvatar = question.SellerAvatar;
        }
    }
}
