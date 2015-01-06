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
