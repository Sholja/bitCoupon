using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitCoupon.DAL.Models;

namespace BitCoupon.API.Models
{
    public class GetCouponsViewModel
    {
        public List<CouponViewModel> BestRated { get; set; }

        public List<CouponViewModel> Latest { get; set; }

        public List<CouponViewModel> BestDeals { get; set; }

        private ApplicationDbContext db = new ApplicationDbContext();

        public GetCouponsViewModel()
        {
            BestRated = new List<CouponViewModel>();
            Latest = new List<CouponViewModel>();
            BestDeals = new List<CouponViewModel>();
          
            var query = db.Coupons.Where(x => x.Priority == 2 && x.Acitve == true && x.IsDeleted == false && x.IsEdited == false
            && x.Approved == true).OrderByDescending(x => x.Purchase).Take(3);
            if (query.Count() == 0)
                query = db.Coupons.OrderByDescending(x => x.Purchase).Take(3);

            query.ToList().ForEach(x => BestRated.Add(new CouponViewModel(x)));

            query = db.Coupons.Where(x => x.Acitve == true && x.IsDeleted == false && x.IsEdited == false
            && x.Approved == true).OrderByDescending(x => x.CouponId).Take(2);
            query.ToList().ForEach(x => Latest.Add(new CouponViewModel(x)));

            query = db.Coupons.Where(x => x.Acitve == true && x.IsDeleted == false && x.IsEdited == false
            && x.Approved == true).OrderByDescending(x => ((x.Price - x.NewPrice) / x.Price) * 100).Take(2);

            query.ToList().ForEach(x => BestDeals.Add(new CouponViewModel(x)));

            
        }
    }
}
