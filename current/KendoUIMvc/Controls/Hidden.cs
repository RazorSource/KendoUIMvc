using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CommonMvc.Razor.Controls;
using KendoUIMvc.Util;

namespace KendoUIMvc.Controls
{
    public class Hidden<TModel, TProperty> : IHidden<TModel, TProperty>
    {
        protected HtmlHelper<TModel> htmlHelper;
        protected Expression<Func<TModel, TProperty>> expression;
        protected string unboundName;
        protected object unboundValue;
        protected string controlId;

        public Hidden(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            this.htmlHelper = htmlHelper;
            this.expression = expression;
            this.controlId = MvcHtmlHelper.GetExpressionPropertyId(htmlHelper, expression);
        }

        public Hidden(HtmlHelper<TModel> htmlHelper, string name, object value = null)
        {
            this.htmlHelper = htmlHelper;
            this.unboundName = name;
            this.unboundValue = value;
            this.controlId = name;
        }

        /// <summary>
        /// Gets the HTML necessary to render the control.
        /// </summary>
        /// <returns>The HTML as a string.</returns>
        public string GetControlString()
        {
            if (expression != null)
            {
                return this.htmlHelper.HiddenFor(this.expression).ToString();
            }
            else
            {
                return this.htmlHelper.Hidden(this.unboundName, this.unboundValue).ToString();
            }
        }

        public override string ToString()
        {
            MvcHtmlHelper.WriteUnencodedContent(this.htmlHelper, GetControlString());

            return "";
        }
    }
}
