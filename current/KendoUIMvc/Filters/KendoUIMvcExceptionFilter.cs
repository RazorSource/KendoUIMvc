using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using KendoUIMvc.Models;

namespace KendoUIMvc.Filters
{
    public class KendoUIMvcExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext exceptionContext)
        {
            // Flag the exception as handled.
            exceptionContext.ExceptionHandled = true;
            exceptionContext.HttpContext.Response.TrySkipIisCustomErrors = true;

            bool isAjaxRequest = exceptionContext.RequestContext.HttpContext.Request.IsAjaxRequest();

            if (isAjaxRequest)
            {
                HandleAjaxException(exceptionContext.HttpContext.Response, exceptionContext.Exception);
            }

            base.OnException(exceptionContext);
        }

        protected void HandleAjaxException(HttpResponseBase response, Exception ex)
        {
            response.Clear();
            response.ContentEncoding = Encoding.UTF8;
            response.HeaderEncoding = Encoding.UTF8;
            response.TrySkipIisCustomErrors = true;
            response.StatusCode = 400;

            //KendoErrorCollection errors = new KendoErrorCollection(new KendoError(ex.Message));

            response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new KendoError(ex.Message)));
        }
    }
}
