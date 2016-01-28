using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BitCoupon.DAL.Models;
using System.Threading;
using BitCoupon.DAL.validators;
using Microsoft.AspNet.Identity;

namespace BitCoupon.MVC.Controllers
{
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class MessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Gets public messages for all admins, 
        /// messages sent by system
        /// </summary>
        /// <returns>list with messages</returns>
        public ActionResult Index()
        {
            return View(db.Messages.Where(x => x.Sender == "System").ToList());
        }

        /// <summary>
        /// Gets details information about selected message
        /// </summary>
        /// <param name="id">id of message</param>
        /// <returns>selected message</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        /// <summary>
        /// Action for delete message
        /// </summary>
        /// <param name="id">id of message to delete</param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Message message = db.Messages.Find(id);
            if (message == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            db.Messages.Remove(message);
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Get action for send message, returns view for sending message
        /// </summary>
        /// <returns>view for create message</returns>
        public ActionResult Send()
        {
            return View();
        }

        /// <summary>
        /// Post action for send message
        /// </summary>
        /// <param name="message">object message</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Send(Message message)
        {
            message.Time = DateTime.Now;
            message.Sender = User.Identity.Name;

            if (User.Identity.GetUserId() == message.ApplicationUserId)  //user can't send message to himself
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }

            Response.StatusCode = 400;
            Response.TrySkipIisCustomErrors = true;
            var modelErrors = ModelState.AllErrors();
            return Json(modelErrors);
        }

        /// <summary>
        /// Gets private messages of user which is logged in
        /// </summary>
        /// <returns>view with messages</returns>
        public ActionResult PrivateMessages()
        {
            string userId = User.Identity.GetUserId();
            List<Message> messages = db.Messages.Where(x => x.ApplicationUserId == userId).ToList();
            return View(messages);
        }

        /// <summary>
        /// Gets details information about selected private message
        /// </summary>
        /// <param name="id">id of selected message</param>
        /// <returns>selected message</returns>
        public ActionResult DetailsPrivate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
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
