using System.Web.Optimization;

namespace WebTorrent.WebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif

            var globalMandatoryStyles = new[]
                          {
                              "~/Metronic/global/plugins/font-awesome/css/font-awesome.min.css",
                              "~/Metronic/global/plugins/simple-line-icons/simple-line-icons.min.css",
                              "~/Metronic/global/plugins/bootstrap/css/bootstrap.min.css",
                              "~/Metronic/global/plugins/uniform/css/uniform.default.css",
                              "~/Metronic/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                              "~/Metronic/global/css/components.css",
                              "~/Metronic/admin/layout/css/layout.css",
                              "~/Metronic/admin/layout/css/themes/light2.css",
                              "~/Metronic/admin/layout/css/custom.css",
                              "~/Styles/common/common.css",
                          };

            //PAGE LEVEL PLUGIN STYLES
            var pageLevelPluginStyles = new[]
                          {
                              "~/Metronic/global/plugins/gritter/css/jquery.gritter.css",
                              "~/Metronic/global/plugins/bootstrap-daterangepicker/daterangepicker-bs3.css",
                              "~/Metronic/global/plugins/fullcalendar/fullcalendar/fullcalendar.css",
                              "~/Metronic/global/plugins/jqvmap/jqvmap/jqvmap.css",
                              "~/Metronic/global/plugins/jquery-ui/jquery-ui-1.10.3.custom.min.css",
                              "~/Metronic/global/plugins/select2/select2.css",
                              "~/Metronic/global/css/plugins.css",
                              "~/Metronic/admin/pages/css/tasks.css"
                          };

            //THEME STYLES
            var themeStyles = new[]
                          {
                              "~/Metronic/global/css/components.css",
                              "~/Metronic/global/css/plugins.css",
                              "~/Metronic/global/css/plugins.css",
                              "~/Metronic/admin/layout/css/layout.css",
                              "~/Metronic/admin/layout/css/themes/default.css",
                              "~/Metronic/admin/layout/css/custom.css"
                          };

            bundles.Add(new StyleBundle("~/css") { Orderer = new AsIsBundleOrderer() }
               .IncludeRewriteCssStyles(globalMandatoryStyles)
               );

            bundles.Add(new StyleBundle("~/css-login") { Orderer = new AsIsBundleOrderer() }
               .IncludeRewriteCssStyles(globalMandatoryStyles)
               .IncludeRewriteCssStyles(pageLevelPluginStyles)
               .IncludeRewriteCssStyles(themeStyles)
               .IncludeRewriteCssStyles(new[]
                                         {
                                             "~/Metronic/global/plugins/select2/select2.css",
                                             "~/Metronic/admin/pages/css/login.css"
                                         })
//               .IncludeAction(applicationStyles)
               );

        }
    }
}
