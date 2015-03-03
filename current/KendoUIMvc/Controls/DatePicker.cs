using CommonMvc.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CommonMvc.Razor.Controls;

namespace KendoUIMvc.Controls
{
    public class DatePicker<TModel, TProperty> : ControlBase<TModel, TProperty, IDatePicker<TModel, TProperty>>, IDatePicker<TModel, TProperty>
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
            IDictionary<string, object> attributes = GetControlAttributes();

            //attributes.Add("data-parse-formats", "YYYY-MM-DDThh:mm:ss");

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

            AddControlClass("date-picker");

            html.Append(@"
                <div style=""display: block;"">
                ");
            html.AppendLine(GetBaseControlString());
            html.Append(@"
                </div>
                ");

            html.AppendLine(@"
                <script type=""text/javascript"">
                    $(document).ready(function() {
                        $('#" + this.controlId + @"').kendoDatePicker({
                            format: ""M/d/yyyy"",
                            parseFormats: [""yyyy-MM-ddTHH:mm:ss""]
                        });
                    });
                </script>");

            return new MvcHtmlString(html.ToString());
        }
    }
}
