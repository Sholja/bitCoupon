using BitCoupon.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BitCoupon.DAL.LogicCollection
{
	public static class SortingOfCoupons
	{
		/// <summary>
		/// gets coupons from database from category id sent and sorts them (if false) descending otherwise ascending
		/// </summary>
		/// <param name="id">id of category if sent null it will return null</param>
		/// <param name="Asc">bool value optional(if sent false it will sort desc otherwise asc)</param>
		/// <returns>returns list of all coupons from asked category sorted</returns>
		public static List<Coupon> SortCouponsByCategoryID(int? id,bool Asc=true)
		{
			List<Coupon> CouponsByCategoryId=new List<Coupon>();
			if(LogicId.LogicCheckId(id)!=HttpStatusCode.OK)
			{
				return null;
			}
			ApplicationDbContext db = new ApplicationDbContext();

			if (Asc)
			{
				CouponsByCategoryId = db.Coupons.Where(x => x.CategoryId == id).OrderBy(x => x.CouponId).ToList();
			} else
			{
				CouponsByCategoryId = db.Coupons.Where(x => x.CouponId == id).OrderByDescending(x => x.CouponId).ToList();
			}
			return CouponsByCategoryId;
			
		}

		/// <summary>
		/// gets all coupons from database and sorts them by name(ascending or descending)
		/// </summary>
		/// <param name="Asc">if sent nothing or true it will be ascending sorted list otherwise descending</param>
		/// <returns>returns sorted list of coupons</returns>
		public static List<Coupon> SortCouponsByCouponName(bool Asc=true)
		{
			ApplicationDbContext db = new ApplicationDbContext();
			List<Coupon> coupons = new List<Coupon>();
			if(Asc==true)
			{
				coupons=db.Coupons.Where(x => x.CouponId > 0).OrderBy(x => x.Name).ToList();
			} else
			{
				coupons=db.Coupons.Where(x => x.CouponId > 0).OrderBy(x => x.Name).ToList();
			}
			return coupons;
		}
		//public static List<Coupon> SortCouponsByLocationNearestToUser(double lat1, double lon1)
		//{
		//	double rlat1 = Math.PI * lat1 / 180;
		//	double rlat2 = Math.PI * lat2 / 180;
		//	double theta = lon1 - lon2;
		//	double rtheta = Math.PI * theta / 180;
		//	double dist =
		//		Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
		//		Math.Cos(rlat2) * Math.Cos(rtheta);
		//	dist = Math.Acos(dist);
		//	dist = dist * 180 / Math.PI;
		//	dist = dist * 60 * 1.1515;

		//		//Kilometers -> default
		//		return dist * 1.609344;
				
		//	}

		//	return dist;
		//}
		

		
	}
}
