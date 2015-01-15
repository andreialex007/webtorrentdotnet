using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using WebTorrent.WebApp.Code;

namespace WebTorrent.WebApp
{
    public class WebApiApplication : HttpApplication
    {
        private const string StringMultipartMediaType = "multipart/form-data";
        private const string StringApplicationMediaType = "application/octet-stream";

        protected void Application_Start()
        {
            //            new MultipartFormFormatter().SupportedMediaTypes




            var mediaTypeFormatter = GlobalConfiguration.Configuration.Formatters.Single(x => x.GetType() == typeof(FormUrlEncodedMediaTypeFormatter));
            mediaTypeFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue(StringMultipartMediaType));
            mediaTypeFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue(StringApplicationMediaType));



            //            ModelBinders

            //            ModelBinders.Binders.DefaultBinder = new CustomModelBinder();

            //            GlobalConfiguration.Configuration.Formatters.Add(new MultipartFormFormatter());
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            IocConfig.Bootstrap();
        }

        public override void Init()
        {
            this.PostAuthenticateRequest += MvcApplicationPostAuthenticateRequest;
            base.Init();
        }

        private static void MvcApplicationPostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }
    }
}