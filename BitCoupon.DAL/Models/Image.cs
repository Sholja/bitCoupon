using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoupon.DAL.Models
{
    public class Image
    {
        public int Id { get; set; }
        public int CouponId { get; set; }

        [DisplayName("Other Images (Optional)")]
        public string ImageUrl { get; set; }

    }
}
