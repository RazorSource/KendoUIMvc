using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using KendoUIMvc.Util;
using CommonMvc.Razor.Controls;

namespace KendoUIMvc.Controls
{
    public class TextBox<TModel, TProperty> : ControlBase<TModel, TProperty, ITextBox<TModel, TProperty>>, ITextBox<TModel, TProperty>
    {
        public TextBox(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression) :
            base(htmlHelper, expression)
        {
        }

        public TextBox(HtmlHelper<TModel> htmlHelper, string name, object value = null) :
            base(htmlHelper, name, value)
        {
        }

        protected string GetBaseControlString()
        {
            IDictionary<string, object> attributes = GetControlAttributes();
            
            if (this.expression == null)
            {
                return this.htmlHelper.TextBox(this.unboundName, this.unboundValue, attributes).ToString();
            }
            else
            {
                return this.htmlHelper.TextBoxFor(expression, attributes).ToString();
            }
        }

        protected override MvcHtmlString GetControlHtml()
        {
            StringBuilder html = new StringBuilder();

            AddControlClass("text-box");

            html.AppendLine(GetBaseControlString());

            return new MvcHtmlString(html.ToString());
        }
    }
}
