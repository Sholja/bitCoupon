using BitCoupon.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BitCoupon.DAL.LogicCollection
{
	public static class LogicComments
	{
		
		
		
		
		
		

		/// <summary>
		/// logic of comments controller in .api
		/// </summary>
		/// <param name="id">nullable id of coupon</param>
		/// <returns>comment item from database</returns>
		public static Comment CommentsControllerLogicApi(int? id)
		{
		  	ApplicationDbContext db=new ApplicationDbContext();
			if(LogicId.LogicCheckId(id)!=HttpStatusCode.OK)
			{
				return null;
			} 
			Comment comment=db.Comments.Find(id);
			if(comment==null)
			{
				return null;
			}
			return comment;
		}
		
		
		
	}
}
