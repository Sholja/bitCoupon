using BitCoupon.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitCoupon.API.Models
{
	public class CollectionOfModelsFromDALViewModel
	{
		public List<Coupon> coupons;
		public List<Comment> comments;
		public List<ApplicationUser> users;
		ApplicationDbContext db = new ApplicationDbContext();
		public CollectionOfModelsFromDALViewModel()
		{
			coupons = db.Coupons.ToList();
			comments = db.Comments.ToList();
			users = db.Users.ToList();
		}
	}
}