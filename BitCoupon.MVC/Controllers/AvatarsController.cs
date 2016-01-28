using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BitCoupon.DAL.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Drawing;
using System.IO;

namespace BitCoupon.MVC.Controllers
{
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class AvatarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string[] _allowedTypes = new string[] { "image/jpeg", "image/jpg", "image/png" };

        /// <summary>
        /// Gets list with avatars
        /// </summary>
        /// <returns>list of avatars</returns>
        public ActionResult Index()
        {
            return View(db.Avatars.ToList());
        }

        /// <summary>
        /// Gets details information about selected avatar
        /// </summary>
        /// <param name="id">id of avatar</param>
        /// <returns>selected avatar</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avatar avatar = db.Avatars.Find(id);
            if (avatar == null)
            {
                return HttpNotFound();
            }
            return View(avatar);
        }

        /// <summary>
        /// Returns view for create avatar
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Post action for creating new avatar
        /// </summary>
        /// <param name="avatar">object avatar</param>
        /// <returns></returns>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PictureUrl,Name")] Avatar avatar)
        {
            string path = UploadImage();
            if (path != string.Empty)
                avatar.PictureUrl = "http://res.cloudinary.com" + path;
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (ModelState.IsValid)
            {
                db.Avatars.Add(avatar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(avatar);
        }

        /// <summary>
        /// Post action for delete avatar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Avatar avatar = db.Avatars.Find(id);
            db.Avatars.Remove(avatar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Uploads image on cloudinary
        /// </summary>
        /// <returns></returns>
        private string UploadImage()
        {
            Account account = new Account("dvnnqotru", "252251816985341", "eqxRrtlVyiWA5-WCil_idtLzP6c");
            Cloudinary cloudinary = new Cloudinary(account);

            if (Request.Files.Count > 0)  //is there file to upload
            {
                HttpPostedFileBase file = Request.Files[0];
                if (file.ContentLength > 0 && _allowedTypes.Contains(file.ContentType))
                {
                    try  //test is selected file a image
                    {
                        using (var bitmap = new Bitmap(file.InputStream))
                        {
                            if (bitmap.Width < 110 || bitmap.Height < 140)  //minimum size of picture is 110x140
                            {
                                ModelState.AddModelError("PictureUrl", "Picture is too small");
                                return null;
                            }
                        }
                    }
                    catch
                    {
                        ModelState.AddModelError("PictureUrl", "The file type is not supported");
                        return null;
                    }

                    file.SaveAs(Path.Combine(Server.MapPath("~/Content/"), file.FileName));

                    var uploadParams = new ImageUploadParams()  //upload to cloudinary
                    {
                        File = new FileDescription(Path.Combine(Server.MapPath("~/Content/"), file.FileName)),
                        Transformation = new Transformation().Crop("fill").Width(110).Height(140)

                    };

                    var uploadResult = cloudinary.Upload(uploadParams);
                    System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/"), file.FileName));
                    return uploadResult.Uri.AbsolutePath;
                }
                else
                {
                    ModelState.AddModelError("PictureUrl", "The file type is not supported");
                    return null;
                }
            }
            return null;
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
