using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BitCoupon.DAL.LogicCollection
{
	public static class LogicId
	{
		/// <summary>
		/// Checks wheather passed value is null or not
		/// </summary>
		/// <param name="Id">value of id</param>
		/// <returns>statusCode badRequest for null or less than 1 id and ok for value</returns>
		public static HttpStatusCode LogicCheckId(int? Id)
		{
			if (Id == null || Id < 1)
			{
				return HttpStatusCode.BadRequest;
			} else
			{
				return HttpStatusCode.OK;
			}
		}
	}
}
