using System;
using System.IO;
using System.Web;

// ReSharper disable AssignNullToNotNullAttribute

namespace WebTorrent.WebApp.Code.Extensions
{
    public static class Utils
    {
        public static string MapPathReverse(string fullServerPath)
        {
            return string.Format(@"~\{0}", fullServerPath.Replace(HttpContext.Current.Request.PhysicalApplicationPath, String.Empty));
        }

        public static byte[] ToByteArray(this HttpPostedFileBase file)
        {
            var target = new MemoryStream();
            file.InputStream.CopyTo(target);
            return target.ToArray();
        }

        public static byte[] ToByteArray(this HttpPostedFile file)
        {
            var target = new MemoryStream();
            file.InputStream.CopyTo(target);
            return target.ToArray();
        }

        public static HttpPostedFile PostedFile(this HttpRequest request)
        {
            return request.Files[0];
        }

        public static bool IsJsonRequest(this HttpContext contextBase)
        {
            return contextBase.Request.ContentType.IndexOf("application/json", StringComparison.OrdinalIgnoreCase) != -1;
        }
    }
}