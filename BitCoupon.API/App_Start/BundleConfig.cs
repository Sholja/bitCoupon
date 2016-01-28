using System.Web;
using System.Web.Optimization;

namespace BitCoupon.API
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                       "~/Scripts/bootbox.js",
                       "~/Scripts/jquery.tabSlideOut.v1.3.js",
                       "~/Scripts/jquery-ui.js",
                       "~/Scripts/jquery.js",
                       "~/Scripts/feedback.js",
                       "~/Scripts/main.js",
                       "~/Scripts/jquery.dataTables.min.js",
                       "~/Scripts/modernizr.custom.js"
                     
                       ));
            bundles.Add(new ScriptBundle("~/bundles/theme").Include(
                       "~/Scripts/main.js",
                       "~/Scripts/plugins.js"

                       ));
            bundles.Add(new ScriptBundle("~/bundles/flow").Include(
                "~/Scripts/flow-upload/ng-flow-standalone.min.js",
                 "~/Scripts/flow-upload/ng-flow.min.js"
             

                 ));

            //nova tema 
            bundles.Add(new ScriptBundle("~/theme/scripts").Include(
                  "~/Scripts/siteTemplate/jquery.animsition.min.js",
                  "~/Scripts/siteTemplate/owl.carousel.min.js",
                  "~/Scripts/siteTemplate/jquery.flexslider-min.js",
                  "~/Scripts/siteTemplate/plugins.js",
                  "~/Scripts/siteTemplate/kupon.js"
                 ));
            bundles.Add(new StyleBundle("~/theme/css").Include(
                     "~/Content/siteTemplate/themify-icons.css",
                     "~/Content/siteTemplate/font-awesome.css",
                       "~/Content/siteTemplate/owl.carousel.css",
                   "~/Content/siteTemplate/animate.min.css",
                     "~/Content/siteTemplate/animsition.css",
                     "~/Content/siteTemplate/plugins.min.css",
                    "~/Content/siteTemplate/style.css"
                   ));
            // kraj nove teme



            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                       "~/Content/font-awesome.css",
                       "~/Content/templatemo_misc.css",
                       "~/Content/templatemo_style.css",
                       "~/Content/testimonails-slider.css",
                       "~/Content/tables.css",
                       "~/Content/Site.css",
                       "~/Content/jquery-ui.min.css",
                       "~/Content/jquery-ui.structure.min.css",
                       "~/Content/jquery-ui.theme.min.css",
                       "~/Content/angular-growl.min.css",
                       "~/Content/angular-carousel.min.css",
                       "~/Content/angular-motion.min.css",
                       "~/Content/bootstrap-additions.min.css",
                       "~/Content/component.css",
                       "~/Content/normalize.css",
                       "~/Content/demo.css"
                      ));


            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/angular/angular.js",
                      "~/Scripts/angular/angular-animate.js",
                      "~/Scripts/angular/angular-touch.min.js",
                      "~/Scripts/angular/angular-resource.min.js",
                      "~/Scripts/angular/ngCart.js",
                      "~/Scripts/angular/ngCart.directives.js",
                      "~/Scripts/angular/ngCart.fulfilment.js",
                      "~/Scripts/angular/angular-file-upload.js",
                      "~/Scripts/angular/angular-ui-router.min.js",
                      "~/Scripts/angular/angular-cookies.min.js",
                      "~/Scripts/jcs-auto-validate.min.js",
                      "~/Scripts/angular/angular-strap.js",
                      "~/Scripts/angular/angular-strap.tpl.js",
                      "~/Scripts/angular/spin.js",
                      "~/Scripts/angular/angular-spinner.js",
                      "~/Scripts/angular/angular-growl.min.js",
                      "~/Scripts/angular/directive.js",
                      "~/Scripts/angular-carousel.min.js",
                      "~/Scripts/angular/smart-table.min.js",
                       "~/Scripts/angular/lrInfiniteScrollPlugin.js",
                      "~/app/app.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/angular/coupons").Include(
                      "~/app/coupon/couponModule.js",
                      "~/app/coupon/mapModule.js",
                      "~/app/coupon/couponFactory.js",
                      "~/app/coupon/couponService.js",
                      "~/app/coupon/categories/categoriesFactory.js",
                      "~/app/coupon/couponList/controllers/couponListController.js",
                      "~/app/coupon/couponList/controllers/searchCouponsController.js",
                      "~/app/coupon/create/controllers/createCouponController.js",
                      "~/app/coupon/create/controllers/albumController.js",
                      "~/app/coupon/details/controllers/detailsController.js",
                      "~/app/coupon/categories/controllers/categoriesController.js",
                      "~/app/coupon/couponList/directives/couponDirective.js",
                      "~/app/coupon/couponList/directives/expiredCouponDirective.js",
                      "~/app/coupon/couponList/directives/searchCouponsDirective.js",
                      "~/app/coupon/couponList/directives/searchExpiredDirective.js",
                      "~/app/coupon/categories/categoriesMenu/controllers/categoriesMenuController.js",
                      "~/app/coupon/categories/categoriesMenu/directives/categoriesMenuDirective.js",
                      "~/app/coupon/delete/controllers/deleteController.js",
                      "~/app/coupon/edit/controllers/editCouponController.js",
                      "~/app/coupon/edit/controllers/editAlbumController.js",
                      "~/app/coupon/create/controllers/mapCreateController.js",
                      "~/app/coupon/details/controllers/mapDetailsController.js",
                      "~/app/coupon/edit/controllers/mapEditController.js",
                      "~/app/rating/rateDirective.js",
                       "~/Scripts/tinymce/angular-tiny-mce.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/angular/comments").Include(
                      "~/app/comment/commentModule.js",
                      "~/app/comment/commentFactory.js"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/angular/account").Include(
                     "~/app/account/accountModule.js",
                     "~/app/account/accountFactory.js",
                     "~/app/account/accountService.js",
                     "~/app/account/register/controllers/registerController.js",
                     "~/app/account/login/controllers/loginController.js",
                     "~/app/account/profile/controllers/buyerInfoController.js",
                     "~/app/account/profile/directives/buyerInfoDirective.js",
                     "~/app/account/profile/directives/couponUserDirective.js",
                     "~/app/account/profile/directives/boughtCouponDisplay.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/angular/feedback").Include(
                    "~/app/feedback/feedbackModule.js",
                    "~/app/feedback/feedbackFactory.js",
                    "~/app/feedback/feedbackService.js",
                    "~/app/feedback/controllers/feedbackController.js",
                    "~/app/feedback/directives/feedbackDirective.js"
                   ));

        }
    }
}
