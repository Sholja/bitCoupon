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

namespace BitCoupon.API.Controllers
{
    public class GoogleAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       

        /// <summary>
        /// Gets location for google map, for specific coupon
        /// </summary>
        /// <param name="id">id of selected coupon</param>
        /// <returns></returns>
        [ResponseType(typeof(GoogleApi))]
        public IHttpActionResult GetGoogleApi(int id)
        {
            var result = db.GoogleApis.Where(x => x.CouponId == id).ToList();

            if (result.Count == 0)
                return StatusCode(HttpStatusCode.BadRequest);

            return Ok(result);
        }

        /// <summary>
        /// Action for edit locations for google map
        /// </summary>
        /// <param name="myplaces">list of locations</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGoogleApi(List<GoogleApi> myplaces)
        {
            if (myplaces == null)
            {
                return BadRequest(); /// checks if list is empty
            }

            foreach (var item in myplaces)
            {
                if (item.Id == null) // checks if id is null 
                {
                    return StatusCode
                        (HttpStatusCode.NotImplemented);
                }
                try
                {
                    double.Parse(item.Lang);
                    double.Parse(item.Long);           //checks if coordinates are numbers 
                }
                catch (Exception e)
                {
                    return StatusCode(HttpStatusCode.RequestedRangeNotSatisfiable);
                }
            }

            var id = myplaces[0].CouponId;
            List<GoogleApi> temp = db.GoogleApis.Where(x => x.CouponId == id).ToList();

            if (ModelState.IsValid)
            {
                foreach (var item in myplaces)
                {
                    db.GoogleApis.Add(item);
                    db.SaveChanges();
                }
            }
            foreach (var item in temp)
            {
                db.GoogleApis.Remove(item);
                db.SaveChanges();
            }

            return Ok();
        }


        /// <summary>
        /// Creates new locations for google map for coupon
        /// </summary>
        /// <param name="myplaces">list of locations</param>
        /// <returns></returns>
        [ResponseType(typeof(GoogleApi))]
        public IHttpActionResult PostGoogleApi(List<GoogleApi> myplaces)
        {
            if (myplaces.Count == 0)
                return BadRequest();

            if (myplaces.First().CouponId == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var id = myplaces[0].CouponId;
                ApplicationDbContext temp = new ApplicationDbContext();

                var locations = temp.GoogleApis.Where(x => x.CouponId == id).Count();

                if (locations == 0)
                {
                    foreach (var item in myplaces)
                    {
                        db.GoogleApis.Add(item);
                        db.SaveChanges();
                    }
                }
                else
                {
                   return StatusCode (HttpStatusCode.Forbidden);
                }
            }
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GoogleApiExists(int id)
        {
            return db.GoogleApis.Count(e => e.Id == id) > 0;
        }
    }
}