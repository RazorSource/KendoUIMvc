using KendoUIMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using KendoUIMvc.Util;

namespace KendoUIMvc.Controls
{
    public class HtmlForm<TModel> : FormBase<TModel, HtmlForm<TModel>>
    {
        public HtmlForm(HtmlHelper<TModel> htmlHelper, string formId, string actionName, string controllerName)
            : base(htmlHelper, formId, actionName, controllerName)
        {            
        }

        protected override MvcForm RenderBeginForm(IDictionary<string, object> layoutAttributes)
        {
            return this.htmlHelper.BeginForm(actionName, controllerName, FormMethod.Post, layoutAttributes);
        }
    }
}
