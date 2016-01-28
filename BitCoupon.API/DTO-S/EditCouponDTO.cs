using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BitCoupon.API.DTO_S
{
     [NotMapped]
    public class EditCouponDTO
    {
        public int CouponId { get; set; }
        public int TotalNumberOfCoupons { get; set; }
        public string DescriptionOnCoupon { get; set; }
        public string DescriptionOnSellerPage { get; set; }
        public string SellerUrl { get; set; }
        public string ApplicationUserId { get; set; }
       
    }
}