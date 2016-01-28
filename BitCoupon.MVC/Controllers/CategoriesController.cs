using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BitCoupon.DAL.Models;
using BitCoupon.DAL.LogicCollection;

namespace BitCoupon.MVC.Controllers
{
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all categories
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

       /// <summary>
       /// Returns details about selected category
       /// </summary>
       /// <param name="id">id of category</param>
       /// <returns>list of categories</returns>
        public ActionResult Details(int? id)
        {
			Category category=LogicCategories.CategoriesControllerLogic(id);
			if ( category== null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			return View(category);
		}


        /// <summary>
        /// Get action for create category, returns view for create
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Post action for create category
        /// </summary>
        /// <param name="category">object category</param>
        /// <returns></returns>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Approved")] Category category)
        {
            category.Approved = true;
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }
        
        /// <summary>
        /// Get action for edit category
        /// </summary>
        /// <param name="id">id of category</param>
        /// <returns>category to edit</returns>
        public ActionResult Edit(int? id)
        {
			Category category = LogicCategories.CategoriesControllerLogic(id);

			if (category == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			return View(category);
			            
        }

        /// <summary>
        /// Post action for edit category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Approved")] Category category)
        {
           
			if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        /// <summary>
        /// Get action for delete category
        /// </summary>
        /// <param name="id">id of category to delete</param>
        /// <returns>category to delete</returns>
        public ActionResult Delete(int? id)
        {
			Category category = LogicCategories.CategoriesControllerLogic(id);
			if (category==null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.OK);
			}
			return View(category);
			
        }

        /// <summary>
        /// Post action for delete category
        /// </summary>
        /// <param name="id">id of category to delete</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id == 1 || id == 2)
                return RedirectToAction("Index");

            List<Coupon> coupons = db.Coupons.Where(x => x.CategoryId == id).ToList();

            foreach (var item in coupons)
            {
                if (ModelState.IsValid)
                {
                    item.CategoryId = 2;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                    return HttpNotFound();
            }

            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       
        /// <summary>
        /// Get action for category approval
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Approve()
        {
            List<Category> categories = db.Categories.Where(x => x.Approved == false).ToList();
            return View(categories);
        }

        /// <summary>
        /// Post method for categories approval
        /// </summary>
        /// <param name="categories">list of categories
        /// which admin will revaluate</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult Approve(List<Category> categories)
        {
            if (categories == null )
            {             
                return View();
            }
            foreach (var item in categories)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    return View(item);
                }
            }
            return RedirectToAction("Index", "Home");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
