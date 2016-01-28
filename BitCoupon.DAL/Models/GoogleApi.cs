using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace BitCoupon.DAL.Models
{
   public class GoogleApi
    {
        public int Id { get; set; }
        public int CouponId { get; set; }
        public string Long { get; set; }
        public string Lang { get; set; }

        public double DistanceFrom(GeoCoordinate origin)
        {
            GeoCoordinate c2 = new GeoCoordinate(double.Parse(Long), double.Parse(Lang));
            return origin.GetDistanceTo(c2);
        }
    }
}
