using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoupon.DAL.Models
{
    public class Buy
    {
        public int Id { get; set; }
        public int CouponId { get; set; }
        public virtual Coupon Coupon { get; set; }

        public string ApplicationUserId { get; set; }
    }
}
