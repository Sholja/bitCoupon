using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoupon.DAL.Models
{
    public class Item
    {
        public int Id { get; set; }

        public int CouponId { get; set; }

        public virtual Coupon Coupon { get; set; }
        public int CartId { get; set; }

        public virtual Cart Cart { get; set; }

        public int Quantity { get; set; }

        public string VerificationId { get; set; }

        public DateTime TimeOfPurchase { get; set; }

    }
}
