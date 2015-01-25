using System.Collections.Generic;
using System.Web.Mvc;
using WebTorrent.Domain.Exceptions;

namespace WebTorrent.WebApp.Controllers.Common
{
    public class ExceptionControllerActionInvoker : ControllerActionInvoker
    {
        protected override ActionResult InvokeActionMethod(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters)
        {
            try
            {
                return base.InvokeActionMethod(controllerContext, actionDescriptor, parameters);
            }
            catch (ValidationException exception)
            {
                return new JsonResult
                {
                    Data = new
                    {
                        exception.ValidationSummary,
                        exception.Errors,
                        HasErrors = true
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }
}