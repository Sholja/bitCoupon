using BitCoupon.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitCoupon.API.Models
{
    public class Pagination
    {
        public List<Coupon> Coupons { get; set; }
        public bool HasNext { get; set; }
        public int NextPage { get; set; }
    }
}