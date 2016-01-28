using BitCoupon.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BitCoupon.DAL.LogicCollection
{
	public static class LogicCategories
	{
		/// <summary>
		/// Checking for category existance
		/// </summary>
		/// <param name="id">id of searched category</param>
		/// <param name="category">does category exists in db under that id</param>
		/// <returns>HttpStatusCode.OK if searched id is in database 
		/// otherwise BadRequest for id==null 
		/// and NotFound if that instance doesnt exist in dataBase
		/// </returns>
		public static Category CategoriesControllerLogic(int? Id)
		{
			ApplicationDbContext db = new ApplicationDbContext();
			if (LogicId.LogicCheckId(Id) != HttpStatusCode.OK)
			{
				return null;
			}
			Category category = db.Categories.Find(Id);
			if (category == null)
			{
				return null;
			} else
			{
				return category;
			}
		}
	}
}
