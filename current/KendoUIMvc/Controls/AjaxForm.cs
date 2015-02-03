using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using CommonMvc.Razor.Controls;

namespace KendoUIMvc.Controls
{
    public class AjaxForm<TModel> : FormBase<TModel, IAjaxForm<TModel>>, IAjaxForm<TModel>
    {
        protected AjaxHelper<TModel> ajaxHelper;
        protected AjaxOptions ajaxOptions;
        protected RouteValueDictionary routeValues;

        public AjaxForm(HtmlHelper<TModel> htmlHelper, AjaxHelper<TModel> ajaxHelper, string formId, string actionName, string controllerName)
            : base(htmlHelper, formId, actionName, controllerName)
        {
            this.ajaxHelper = ajaxHelper;
            this.ajaxOptions = new AjaxOptions();
        }

        /// <summary>
        /// Sets AjaxOptions for the form.
        /// </summary>
        /// <param name="ajaxOptions">AjaxOptions instance.</param>
        /// <returns></returns>
        public IAjaxForm<TModel> SetAjaxOptions(AjaxOptions ajaxOptions)
        {
            this.ajaxOptions = ajaxOptions;
            return this;
        }

        /// <summary>
        /// Sets additional route values to use on the AJAX form.
        /// </summary>
        /// <param name="routeValues">Dictionary of route values.</param>
        /// <returns></returns>
        public IAjaxForm<TModel> SetRouteValues(RouteValueDictionary routeValues)
        {
            this.routeValues = routeValues;
            return this;
        }

        protected override MvcForm RenderBeginForm(IDictionary<string, object> layoutAttributes)
        {
            return this.ajaxHelper.BeginForm(this.actionName, this.controllerName, routeValues, ajaxOptions, layoutAttributes);
        }
    }
}