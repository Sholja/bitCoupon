using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BitCoupon.DAL.Models;

namespace BitCoupon.MVC.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        /// <summary>
        /// Deletes comment from db
        /// </summary>
        /// <param name="id">id of comment to delete</param>
        /// <returns></returns>
        [Authorize(Roles ="Admin,SuperAdmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Comment comment = db.Comments.Find(id);
            if (comment == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            db.Comments.Remove(comment);
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);    
        }

    }
}