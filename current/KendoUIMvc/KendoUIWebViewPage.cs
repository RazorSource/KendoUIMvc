using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KendoUIMvc
{
    public abstract class KendoUIWebViewPage<TModel> : WebViewPage<TModel>
    {
        private KendoUIHtmlHelper<TModel> kendoUIHtmlHelper;

        public KendoUIHtmlHelper<TModel> UI
        {
            get
            {
                if (this.kendoUIHtmlHelper == null)
                {
                    this.kendoUIHtmlHelper = new KendoUIHtmlHelper<TModel>(Model, Html, Ajax);
                }

                return this.kendoUIHtmlHelper;
            }
        }
    }

    //public abstract class KendoUIWebViewPage : KendoUIWebViewPage<dynamic>
    //{
    //}
}
