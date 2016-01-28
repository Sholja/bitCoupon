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
using CloudinaryDotNet;
using System.Web;
using CloudinaryDotNet.Actions;
using System.IO;
using BitCoupon.API.Models;
using System.Threading;
using Microsoft.AspNet.Identity;
using BitCoupon.DAL.HtmlSanitizer;
using BitCoupon.API.DTO_S;




namespace BitCoupon.API.Controllers
{
    public class CouponsAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Gets list with coupons for index page
        /// </summary>
        /// <param name="page">page for pagination</param>
        /// <returns>list with coupons</returns>
        public IHttpActionResult GetCoupons(int? page)
        {
            int pageSize = 4;
            int skip = 0;

            if (page != null)
            {
                skip = page.Value;
            }

            List<Coupon> coupons = db.Coupons.Include(c => c.Category).Where(x => x.IsEdited == false && x.IsDeleted == false && x.Acitve == true && x.Approved == true && x.ExpirationTime > DateTime.Now).OrderBy(x => x.CouponId).Skip(skip * pageSize).Take(pageSize).ToList();

            bool hasNext = db.Coupons.Count() > ((skip + 1) * pageSize);
            int next = skip + 1;

            return Ok(new Pagination() { Coupons = coupons, HasNext = hasNext, NextPage = next });
        }

        /// <summary>
        /// Gets all expired coupons
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Route("api/CouponsAPI/GetExpiredCoupons/{page}")]
        public IHttpActionResult GetExpiredCoupons(int? page)
        {
            if (page == null)
                return BadRequest();
            int pageSize = 4;
            int skip = page.Value; ;
         
            List<Coupon> coupons = db.Coupons.Include(c => c.Category).Where(x => x.ExpirationTime < DateTime.Now || x.TotalNumberOfCoupons == 0).OrderBy(x => x.ExpirationTime).Skip(skip * pageSize).Take(pageSize).ToList();

            bool hasNext = coupons.Count() > ((skip + 1) * pageSize);
            int next = skip + 1;

            return Ok(new Pagination() { Coupons = coupons, HasNext = hasNext, NextPage = next });
        }

        /// <summary>
        /// Gets selected coupon details
        /// </summary>
        /// <param name="id">id of selected coupon</param>
        /// <returns>selected coupon</returns>
        [ResponseType(typeof(Coupon))]
        public IHttpActionResult GetCoupon(int id)
        {
            Coupon coupon = db.Coupons.Where(x => x.CouponId == id).Include(c => c.Images).Include(z => z.Comments).SingleOrDefault();
            if (coupon == null)
            {
                return NotFound();
            }
            if (coupon.Acitve == false || coupon.IsDeleted == true || coupon.IsEdited == true || coupon.Approved != true)
            {
                return BadRequest();
            }

            var couponView = new CouponViewModel(coupon);

            return Ok(couponView);
        }

        /// <summary>
        /// Returns details of selected expired coupon
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/couponsapi/getexpiredcoupon")]
        [HttpGet]
        public IHttpActionResult GetExpiredCoupon(int couponId)
        {
            var coupon = db.Coupons.Find(couponId);
            if (coupon == null)
                return NotFound();

            var couponView = new CouponViewModel(coupon);
            return Ok(couponView);
        }

        /// <summary>
        /// Action for edit coupon
        /// </summary>
        /// <param name="id">coupon id</param>
        /// <param name="coupon">dto with changed values for coupon</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        [Authorize(Roles ="Seller")]
        public IHttpActionResult PutCoupon(int id, EditCouponDTO coupon)
        {

            if (coupon.ApplicationUserId != this.User.Identity.GetUserId())
            {
                return Unauthorized();
            
            }
            //ApplicationDbContext dbtemp = new ApplicationDbContext();
            //var dbCoupon = db.Coupons.Find(id);
            var dbCoupon = db.Coupons.Find(id);
            // TODO cant increse total number of coupons !!
           


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coupon.CouponId)
            {
                return BadRequest();
            }
            if (coupon.TotalNumberOfCoupons % dbCoupon.RequiredNumberOfCoupons != 0)
            {
                return StatusCode(HttpStatusCode.NotAcceptable);
            }
            if (coupon.TotalNumberOfCoupons + dbCoupon.TotalNumberOfCoupons > 1000)
            {
                return BadRequest();
            }
         

            HtmlSanitizer.SanitizeHtml(coupon.DescriptionOnSellerPage); // checks for unavailable content

            dbCoupon.TotalNumberOfCoupons += coupon.TotalNumberOfCoupons;
            dbCoupon.DescriptionOnCoupon = coupon.DescriptionOnCoupon;
            dbCoupon.DescriptionOnSellerPage = coupon.DescriptionOnSellerPage;
            dbCoupon.SellerUrl = coupon.SellerUrl;
            dbCoupon.IsEdited = true;
           //implement automapper
           
            db.Entry(dbCoupon).State = EntityState.Modified;
            db.SaveChanges();


            return Ok(dbCoupon);
        }

        /// <summary>
        /// Action for create new coupon
        /// </summary>
        /// <param name="coupon">created object coupon</param>
        /// <returns></returns>
        [Authorize(Roles = "Seller")]
        [ResponseType(typeof(Coupon))]
        public IHttpActionResult PostCoupon(Coupon coupon)
        {
            HtmlSanitizer.SanitizeHtml(coupon.DescriptionOnSellerPage);
            coupon.ApplicationUserId = this.User.Identity.GetUserId();
            coupon.IsDeleted = false;
            coupon.IsEdited = false;
            coupon.Acitve = true;
            coupon.Approved = false;
            coupon.PictureUrl = "/Images/placeholder.png";
            coupon.Priority = 3;
            this.Validate(coupon);


            if (coupon.NuberOfCouponsToOfferManaged % coupon.RequiredNumberOfCoupons != 0 || coupon.NuberOfCouponsToOfferManaged > coupon.TotalNumberOfCoupons)
            {
                ModelState.AddModelError("NuberOfCouponsToOfferManaged", "Number of coupons to satisfy your ofer has to be divisible by required number of coupons");

                return BadRequest(ModelState);
            }
            if (coupon.Price < coupon.NewPrice)
            {
                ModelState.AddModelError("NewPrice", "New Price has to be less then current price");

                return BadRequest(ModelState);
            }


            if (ModelState.IsValid)
            {
                if ((coupon.TotalNumberOfCoupons % coupon.RequiredNumberOfCoupons) != 0 || coupon.TotalNumberOfCoupons < coupon.RequiredNumberOfCoupons)
                {
                    ModelState.AddModelError("TotalNumberOfCoupons", "Total number of coupons doesn't match required coupon count.");
                    return BadRequest(ModelState);
                }

                db.Coupons.Add(coupon);
                db.SaveChanges();

                
                return CreatedAtRoute("DefaultApi", new { id = coupon.CouponId }, coupon);
            }


          return BadRequest(ModelState);
        }

        /// <summary>
        /// Action for delete coupon
        /// </summary>
        /// <param name="id">id of coupon to delete</param>
        /// <returns></returns>
        [ResponseType(typeof(Coupon))]
        [Authorize(Roles ="Seller")]
        public IHttpActionResult DeleteCoupon(int id)
        {
            Coupon coupon = db.Coupons.Find(id);
            if (coupon == null)
            {
                return NotFound();
            }
            if (coupon.ApplicationUserId != this.User.Identity.GetUserId())
                return Unauthorized();

            if (coupon.Purchase * coupon.RequiredNumberOfCoupons >= coupon.NuberOfCouponsToOfferManaged)
            {
                return BadRequest();
            }

            coupon.IsDeleted = true;
            coupon.Acitve = true;
            coupon.Approved = true;
            db.SaveChanges();

            return Ok(coupon);
        }

        

        /// <summary>
        /// Searches coupons by name and category
        /// </summary>
        /// <param name="name">name of coupon</param>
        /// <param name="CategoryId">id of category</param>
        /// <returns>partial View, list of coupons</returns>
        [HttpGet]
        public IHttpActionResult Search(string name, int CategoryId, bool method)
        {
            if (method)
            {
                int num = CategoryId;

                if (name == null)
                    name = "";

                var priorityNoTwo = db.Coupons.Where(x => x.Acitve == true && x.IsDeleted == false && x.IsEdited == false && x.Approved == true && x.Priority == 2).ToList();

                var query = num == 1 ? db.Coupons.  //if category id==1 (All Categories) search only by name 
                       Where(x => x.Name.Contains(name) && 
                       x.Acitve == true && x.IsDeleted == false && 
                       x.IsEdited == false && x.Approved == true &&
                      x.Priority == 1).ToList()
                       : db.Coupons.
                       Where(x => x.Name.Contains(name) && 
                       x.CategoryId == num && x.Category.Approved == true && 
                       x.Acitve == true && x.IsDeleted == false && 
                       x.IsEdited == false && x.Approved == true &&
                       x.Priority == 1).ToList();

                var priorityNotThree = num == 1 ? db.Coupons.  //if category id==1 (All Categories) search only by name 
                       Where(x => x.Name.Contains(name) &&
                       x.Acitve == true && x.IsDeleted == false &&
                       x.IsEdited == false && x.Approved == true &&
                      x.Priority == 3).ToList()
                       : db.Coupons.
                       Where(x => x.Name.Contains(name) &&
                       x.CategoryId == num && x.Category.Approved == true &&
                       x.Acitve == true && x.IsDeleted == false &&
                       x.IsEdited == false && x.Approved == true &&
                       x.Priority == 3).ToList();

                priorityNoTwo.AddRange(priorityNotThree);
                var list = Randomize(priorityNoTwo);

                query.AddRange(list);

                return Ok(query);
            }
            else
            {
                int num = CategoryId;

                if (name == null)
                    name = "";

                var result = num == 1 ? db.Coupons.  //if category id==1 (All Categories) search only by name 
                       Where(x => x.Name.Contains(name) && (x.ExpirationTime < DateTime.Now || x.TotalNumberOfCoupons == 0)).OrderByDescending(x => x.CouponId)
                       : db.Coupons.
                       Where(x => x.Name.Contains(name) && x.CategoryId == num && x.Category.Approved == true && (x.ExpirationTime < DateTime.Now || x.TotalNumberOfCoupons == 0)).OrderByDescending(x => x.CouponId);

                return Ok(result.ToList());
            }
        }

        /// <summary>
        /// Gets all avatars from db
        /// </summary>
        /// <returns>returns list with avatars</returns>
        [Route("api/couponsapi/getavatars")]
        [HttpGet]
        public IQueryable GetAvatars()
        {
            return db.Avatars.OrderBy(x => x.Name);
        }
        

        /// <summary>
        /// Gets purchased items (coupons) for logged in user
        /// </summary>
        /// <param name="page">page for pagination</param>
        /// <param name="reverse">for sorting order</param>
        /// <param name="sort">which column to sort</param>
        /// <param name="search">for search (by name and description)</param>
        /// <returns></returns>
        [Route("api/couponsapi/getitems")]
        [HttpGet]
        public IHttpActionResult GetItems(int page, bool reverse, string sort, string search)
        {
            string userId = this.User.Identity.GetUserId();
            List<ViewItem> Items = new List<ViewItem>();
            IQueryable<BitCoupon.DAL.Models.Item> query = db.Items.Where(x => x.Cart.ApplicationUserId == userId).OrderByDescending(x => x.Cart.TimeOfPurchase).Skip(page).Take(8);

            if (search != null)
                query = query.Where(x => x.Coupon.Name.Contains(search) || x.Coupon.DescriptionOnCoupon.Contains(search));

            if (sort != null)
                query = SortItems(sort, query, reverse);

            query.ToList().ForEach(x => Items.Add(new ViewItem(x)));

            return Ok(Items);
        }

        /// <summary>
        /// Gets active coupons from selected seller
        /// to display them on seller page
        /// </summary>
        /// <param name="userId">id of selected user</param>
        /// <returns>list of coupons</returns>
       [Route("api/couponsapi/getactive")]
       [HttpGet]
        public IHttpActionResult GetActive(int page, bool reverse, string sort, string search, int? couponId, bool method)
        {
            string userId = string.Empty;
            if (couponId == null || couponId == 0)
                userId = this.User.Identity.GetUserId();
            else
                userId = db.Coupons.Find(couponId).ApplicationUserId;
            var query = method ? db.Coupons.Where(x => x.ApplicationUserId == userId
            && x.Acitve == true && x.IsDeleted == false && x.IsEdited == false
            && x.Approved == true).OrderByDescending(x => x.ExpirationTime).Skip(page).Take(8) :
            db.Coupons.Where(x => x.ApplicationUserId == userId
            && x.Acitve == false && x.IsDeleted == false && x.IsEdited == false
            && x.Approved == true).OrderByDescending(x => x.ExpirationTime).Skip(page).Take(8)
            ;

            if (search != null)
                query = query.Where(x => x.Name.Contains(search) || x.DescriptionOnCoupon.Contains(search));

            if (sort != null)
                query = SortCoupons(sort, query, reverse);

            return Ok(query.ToList());
        }

        /// <summary>
        /// Returns collection of coupons (best rated, latest and best deals)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/couponsapi/bestrated")]
        public IHttpActionResult BestRated()
        {
            GetCouponsViewModel coupons = new GetCouponsViewModel();
            return Ok(coupons);
        }
        

        /// <summary>
        /// Help method which will sort coupons depending 
        /// on input parameterd
        /// </summary>
        /// <param name="sort">column to sort</param>
        /// <param name="query">query to sort</param>
        /// <param name="reverse">sorting oreder</param>
        /// <returns></returns>
        private IQueryable<BitCoupon.DAL.Models.Item> SortItems(string sort, IQueryable<BitCoupon.DAL.Models.Item> query, bool reverse)
        {
            switch (sort)
            {
                case "couponName":
                    {
                        if (!reverse)
                            query = query.OrderByDescending(x => x.Coupon.Name);
                        else
                            query = query.OrderBy(x => x.Coupon.Name);
                        break;
                    }
                case "description":
                    {
                        if (!reverse)
                            query = query.OrderByDescending(x => x.Coupon.DescriptionOnCoupon);
                        else
                            query = query.OrderBy(x => x.Coupon.DescriptionOnCoupon);
                        break;
                    }
                case "price":
                    {
                        if (!reverse)
                            query = query.OrderByDescending(x => x.Coupon.Price);
                        else
                            query = query.OrderBy(x => x.Coupon.Price);
                        break;
                    }
                default:
                    query = query.OrderByDescending(x => x.Coupon.CouponId);
                    break;
            }
            return query;
        }

        /// <summary>
        /// Randomize members of list
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<Coupon> Randomize(List<Coupon> list)
        {
            Random random = new Random();
            List<Coupon> coupons = new List<Coupon>();
            int index = 0, length = list.Count ;
            for (int i = 0; i < length; i++)
            {
                do
                {
                    index = random.Next(0, list.Count - 1);
                } while (list.ElementAt(index) == null);
                coupons.Add(list[index]);
                list.RemoveAt(index);
            }

            return coupons;
        }

        private IQueryable<Coupon> SortCoupons(string sort, IQueryable<Coupon> query, bool reverse)
        {
            switch (sort)
            {
                case "couponName":
                    {
                        if (!reverse)
                            query = query.OrderByDescending(x => x.Name);
                        else
                            query = query.OrderBy(x => x.Name);
                        break;
                    }
                case "description":
                    {
                        if (!reverse)
                            query = query.OrderByDescending(x => x.DescriptionOnCoupon);
                        else
                            query = query.OrderBy(x => x.DescriptionOnCoupon);
                        break;
                    }
                case "price":
                    {
                        if (!reverse)
                            query = query.OrderByDescending(x => x.Price);
                        else
                            query = query.OrderBy(x => x.Price);
                        break;
                    }
                default:
                    query = query.OrderByDescending(x => x.CouponId);
                    break;
            }
            return query;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CouponExists(int id)
        {
            return db.Coupons.Count(e => e.CouponId == id) > 0;
        }
    }
}