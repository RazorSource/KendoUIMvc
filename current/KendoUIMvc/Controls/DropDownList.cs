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
    public class DropDownList<TModel, TProperty> : ControlBase<TModel, TProperty, IDropDownList<TModel, TProperty>>, IDropDownList<TModel, TProperty>
    {
        protected IList<SelectListItem> selectListDataSource;

        public DropDownList(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression) :
            base(htmlHelper, expression)
        {
        }

        public DropDownList(HtmlHelper<TModel> htmlHelper, string name, object value = null) :
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

        /// <summary>
        /// Sets the server side data source to use to populate the drop down list options.
        /// </summary>
        /// <param name="dataSource">List of select list items to use for the drop down options.</param>
        /// <returns></returns>
        public IDropDownList<TModel, TProperty> SetDataSource(IList<SelectListItem> dataSource)
        {
            this.selectListDataSource = dataSource;

            return this;
        }

        protected override MvcHtmlString GetControlHtml()
        {
            StringBuilder html = new StringBuilder();

            AddControlClass("drop-down-list");

            html.AppendLine(GetBaseControlString());

            html.AppendLine(@"
                <script type=""text/javascript"">
                    $(document).ready(function() {");
            
            if (this.selectListDataSource != null)
            {
                html.Append(GetSelectListCreatedScript());
            }

            html.AppendLine(@"
                    });
                </script>");

            return new MvcHtmlString(html.ToString());
        }

        protected virtual string GetSelectListCreatedScript()
        {
            StringBuilder script = new StringBuilder();

            script.AppendLine(@"
                        var " + this.controlId + @"Data = [");

            int itemsAddedCt = 0;
            foreach (SelectListItem nextSelectListItem in selectListDataSource)
            {
                script.AppendLine(@"{ Text: """ + nextSelectListItem.Text + @""", Value: """ + nextSelectListItem.Value + @""" }" +
                    (++itemsAddedCt < selectListDataSource.Count ? "," : ""));
            }

            script.AppendLine(@"
                        ];

                        $('#" + this.controlId + @"').kendoDropDownList({
                            dataTextField: 'Text',
                            dataValueField: 'Value',
                            dataSource: " + this.controlId + @"Data
                        });");

            return script.ToString();
        }
    }
}
