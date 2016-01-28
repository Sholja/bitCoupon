using BitCoupon.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoupon.API.Models
{
    public class CouponViewModel
    {
        public int CouponId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double NewPrice { get; set; }
        public string DescriptionOnCoupon { get; set; }

        public string DescriptionOnSellerPage { get; set; }
        public DateTime ExpirationTime { get; set; }

        public int NuberOfCouponsToOfferManaged { get; set; }

        public int RequiredNumberOfCoupons { get; set; }
        public int TotalNumberOfCoupons { get; set; }
        public string SellerUrl { get; set; }
        public string PictureUrl { get; set; }

        public string SellerName { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Image> Images { get; set; }
        public List<GoogleApi> Locations { get; set; }

        public List<CommentsViewModel> Comments { get; set; }
        
        public string ApplicationUserId { get; set; }

        public List<QuestionViewModel> Questions { get; set; }
        public List<Coupon> CtgCoupons { get; set; }

        public int Rating { get; set; }

        private ApplicationDbContext db = new ApplicationDbContext();

        public CouponViewModel(Coupon coupon)
        {
            this.Comments = new List<CommentsViewModel>();
            this.Questions = new List<QuestionViewModel>();
            this.CouponId = coupon.CouponId;
            this.Name = coupon.Name;
            this.Price = coupon.Price;
            this.NewPrice = coupon.NewPrice;
            this.DescriptionOnCoupon = coupon.DescriptionOnCoupon;
            this.DescriptionOnSellerPage = coupon.DescriptionOnSellerPage;
            this.ExpirationTime = coupon.ExpirationTime;
            this.NuberOfCouponsToOfferManaged = coupon.NuberOfCouponsToOfferManaged;
            this.RequiredNumberOfCoupons = coupon.RequiredNumberOfCoupons;
            this.TotalNumberOfCoupons = coupon.TotalNumberOfCoupons;
            this.SellerUrl = coupon.SellerUrl;
            this.PictureUrl = coupon.PictureUrl;
            this.CategoryId = coupon.CategoryId;
            this.Category = coupon.Category;
            this.SellerName = coupon.ApplicationUser.FirstName + " " + coupon.ApplicationUser.LastName;
            this.Images = db.Images.Where(x => x.CouponId == coupon.CouponId).ToList();
            this.Locations = db.GoogleApis.Where(x => x.CouponId == coupon.CouponId).ToList();
            List<Comment> comments = db.Comments.Where(x => x.CouponId == coupon.CouponId).ToList();
            for (int i = 0; i < comments.Count; i++)
            {
                CommentsViewModel commentView = new CommentsViewModel(comments[i]);
                Comments.Add(commentView);
            }
            this.ApplicationUserId = coupon.ApplicationUserId;
            var questions = db.Questions.Where(x => x.CouponId == coupon.CouponId).ToList();
            for (int i = 0; i < questions.Count; i++)
            {
                var question = new QuestionViewModel(questions[i]);
                this.Questions.Add(question);
            }
            this.CtgCoupons = db.Coupons.Where(x => x.CategoryId == this.CategoryId).Take(2).ToList();

            var rates = db.Comments.Where(x => x.CouponId == coupon.CouponId && x.CouponRate != 0).Select(x => x.CouponRate).ToList();
            if (rates.Count != 0)
                Rating = (int)rates.Average();
            else
                Rating = 0;
        }
    }
}
