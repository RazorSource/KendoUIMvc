using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using KendoUIMvc.Models;
using CommonMvc.Util;
using CommonMvc.Models;
using CommonMvc.Razor.Controls;

namespace KendoUIMvc.Controls
{
    public class Checkbox<TModel, TProperty> : ControlBase<TModel, TProperty, ICheckbox<TModel, TProperty>>, ICheckbox<TModel, TProperty>
    {
        protected bool checkedByDefault;
        protected bool showLabelRight;

        public Checkbox(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression) :
            base(htmlHelper, expression)
        {
        }

        public Checkbox(HtmlHelper<TModel> htmlHelper, string name, bool? value = false) :
            base(htmlHelper, name, value)
        {
        }

        /// <summary>
        /// Sets a flag indicating if the checkbox should be checked by default.  The default value is false.
        /// </summary>
        /// <param name="checkedByDefault">True if the checkbox should initially be checked.</param>
        /// <returns></returns>
        public ICheckbox<TModel, TProperty> SetCheckedByDefault(bool checkedByDefault)
        {
            this.checkedByDefault = checkedByDefault;
            return this;
        }

        /// <summary>
        /// Forces the label to be shown on the right hand side of the checkbox.  The default value is false.
        /// </summary>
        /// <param name="showLabelRight">True if the label should be shown to the right of the checkbox.</param>
        /// <returns></returns>
        public ICheckbox<TModel, TProperty> SetShowLabelRight(bool showLabelRight)
        {
            this.showLabelRight = showLabelRight;
            return this;
        }

        protected string GetBaseControlString()
        {
            StringBuilder html = new StringBuilder();
            IDictionary<string, object> attributes = GetControlAttributes();
            string controlName;
            bool isChecked = false;
            string containerCssClass = this.showLabelRight ? "km-checkbox-container-right" : "km-checkbox-container";

            this.AddControlClass("k-checkbox");

            if (this.expression != null)
            {
                controlName = MvcHtmlHelper.GetExpressionPropertyName(this.htmlHelper, this.expression);
                isChecked = checkedByDefault || MvcHtmlHelper.GetBooleanValue(this.htmlHelper, this.expression);
            }
            else
            {
                controlName = this.unboundName;
                isChecked = checkedByDefault || (this.unboundValue != null && (bool)this.unboundValue);
            }

            html.Append(@"
                <div class=""" + containerCssClass + @""">
                    <input id=""" + this.controlId + @""" class=""k-checkbox "" type=""checkbox"" value=""true"" name=""" + controlName + @"""");            

            if (isChecked)
            {
                html.Append(@" checked=""true"" ");
            }

            foreach (string nextKey in attributes.Keys)
            {
                // The checked option is specially handled above.
                if (nextKey != "id" && nextKey != "checked" && nextKey != "class")
                {
                    html.Append(" " + nextKey + @"=""" + attributes[nextKey].ToString() + @"""");
                }
            }

            html.Append(@">
                    " + GetCheckboxLabelString() + @"
                </div>");

            return html.ToString();
        }

        /// <summary>
        /// Override the standard GetLabelString functionality.  For checkboxes, the label is emitted after the control if the showLabelRight
        /// flag is set.
        /// </summary>
        /// <returns></returns>
        protected override string GetLabelString()
        {
            if (this.showLabelRight)
            {
                ViewSettings viewSettings = RequestHelper.ViewSettings;

                if (viewSettings.FormLayout == ViewSettings.FormLayoutOption.Vertical)
                {
                    // Add a blank spacer row.
                    return @"<label class=""col-md-12"">&nbsp;</label>";
                }
                else
                {
                    IDictionary<string, object> labelAttributes = GetLabelAttributes();
                    return this.htmlHelper.LabelForModel(" ", labelAttributes).ToString();
                }
            }
            else
            {
                return base.GetLabelString();                
            }
            
        }

        protected virtual string GetCheckboxLabelString()
        {
            IDictionary<string, object> labelAttributes = new Dictionary<string, object>();
            // Apply checkbox class to get bootstrap styling.
            labelAttributes.Add("class", "k-checkbox-label");
            labelAttributes.Add("for", this.controlId);

            string rightLabelText = " ";

            if (this.showLabelRight)
            {
                if (this.labelText != null)
                {
                    rightLabelText = this.labelText;
                }
                else if (this.expression != null)
                {
                    rightLabelText = MvcHtmlHelper.GetDisplayText<TModel, TProperty>(this.htmlHelper, this.expression);
                }
            }

            return this.htmlHelper.LabelForModel(rightLabelText, labelAttributes).ToString();
        } 

        protected override MvcHtmlString GetControlHtml()
        {
            StringBuilder html = new StringBuilder();

            AddControlClass("checkbox");

            html.AppendLine(GetBaseControlString());

            return new MvcHtmlString(html.ToString());
        }
    }
}
