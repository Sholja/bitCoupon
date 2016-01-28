using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BitCoupon.DAL.Models;
using BitCoupon.API.Models;

namespace BitCoupon.API.Controllers
{
    public class CategoriesAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Gets categories for search dropdown
        /// </summary>
        /// <returns>list with categories</returns>
        public IHttpActionResult GetCategories()
        {
            var query = db.Categories.Where(x => x.Approved == true).OrderBy(x => x.Name);
            List<CategoriesViewModel> categories = new List<CategoriesViewModel>();
            query.ToList().ForEach(x => categories.Add(new CategoriesViewModel(x)));
            return Ok(categories) ;
        }

        /// <summary>
        /// Gets categories for create coupon dropdown
        /// </summary>
        /// <returns>list with categories</returns>
        [Route("api/categoriesapi/getcategoriesforcreate")]
        [HttpGet]
        public IHttpActionResult GetCategoriesForCreate()
        {
            var query = db.Categories.Where(x => x.Approved == true && x.Name != "All Categories").OrderBy(x => x.Name);
            List<CategoriesViewModel> categories = new List<CategoriesViewModel>();
            query.ToList().ForEach(x => categories.Add(new CategoriesViewModel(x)));
            return Ok(categories);
        }


        /// <summary>
        /// Search coupon by category, gets coupons based
        /// on selected category
        /// </summary>
        /// <param name="name">category name</param>
        /// <returns>list with coupons</returns>
        [ResponseType(typeof(Category))]
        public List<Coupon> GetCategory(string name)
        {
            var result = name == "All Categories" ? db.Coupons.Where(x => x.Acitve == true && x.IsDeleted == false
            && x.IsEdited == false && x.Approved == true).ToList() :
            db.Coupons.Where(x => x.Category.Name == name && x.Acitve == true
            && x.IsDeleted == false && x.IsEdited == false && x.Approved == true).ToList();

            return result;
        }     

        /// <summary>
        /// Action for create category by seller
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [ResponseType(typeof(Category))]
        [Authorize(Roles ="Seller")]
        public IHttpActionResult PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category.Id }, category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.Id == id) > 0;
        }
    }
}