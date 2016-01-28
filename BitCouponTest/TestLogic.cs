using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BitCoupon.DAL.LogicCollection;
using BitCoupon.DAL.Models;
using System.Net;
using BitCoupon.MVC.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Web.Helpers;




namespace BitCouponTest
{
	[TestClass]
	public class LogicCollectionTests
	{

		
		
		[TestMethod]
		public void IdTestLogic()
		{
			var result = LogicId.LogicCheckId(1);

			Assert.IsNotNull(result, "not sending anything");
			Assert.IsInstanceOfType(result, typeof(HttpStatusCode));
			Assert.AreEqual(HttpStatusCode.OK, result);
			

		}
		
		
		//[TestMethod]
		
		//public void CategoriesControllerLogicTest()
		//{
		//	Category category = new Category()
		//	{
		//		Name = "testCategory",
		//		Id = 5,
		//		Approved = true
		//	};

		//	var result = LogicCoupons.CategoriesControllerLogic(category);


		//	Assert.IsNotNull(result, "not sending anything");
		//	Assert.IsInstanceOfType(result, typeof(HttpStatusCode));
		//	Assert.AreEqual(HttpStatusCode.OK, result);

		//}
		
		
		//[TestMethod]
		//public void CouponsControllerLogicTest()
		//{
		//	Coupon coupon = new Coupon()
		//	{
		//		CategoryId = 1,
		//		Name = "TestCoupon",
		//		Price = 1000,
		//		Discount = 20,
		//		DescriptionOnCoupon = "TestDescriptionForCouponMin30Max100",
		//		DescriptionOnSellerPage = "TestDescriptionForCouponOnCouponDetailsMin100Max500"
		//		+ "...TestDescriptionForCouponOnCouponDetailsMin100Max500...",
		//		ExpirationTime = new DateTime(2015, 12, 12),
		//		RequiredNumberOfCoupons = 1,
		//		TotalNumberOfCoupons = 1,
		//		SellerUrl = "someUrl",
		//		PictureUrl = "http://www.jpl.nasa.gov/spaceimages/images/mediumsize/PIA17011_ip.jpg",
		//		Category = new Category()
		//		{
		//			Approved = true,
		//			Id = 1,
		//			Name = "TestCategoryForCoupon"
		//		}
		//	};
		//	var result = LogicCoupons.CouponsControllerLogic(coupon);
		//	Assert.IsNotNull(result, "doesnt return values");
		//	Assert.IsInstanceOfType(result, typeof(HttpStatusCode));
		//	Assert.AreEqual(HttpStatusCode.OK, result);
		//}
		
		
		////[TestMethod]
		////public void TestCategoriesControlerMVCIndex()
		////{
		////	CategoriesController cc = new CategoriesController();

		////	var result = cc.Index();

		////	Assert.IsNotNull(result, "not returning value");
		////	Assert.IsInstanceOfType(result, typeof(ViewResult));
		////}

		//[TestMethod]
		//public void TestCategoriesControlerMVCCreate()
		//{
		//	CategoriesController cc = new CategoriesController();

		//	var result = cc.Create();

		//	Assert.IsNotNull(result, "not returning value");
		//}

		
		[TestMethod]//Delete Confirmed
		public void TestCategoriesControllerMVCDeleteConfirmed()
		{
			CategoriesController cc = new CategoriesController();
			var result = cc.DeleteConfirmed(1);

			Assert.IsNotNull(result, "doesnt return values");
			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
		}

		
		//[TestMethod]//Aprove Get
		//public void TestCategoriesControllerApprove()
		//{
		//	CategoriesController cc = new CategoriesController();

		//	var result = cc.Approve();

		//	Assert.IsNotNull(result, "doesnt return values");
		//	Assert.IsInstanceOfType(result, typeof(ViewResult));
		//}

		
		[TestMethod]
		public void TestCategoriesContollerApprovePost()
		{
			List<Category> categories=new List<Category>();
			CategoriesController cc = new CategoriesController();
			var result = cc.Approve(categories);

			Assert.IsNotInstanceOfType(result, typeof(ViewResult));
		}
		//[TestMethod]
		//public void TestCategoriesControllerUnique()
		//{
		//	CategoriesController cc=new CategoriesController();
		//	JsonResult result = cc.Unique("Other");
		//	Assert.IsInstanceOfType(result,typeof(JsonResult));
		//}
		//public void TestCategoriesControllerDispose()
		//{
		//	CategoriesController cc = new CategoriesController();
			
		//}
  //      [TestMethod]
		//public void TestCategoriesControllerApproveGet()
		//{
		//	CategoriesController cc = new CategoriesController();

		//	var result = cc.Approve();

		//	Assert.IsNotNull(result);
		//	Assert.IsInstanceOfType(result, typeof(List<Category>));
		//}
    



	}
}
