using System.Collections.Generic;
using System.Web.Optimization;

namespace WebTorrent.WebApp
{
    public class AsIsBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}