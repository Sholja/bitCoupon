using BitCoupon.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoupon.API.Models
{
    public class ViewItem
    {
        public int ItemId { get; set; }

        public string CouponName { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string PaymentId { get; set; }

        public string VerificationId { get; set; }

        public DateTime TimeOfPurchase { get; set; }

        public bool IsRefunded { get; set; }

        public int CartId { get; set; }

        private ApplicationDbContext db = new ApplicationDbContext();

        public ViewItem(BitCoupon.DAL.Models.Item item)
        {

            this.ItemId = item.Id;
            this.CouponName = item.Coupon.Name;
            this.Description = item.Coupon.DescriptionOnCoupon;
            this.Price = item.Coupon.Price;
            this.PaymentId = item.Cart.PaymentId;
            this.VerificationId = item.VerificationId;
            this.TimeOfPurchase = item.TimeOfPurchase;
            var refund = db.Refunds.Where(x => x.CartId == item.CartId).SingleOrDefault();
            if (refund != null)
                IsRefunded = refund.Finished == "Approved" ? true : false;

            CartId = item.CartId;
        }
    }
}
