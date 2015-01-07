using System;
using System.Web.Optimization;

namespace WebTorrent.WebApp
{
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