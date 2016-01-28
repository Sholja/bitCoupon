using BitCoupon.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitCoupon.MVC.Models
{
	public class CollectionOfModelsViewModel
	{
		public List<Coupon> coupons;
		public List<Category> categories;
		public List<ApplicationUser> users;
        public int userCount;
		ApplicationDbContext db = new ApplicationDbContext();
      
        public CollectionOfModelsViewModel()
		{
			coupons = db.Coupons.ToList();
			categories = db.Categories.ToList();

            var admin = db.Roles.Where(x => x.Name == "Admin").FirstOrDefault();
            var super = db.Roles.Where(x => x.Name == "SuperAdmin").FirstOrDefault();

            users = db.Users
                .Where(x => x.Roles.Select(y => y.RoleId)
                .Contains(admin.Id) || 
                x.Roles.Select(z => z.RoleId)
                .Contains(super.Id)).ToList();

            userCount = db.Users.ToList().Count();
		}

	}
}