using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitCoupon.DAL.Models;

namespace BitCoupon.API.Models
{
    public class DetailsViewModel
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public Coupon Coupon { get; set; }

        public List<Report> Reports { get; set; }

        public List<Comment> Comments { get; set; }
        public List<Coupon> CtgCoupons { get; set; }


        public DetailsViewModel(int id)
        {
            Coupon = db.Coupons.Find(id);
            Reports = db.Reports.Where(x => x.CommentId == id).ToList();
            Comments = db.Comments.Where(x => x.CouponId == id).ToList();
            CtgCoupons = db.Coupons.Where(x => x.CategoryId == Coupon.CategoryId).ToList();
        }
    }
}
