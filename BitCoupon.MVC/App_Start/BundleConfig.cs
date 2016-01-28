using System.Web;
using System.Web.Optimization;

namespace BitCoupon.MVC
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/respond.js",
                      "~/Scripts/bootbox.js"
                      ));


            bundles.Add(new ScriptBundle("~/bundles/adminpanel").Include(
                "~/Scripts/adminpanel/counter.js",
                 "~/Scripts/adminpanel/excanvas.js",
                 "~/Scripts/adminpanel/fullcalendar.min.js",
                 "~/Scripts/adminpanel/jquery-migrate-1.0.0.min.js",
                 "~/Scripts/adminpanel/jquery-ui-1.10.0.custom.min.js",
                 "~/Scripts/adminpanel/jquery.chosen.min.js",
                 "~/Scripts/adminpanel/jquery.cleditor.min.js",
                 "~/Scripts/adminpanel/jquery.cookie.js",
                 "~/Scripts/adminpanel/jquery.dataTables.min.js",
                 "~/Scripts/adminpanel/jquery.elfinder.min.js",
                 "~/Scripts/adminpanel/jquery.flot.js",
                 "~/Scripts/adminpanel/jquery.flot.pie.js",
                 "~/Scripts/adminpanel/jquery.flot.resize.min.js",
                 "~/Scripts/adminpanel/jquery.flot.stack.js",
                 "~/Scripts/adminpanel/jquery.gritter.min.js",
                 "~/Scripts/adminpanel/jquery.imagesloaded.js",
                 "~/Scripts/adminpanel/jquery.iphone.toggle.js",
                 "~/Scripts/adminpanel/jquery.knob.modified.js",
                 "~/Scripts/adminpanel/jquery.masonry.min.js",
                 "~/Scripts/adminpanel/jquery.noty.js",
                 "~/Scripts/adminpanel/jquery.raty.min.js",
                 "~/Scripts/adminpanel/jquery.sparkline.min.js",
                 "~/Scripts/adminpanel/jquery.ui.touch-punch.js",
                 "~/Scripts/adminpanel/jquery.uniform.min.js",
                 "~/Scripts/adminpanel/jquery.uploadify-3.1.min.js",
                 "~/Scripts/adminpanel/modernizr.js",
                 "~/Scripts/adminpanel/retina.js",
                   "~/Scripts/adminpanel/custom.js"
                ));

            bundles.Add(new StyleBundle("~/Content/adminpanel").Include(
                "~/Content/adminpanel/chosen.css",
                "~/Content/adminpanel/elfinder.min.css",
                "~/Content/adminpanel/elfinder.theme.css",
                "~/Content/adminpanel/fullcalendar.css",
                "~/Content/adminpanel/glyphicons.css",
                "~/Content/adminpanel/halflings.css",
                "~/Content/adminpanel/ie.css",
                "~/Content/adminpanel/ie9.css",
                "~/Content/adminpanel/jquery-ui-1.8.21.custom.css",
                "~/Content/adminpanel/jquery.cleditor.css",
                "~/Content/adminpanel/jquery.gritter.css",
                "~/Content/adminpanel/jquery.iphone.toggle.css",
                "~/Content/adminpanel/jquery.noty.css",
                "~/Content/adminpanel/noty_theme_default.css",
                "~/Content/adminpanel/Site.css",
                "~/Content/adminpanel/style-forms.css",
                "~/Content/adminpanel/style-responsive.css",
                "~/Content/adminpanel/style.css",
                "~/Content/adminpanel/styles.css",
                "~/Content/adminpanel/uniform.default.css",
                "~/Content/adminpanel/uploadify.css"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
                      
		}
	}
}
