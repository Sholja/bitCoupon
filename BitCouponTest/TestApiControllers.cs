//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using BitCoupon.API.Controllers;
//using System.Web.Mvc;
//using BitCoupon.DAL.Models;


//namespace BitCouponTest								   
//{
//	[TestClass]
//	public class TestApiControllers
//	{
//		[TestMethod]
//		public void TestAddCategoryGet()
//		{
//			CategoriesController cc = new CategoriesController();

//			var result=cc.AddCategory();

//			Assert.IsNotNull(result, "doesnt return value");
//			Assert.IsInstanceOfType(result, typeof(ViewResult));
//		}
//		//[TestMethod]
//		//public void TestCouponControllerIndexValueNull()
//		//{
//		//	CouponsController cc = new CouponsController();

//		//	var result = cc.Index(null);

//		//	Assert.IsNotNull(result, "doesnt return value");
//		//	Assert.IsInstanceOfType(result, typeof(ViewResult));
//		//}
//		//[TestMethod]
//		//public void TestCouponControllerIndexValue1()
//		//{
//		//	CouponsController cc = new CouponsController();

//		//	var result = cc.Index(1);

//		//	Assert.IsNotNull(result, "doesnt return value");
//		//	Assert.IsInstanceOfType(result, typeof(PartialViewResult));
//		//}
//		//[TestMethod]
//		//public void TestCouponControllerIndexValue9999()
//		//{
//		//	CouponsController cc = new CouponsController();

//		//	var result = cc.Index(9999);

//		//	Assert.IsNotNull(result, "doesnt return value");
//		//	Assert.IsInstanceOfType(result, typeof(PartialViewResult));
//		//}
//		//[TestMethod]
//		//public void TestCouponControllerDetailsValueNull()
//		//{
//		//	CouponsController cc = new CouponsController();

//		//	var result = cc.Details(null);

//		//	Assert.IsNotNull(result, "doesnt return value");
//		//	Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
//		//}
		
//		//[TestMethod]
//		//public void TestCouponControllerDetailsValue9999()
//		//{
//		//	CouponsController cc = new CouponsController();

//		//	var result = cc.Details(9999);

//		//	Assert.IsNotNull(result, "doesnt return value");
//		//	Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
//		//}
//		[TestMethod]
//		public void TestCouponControllerSearchByCategoryValueNull()
//		{
//			CouponsController cc = new CouponsController();

//			var result = cc.SearchByCategory(null);

//			Assert.IsNotNull(result, "doesnt return value");
//			Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
//		}
//		//[TestMethod]
//		//public void TestCouponControllerSearchByCategoryValue1()
//		//{
//		//	CouponsController cc = new CouponsController();

//		//	var result = cc.SearchByCategory(1);

//		//	Assert.IsNotNull(result, "doesnt return value");
//		//	Assert.IsInstanceOfType(result, typeof(ViewResult));
//		//}
//		//[TestMethod]
//		//public void TestCouponControllerSearchByCategoryValue9999()
//		//{
//		//	CouponsController cc = new CouponsController();

//		//	var result = cc.SearchByCategory(9999);

//		//	Assert.IsNotNull(result, "doesnt return value");
//		//	Assert.IsInstanceOfType(result, typeof(ViewResult));
//		//}
//		[TestMethod]
//		public void TestCouponControllerSearchValuesIdNull()
//		{
//			CouponsController cc = new CouponsController();

//			//var result = cc.Search("SomeName","1",null);

//			//Assert.IsNotNull(result, "doesnt return value");
//			//Assert.IsInstanceOfType(result, typeof(PartialViewResult));
//		}
//		[TestMethod]
//		public void TestCouponControllerSearchValuesId1()
//		{
//			CouponsController cc = new CouponsController();

//			//var result = cc.Search("SomeName", "1", 1);

//			//Assert.IsNotNull(result, "doesnt return value");
//			//Assert.IsInstanceOfType(result, typeof(PartialViewResult));
//		}

		
		
//		//[TestMethod]
//		//public void TestAddCategoryPost()
//		//{
//		//	CategoriesController cc = new CategoriesController();

//		//	Category category=new Category(){
//		//		Name="TestCategory"
//		//	};

//		//	var result = cc.AddCategory(category);

//		//	Assert.IsNotNull(result, "Not saving category");
//		//	Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

//		//	category = new Category();
//		//	result = cc.AddCategory(category);
			
//		//	Assert.IsInstanceOfType(result, typeof(ViewResult));			
//		//}






        







        





//	}
//}
