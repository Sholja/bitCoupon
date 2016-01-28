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
using System.Web;
using System.IO;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace BitCoupon.API.Controllers
{
    public class ImagesUploadApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Action for edit images of selected coupon
        /// </summary>
        /// <param name="id">id of coupon</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutImage(int id)
        {          
           if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var httpPostedFile = HttpContext.Current.Request.Files["file"];
                bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath("~/UploadedDocuments"));
                if (!folderExists)
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/UploadedDocuments"));
                var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedDocuments"),
                                                httpPostedFile.FileName);
                httpPostedFile.SaveAs(fileSavePath);

                if (File.Exists(fileSavePath))
                {
                    Account account = new Account("dvnnqotru", "252251816985341", "eqxRrtlVyiWA5-WCil_idtLzP6c");
                    Cloudinary cloudinary = new Cloudinary(account);

                    var uploadParams = new ImageUploadParams()  //upload to cloudinary
                    {
                        File = new FileDescription(fileSavePath),
                        Transformation = new Transformation().Crop("fill").Width(720).Height(480)

                    };
                    
                    
                    var uploadResult = new ImageUploadResult();
                   
                    try
                    {

                         uploadResult = cloudinary.Upload(uploadParams);
                    }
                    catch
                    {

                        return StatusCode(HttpStatusCode.ExpectationFailed);
                    }

                    System.IO.File.Delete(fileSavePath);

                        try
                        {
                            var coupon = db.Coupons.Find(id);
                            coupon.PictureUrl = "http://res.cloudinary.com" + uploadResult.Uri.AbsolutePath;
                            db.Entry(coupon).State = EntityState.Modified;
                            db.SaveChanges();
                            return Ok(coupon.PictureUrl);
                        }
                        catch (Exception e)
                        {



                       
                    }
                  
                    }
                   

                    //// http://www.codeproject.com/Tips/900200/SFTP-File-Upload-Using-ASP-NET-Web-API-and-Angular

                }
            
            return BadRequest();

        }


        /// <summary>
        /// Action for uploading pictures on cloudinary
        /// </summary>
        /// <param name="id">id of coupon</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public IHttpActionResult UploadFile(int id)
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var coupon = db.Coupons.Find(id);  // if tying to upload on full gallery
                if (coupon.Images.Count() >= 6)
                {
                    return BadRequest();
                }

                var httpPostedFile = HttpContext.Current.Request.Files["file"];
                bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath("~/UploadedDocuments"));
                if (!folderExists)
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/UploadedDocuments"));
                var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedDocuments"),
                                                httpPostedFile.FileName);
                httpPostedFile.SaveAs(fileSavePath);

                if (File.Exists(fileSavePath))
                {
                    Account account = new Account("dvnnqotru", "252251816985341", "eqxRrtlVyiWA5-WCil_idtLzP6c");
                    Cloudinary cloudinary = new Cloudinary(account);

                    var uploadParams = new ImageUploadParams()  //upload to cloudinary
                    {
                        File = new FileDescription(fileSavePath),
                        Transformation = new Transformation().Crop("fill").Width(720).Height(480)

                    };

                    var uploadResult = new ImageUploadResult();

                    try
                    {

                        uploadResult = cloudinary.Upload(uploadParams);
                    }
                    catch
                    {

                        return StatusCode(HttpStatusCode.ExpectationFailed);
                    }
                    System.IO.File.Delete(fileSavePath);

                    if(uploadResult.Uri == null)
                    {
                      return StatusCode(HttpStatusCode.ExpectationFailed);
                    
                    }

                    if (coupon.PictureUrl == "/Images/placeholder.png")         // testing if picture is main picture
                    {
                        coupon.PictureUrl = "http://res.cloudinary.com" + uploadResult.Uri.AbsolutePath;
                        db.Entry(coupon).State = EntityState.Modified;
                        db.SaveChanges();
                        return Ok(coupon.PictureUrl);
                    }
                    else // else is gallery 
                    {
                        var image = new Image()
                        {
                            ImageUrl = "http://res.cloudinary.com" + uploadResult.Uri.AbsolutePath,
                            CouponId = id
                        };
                        db.Images.Add(image);
                        db.SaveChanges();
                        return Ok(image);
                    }

                    //// http://www.codeproject.com/Tips/900200/SFTP-File-Upload-Using-ASP-NET-Web-API-and-Angular

                }
            }
            return BadRequest();
        }


        /// <summary>
        /// Action for delete images of selected coupon
        /// </summary>
        /// <param name="id">id of coupon</param>
        /// <returns></returns>
        [ResponseType(typeof(Image))]
        public IHttpActionResult DeleteImage(int id)
        {
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return NotFound();
            }

            db.Images.Remove(image);
            db.SaveChanges();

            return Ok(image);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ImageExists(int id)
        {
            return db.Images.Count(e => e.Id == id) > 0;
        }
        
    }
}