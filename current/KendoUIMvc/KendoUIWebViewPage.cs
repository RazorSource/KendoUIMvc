using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CommonMvc.Razor;

namespace KendoUIMvc
{
    public abstract class KendoUIWebViewPage<TModel> : WebViewPage<TModel>
    {
        private IRazorHtmlHelper<TModel> razorHtmlHelper;

        public IRazorHtmlHelper<TModel> UI
        {
            get
            {
                if (this.razorHtmlHelper == null)
                {
                    this.razorHtmlHelper = new KendoUIHtmlHelper<TModel>(Model, Html, Ajax);
                }

                return this.razorHtmlHelper;
            }
        }
    }
}
