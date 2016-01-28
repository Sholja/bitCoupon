using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitCoupon.DAL.Models;
using System.Web.Security;

namespace BitCoupon.MVC.Controllers
{
    
	public class HomeController : Controller
	{
        ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Admin, SuperAdmin,Salesman")]
        public ActionResult Index()
		{
            return View();
		}

        /// <summary>
        /// Returns view with users
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Users()
        {
            if (User.IsInRole("SuperAdmin"))  //if user isn't is in role SuperAdmin, don't return him dropdown for change role with admin role
            {
                ViewBag.RoleId = new SelectList(db.Roles.Where(x => x.Name != "SuperAdmin").ToList(), "Id", "Name");
            }
            else
            {
                ViewBag.RoleId = new SelectList(db.Roles.Where(x => x.Name != "SuperAdmin" && x.Name != "Admin").ToList(), "Id", "Name");
            }
            return View(db.Users.ToList());
        }
	}
}