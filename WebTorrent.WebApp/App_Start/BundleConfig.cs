using System;
using System.Collections.Generic;
using System.Web.Optimization;

namespace WebTorrent.WebApp.App_Start
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
                              "~/Styles/Metronic/global/plugins/font-awesome/css/font-awesome.min.css",
                              "~/Styles/Metronic/global/plugins/simple-line-icons/simple-line-icons.min.css",
                              "~/Styles/Metronic/global/plugins/bootstrap/css/bootstrap.min.css",
                              "~/Styles/Metronic/global/plugins/uniform/css/uniform.default.css",
                              "~/Styles/Metronic/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                              "~/Styles/Metronic/global/css/components.css",
                              "~/Styles/Metronic/admin/layout/css/layout.css",
                              "~/Styles/Metronic/admin/layout/css/themes/default.css",
                              "~/Styles/Metronic/admin/layout/css/custom.css",

                             
                          };

            bundles.Add(new StyleBundle("~/css") { Orderer = new AsIsBundleOrderer() }
               .IncludeRewriteCssStyles(globalMandatoryStyles)
               );

        }
    }


    public class AsIsBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }

    public static class BundlesExtensions
    {
        public static Bundle IncludeRewriteCss(this Bundle bundle, string path)
        {
            return bundle.Include(path, new CssRewriteUrlTransform());
        }

        public static Bundle IncludeRewriteCssStyles(this Bundle bundle, params string[] paths)
        {
            foreach (var path in paths)
                bundle.Include(path, new CssRewriteUrlTransform());

            return bundle;
        }

        public static Bundle IncludeAction(this Bundle bundle, Func<Bundle, Bundle> func, params IItemTransform[] transforms)
        {
            return func(bundle);
        }
    }
}
