using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BitCoupon.MVC;
using BitCoupon.MVC.Controllers;
using System.Web.Mvc;
using BitCoupon.DAL.Models;
using Moq;

namespace BitCouponTest
{
	[TestClass]
	public class TestMVCControllers
	{
		
		[TestMethod]
		public void TestDetailsValueNull()
		{
			CategoriesController cc = new CategoriesController();

			var result = cc.Details(null);

			Assert.IsNotNull(result, "Returns null");
			Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
		}
	
		[TestMethod]
		public void TestCreate()
		{
			CategoriesController cc = new CategoriesController();

			var result = cc.Create();

			Assert.IsNotNull(result,"Returns Null");
			Assert.IsInstanceOfType(result,typeof(ViewResult));
		}
		//[TestMethod]
		//public void TestCreate()
		//{

		//}
		//[TestMethod]
		//public void TestEditValue1() //Get
		//{
		//	CategoriesController cc = new CategoriesController();

		//	var result = cc.Edit(1);

		//	Assert.IsNotNull(result, "Returns null");
		//	Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
		//}
		
		//[TestMethod]
		//public void TestEditValue9999() //Get
		//{
		//	CategoriesController cc = new CategoriesController();

		//	var result = cc.Edit(9999);

		//	Assert.IsNotNull(result, "Returns null");
		//	Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
		//}
		[TestMethod]
		public void TestEditValuenull() //Get
		{
			CategoriesController cc = new CategoriesController();

			int? testNull = null;
			var result = cc.Edit(testNull);

			Assert.IsNotNull(result, "Returns null");
			Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
		}
		//[TestMethod]
		//public void TestEditValueCategory1true() //Post
		//{
		//	ApplicationDbContext db = new ApplicationDbContext();
		//	CategoriesController cc = new CategoriesController();

		//	//Category category=new Category(){
		//	//	Name=db.Categories.Find(3).Name,
		//	//	Approved=true,
		//	//	Id=3
		//	//};
		//	Category category = db.Categories.Find(1);
		//	category.Approved = true;
		//	category.Id = 3;
		//	category.Name = db.Categories.Find(3).Name;
			
		//	var result = cc.Edit(category);

		//	Assert.IsNotNull(result, "Returns null");
		//	Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
		//}
		//[TestMethod]
		//public void TestEditValueCategory9999true() //Post
		//{
		//	CategoriesController cc = new CategoriesController();

		//	Category category=new Category(){
		//		Name="TestCategoryEditPost",
		//		Approved=true,
		//		Id=9999
		//	};
		//	var result = cc.Edit(category);

		//	Assert.IsNotNull(result, "Returns null");
		//	Assert.IsInstanceOfType(result, typeof(ViewResult));
		//}
		//[TestMethod,OverrideAuthentication]
		//public void TestEditValueCategorynulltrue()//Post
		//{
			
		//	CategoriesController cc = new CategoriesController();

		//	Category category = new Category();
			

		//	var result = cc.Edit(category);

		//	Assert.IsNotNull(result, "Returns null");
		//	Assert.IsInstanceOfType(result, typeof(ViewResult));
		//}
		//[TestMethod,OverrideAuthentication]
		//public void TestEditWithValidation()
		//{
		//	CategoriesController cc=new CategoriesController();
		//	var controller = cc.Approve();
		//	controller.SetFakeAuthenticatedControllerContext("Seller");
		//}
		

		
	}
}
