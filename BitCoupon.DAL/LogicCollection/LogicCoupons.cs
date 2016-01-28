using BitCoupon.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.Entity;
using System.Web.Mvc;


namespace BitCoupon.DAL.LogicCollection
{
	public static class LogicCoupons
	{

		
		
		/// <summary>
		/// Checking for coupons existance
		/// </summary>
		/// <param name="id">id of searched coupon</param>
		/// <param name="coupon">does coupon exists in db under that id</param>
		/// <returns>HttpStatusCode.OK if searched id is in database 
		/// otherwise BadRequest for id==null 
		/// and NotFound if that instance doesnt exist in dataBase
		/// </returns>
		public static HttpStatusCode CouponsControllerLogic(Coupon coupon)
		{
			if (coupon == null)
			{
				return HttpStatusCode.NotFound;
			} else
			{
				return HttpStatusCode.OK;
			}
		}
		/// <summary>
		/// gets from base all coupon names which start with sent string
		/// </summary>
		/// 
		/// <param name="letters">
		/// receives a string with which it 
		/// searches names of ooupons and puts it into a list
		/// </param>
		/// 
		/// <returns>
		/// list of strings containing names of coupons 
		/// which beggins with sent string
		/// </returns>
		public static List<string> CouponsControllerApiLogic(string letters)
		{

			ApplicationDbContext db = new ApplicationDbContext();
			List<Coupon> FromBaseCoupons = db.Coupons.Where(x => x.Name.StartsWith(letters)).ToList();
			List<string> CouponNamesListWhereNameStartWithReceivedString = new List<string>();
			foreach (var item in FromBaseCoupons)
			{
				if (item.Name.StartsWith(letters))
				{
					CouponNamesListWhereNameStartWithReceivedString.Add(item.Name);
				}
			}
			return CouponNamesListWhereNameStartWithReceivedString;


		}

		public static HttpStatusCodeResult CouponBuyingLogic(Coupon coupon,System.Web.Mvc.ModelStateDictionary modelState,string SessionId)
		{
			ApplicationDbContext db = new ApplicationDbContext();

			if (modelState.IsValid)
			{
				coupon.TotalNumberOfCoupons -= coupon.RequiredNumberOfCoupons;
				coupon.Purchase++;

				Buy buy = new Buy() { CouponId = coupon.CouponId, ApplicationUserId = SessionId };
				db.Buys.Add(buy);

                if (coupon.NuberOfCouponsToOfferManaged>0)
                coupon.NuberOfCouponsToOfferManaged -= coupon.RequiredNumberOfCoupons;

				db.Entry(coupon).State = EntityState.Modified;
				db.SaveChanges();

				if (coupon.TotalNumberOfCoupons > 0)
				{
					return new HttpStatusCodeResult(HttpStatusCode.OK);
				}

				coupon.Acitve = false;
				db.Entry(coupon).State = EntityState.Modified;
				db.SaveChanges();

				return new HttpStatusCodeResult(HttpStatusCode.OK);
			}
			return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		}
		/// <summary>
		/// method for geting cupon with some id
		/// </summary>
		/// <param name="id">nullable id</param>
		/// <returns>returns coupon if exists otherwise it returns null</returns>
		public static Coupon GetCoupon(int? id)
		{
			ApplicationDbContext db = new ApplicationDbContext();

			if (LogicId.LogicCheckId(id) != HttpStatusCode.OK)
			{
				return null;
			}
			Coupon coupon = db.Coupons.Find(id);
			return coupon;

		}
		/// <summary>
		/// method for getting list of coupons by category from database
		/// </summary>
		/// <param name="id">id of category</param>
		/// <returns>list of coupons that belongs to categorie's sent id</returns>
		public static List<Coupon> GetCouponListByCategoryId(int? id)
		{
			ApplicationDbContext db = new ApplicationDbContext();

			List<Coupon> coupons = db.Coupons.Where(x => x.CategoryId == id && x.Category.Approved == true && x.IsDeleted == false && x.IsEdited==false && x.Acitve == true).OrderBy(x => x.Name).ToList();

			return coupons;
		}


        /// <summary>
        /// generates unique string verificationId for each Cart
        /// </summary>
        /// <returns> unique string id </returns>
        public static string GenerateVerificationId(List<BitCoupon.DAL.Models.Item> items)
        {
            Guid g = Guid.NewGuid();
            string varificationId = Convert.ToBase64String(g.ToByteArray());
            varificationId = varificationId.Replace("=", "");
            varificationId = varificationId.Replace("+", "");

            if (items.Count != 0) // goes trough carts and checks if verification id is unique  
            {
                for (int i = 0; i < items.Count; i++)
                {

                    if (items[i].VerificationId == varificationId)
                    {
                        varificationId = GenerateVerificationId(items);

                    }
                }
            }

            return varificationId;
        }

    }
}
