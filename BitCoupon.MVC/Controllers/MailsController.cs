using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BitCoupon.DAL.Models;
using BitCoupon.DAL.HtmlSanitizer;
using BitCoupon.DAL.LogicCollection;
using System.Net.Mail;

namespace BitCoupon.MVC.Controllers
{
    public class MailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Gets email tempaltes from database
        /// </summary>
        /// <returns>list with email templates</returns>
        public ActionResult Index()
        {
            return View(db.Mails.ToList());
        }

        /// <summary>
        /// Gets information about selected email temaplate
        /// </summary>
        /// <param name="id">id of template</param>
        /// <returns>selected template</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mail mail = db.Mails.Find(id);
            if (mail == null)
            {
                return HttpNotFound();
            }
            return View(mail);
        }

        /// <summary>
        /// Get action for create email template
        /// </summary>
        /// <returns>view for create</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Post action for create email template
        /// </summary>
        /// <param name="mail">object email template</param>
        /// <returns></returns>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateAntiForgeryToken]
        
        public ActionResult Create([Bind(Include = "Id,Body,Subject")] Mail mail)
        {
            if (ModelState.IsValid)
            {
                mail.Body = HtmlSanitizer.SanitizeHtml(mail.Body);
                db.Mails.Add(mail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mail);
        }

        /// <summary>
        /// Get action for edit email template
        /// </summary>
        /// <param name="id">id of template to edit</param>
        /// <returns>view with selected temlate to edit</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mail mail = db.Mails.Find(id);
            if (mail == null)
            {
                return HttpNotFound();
            }
            return View(mail);
        }

        /// <summary>
        /// Post action for edit email template
        /// </summary>
        /// <param name="mail">edited object email template</param>
        /// <returns></returns>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Body,Subject")] Mail mail)
        {
            if (ModelState.IsValid)
            {
              mail.Body = HtmlSanitizer.SanitizeHtml(mail.Body);
                db.Entry(mail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mail);
        }

        /// <summary>
        /// Get action for delete email template
        /// </summary>
        /// <param name="id">id of template to delete</param>
        /// <returns>view with selected template</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mail mail = db.Mails.Find(id);
            if (mail == null)
            {
                return HttpNotFound();
            }
            return View(mail);
        }

        /// <summary>
        /// Post action for delete email template
        /// </summary>
        /// <param name="id">id of template to delete</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mail mail = db.Mails.Find(id);
            db.Mails.Remove(mail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Get action for create newseltter
        /// </summary>
        /// <returns>view with list of coupons to add in newsletter</returns>
        [HttpGet]
        public ActionResult CreateNewsletter()
        {
             return View(db.Coupons.Where(x=> x.Acitve == true && x.IsDeleted == false && x.IsEdited == false && x.Approved == true).ToList());
        }

        /// <summary>
        /// Post action for create newsletter, gets selected coupons 
        /// and send them via email
        /// </summary>
        /// <param name="id1">id of first coupon</param>
        /// <param name="id2">id of second coupon</param>
        /// <param name="id3">id of third coupon</param>
        /// <param name="id4">id of fourth coupon</param>
        /// <param name="id5">id of fifth coupon</param>
        /// <param name="id6">id of sixth coupon</param>
        /// <param name="id7">id of seventh coupon</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateNewsletter(int id1, int id2, int id3, int id4, int id5, int id6, int id7)
        {
            List<int> ids = new List<int>();
            ids.Add(id1);
            ids.Add(id2);
            ids.Add(id3);
            ids.Add(id4);
            ids.Add(id5);
            ids.Add(id6);
            ids.Add(id7);

            var coupons = db.Coupons.Where(x => x.Acitve == true && x.IsDeleted == false && x.IsEdited == false && x.Approved == true).ToList();

            var myCoupons = new List<Coupon>();
            foreach (var item in ids)
            {
                var coupon = coupons.Where(x => x.CouponId == item).FirstOrDefault();
                if(coupon!= null)
                 myCoupons.Add(coupon);   

            }
            
            bool mailStatus = MailBuilder(myCoupons);

            return RedirectToAction("Index");            
        }
                     
        public ActionResult Test()        
        {
            return View();
        }

        /// <summary>
        /// Help method for sending newsletter via email
        /// </summary>
        /// <param name="coupons">coupons to send in newsletter</param>
        /// <returns></returns>
        private bool MailBuilder(List<Coupon> coupons)
        {        
            var customers = db.Users.Where(x=> x.Role == "Buyer").ToList();
            var mail = db.Mails.Where(x => x.Subject == "Newsletter").FirstOrDefault();

            var body = mail.Body; 
            string subject = mail.Subject;
            
            var counter = 1;

            foreach (var item in coupons)
	        {
                string tempPicture = "%pic" + counter+"%";
                string tempText = "%text" + counter + "%";

                body = body.Replace(tempPicture, item.PictureUrl);
                body = body.Replace(tempText, item.Name);
                counter++;
	        }
		            
            foreach (var item in customers)
	        {
               var user = new MailAddress(item.Email,item.FirstName);
               MailService.SendMail(subject, body, user);
	        }
            return true;
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
