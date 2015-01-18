using KendoUIMvc.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace KendoUIMvc.Controls
{
    public class DatePicker<TModel, TProperty> : ControlBase<TModel, TProperty, DatePicker<TModel, TProperty>>
    {
        public DatePicker(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression) :
            base(htmlHelper, expression)
        {
        }

        public DatePicker(HtmlHelper<TModel> htmlHelper, string name, object value = null) :
            base(htmlHelper, name, value)
        {
        }

        protected string GetBaseControlString()
        {
            if (this.expression == null)
            {
                return this.htmlHelper.TextBox(this.unboundName, this.unboundValue, GetControlAttributes()).ToString();
            }
            else
            {
                return this.htmlHelper.TextBoxFor(expression, GetControlAttributes()).ToString();
            }
        }

        protected override MvcHtmlString GetControlHtml()
        {
            StringBuilder html = new StringBuilder();

            AddControlClass("date-picker");

            html.AppendLine(GetBaseControlString());

            html.AppendLine(@"
                <script type=""text/javascript"">
                    $(document).ready(function() {
                        $('#" + this.controlId + @"').kendoDatePicker();
                    });
                </script>");

            return new MvcHtmlString(html.ToString());
        }
    }
}
