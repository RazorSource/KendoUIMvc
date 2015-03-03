using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CommonMvc.Util;
using CommonMvc.Razor.Controls;

namespace KendoUIMvc.Controls
{
    public class TextArea<TModel, TProperty> : ControlBase<TModel, TProperty, ITextArea<TModel, TProperty>>, ITextArea<TModel, TProperty>
    {
        protected int? rows;
        protected int? columns;

        public TextArea(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression) :
            base(htmlHelper, expression)
        {
        }

        public TextArea(HtmlHelper<TModel> htmlHelper, string name, string value = null) :
            base(htmlHelper, name, value)
        {
        }

        /// <summary>
        /// Sets the number of rows to display for the text area.
        /// </summary>
        /// <param name="rows">Number of rows to display.</param>
        /// <returns></returns>
        public ITextArea<TModel, TProperty> SetRows(int rows)
        {
            this.rows = rows;
            return this;
        }

        /// <summary>
        /// Sets the number of columns to display in the text area.  Using the HTML TextArea cols attribute.
        /// Note that in some responsive design layouts such as Twitter Bootstrap this property may not have
        /// any effect, as the width may be specified via CSS and override this value.
        /// </summary>
        /// <param name="columns">The number of columns to display.</param>
        /// <returns></returns>
        public ITextArea<TModel, TProperty> SetColumns(int columns)
        {
            this.columns = columns;
            return this;
        }

        protected string GetBaseControlString()
        {
            IDictionary<string, object> attributes = GetControlAttributes();

            // Add rows and columns as custom attributes in case only one is specified.
            if (this.rows != null)
            {
                attributes.Add("rows", this.rows);
            }

            if (this.columns != null)
            {
                attributes.Add("cols", this.columns);
            }

            if (this.expression == null)
            {
                return this.htmlHelper.TextArea(this.unboundName, (string)this.unboundValue, attributes).ToString();
            }
            else
            {
                return this.htmlHelper.TextAreaFor(expression, attributes).ToString();
            }
        }

        protected override MvcHtmlString GetControlHtml()
        {
            StringBuilder html = new StringBuilder();

            AddControlClass("text-area");

            html.AppendLine(GetBaseControlString());

            return new MvcHtmlString(html.ToString());
        }
    }
}
