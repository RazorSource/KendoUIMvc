﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using KendoUIMvc.Util;
using KendoUIMvc.Models;
using System.Linq.Expressions;

namespace KendoUIMvc.Controls
{
    public abstract class ControlBase<TModel, TProperty, TControl> where TControl : class
    {
        private const string LEVEL_1_SPACER = "    ";
        private const string LEVEL_2_SPACER = LEVEL_1_SPACER + LEVEL_1_SPACER;
        private const string LEVEL_3_SPACER = LEVEL_2_SPACER + LEVEL_1_SPACER;
        private const string LEVEL_4_SPACER = LEVEL_3_SPACER + LEVEL_1_SPACER;

        protected HtmlHelper<TModel> htmlHelper;
        protected Expression<Func<TModel, TProperty>> expression;
        protected string unboundName;
        protected object unboundValue;
        protected string controlId;
        protected IList<string> labelClasses = new List<string>();
        protected IList<string> controlClasses = new List<string>();
        protected string labelText;
        protected string labelColumnStyle;
        protected string controlColumnStyle;
        protected string groupColumnStyle;

        protected abstract MvcHtmlString GetControlHtml();

        private ControlBase(HtmlHelper<TModel> htmlHelper)
        {
            this.htmlHelper = htmlHelper;
            ViewSettings viewSettings = htmlHelper.ViewData.GetViewSettings();
            this.labelColumnStyle = viewSettings.DefaultLabelColumnStyle;
            this.controlColumnStyle = viewSettings.DefaultControlColumnStyle;
            this.groupColumnStyle = viewSettings.DefaultGroupColumnStyle;
        }

        public ControlBase(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
            : this(htmlHelper)
        {            
            this.expression = expression;

            this.controlId = MvcHtmlHelper.GetExpressionPropertyId(htmlHelper, expression);
        }

        public ControlBase(HtmlHelper<TModel> htmlHelper, string name, object value = null)
            : this(htmlHelper)
        {
            this.unboundName = name;
            this.unboundValue = value;
            // Set the control ID to the unbound name.
            this.controlId = name;
        }

        /// <summary>
        /// Sets a override label text for the control.  For bound controls the label is derived from the model bound by the expression.
        /// If the control is not bound to a model by an expression, the LabelText can be used to display a label with the unbound control.
        /// </summary>
        /// <returns></returns>
        public TControl SetLabelText(string labelText)
        {
            this.labelText = labelText;
            return this as TControl;
        }

        /// <summary>
        /// Adds a CSS class to the label tag.
        /// </summary>
        /// <param name="styleClass">CSS class name to add.</param>
        /// <returns></returns>
        public TControl AddLabelClass(string styleClass)
        {
            if (!this.labelClasses.Contains(styleClass))
            {
                this.labelClasses.Add(styleClass);
            }

            return this as TControl;
        }

        /// <summary>
        /// Sets the label column style that is used to determine the bootstrap layout for the label.
        /// </summary>
        /// <param name="labelColumnStyle">The bootstrap label column style.  The default column style for the label
        /// is col-md-2.</param>
        /// <returns></returns>
        public TControl SetLabelColumnStyle(string labelColumnStyle)
        {
            this.labelColumnStyle = labelColumnStyle;
            return this as TControl;
        }

        /// <summary>
        /// Sets the label column style that is used to determine the bootstrap layout for the label.
        /// </summary>
        /// <param name="controlColumnStyle">The bootstrap label column style.  The default column style for the column
        /// is col-md-10.</param>
        /// <returns></returns>
        public TControl SetControlColumnStyle(string controlColumnStyle)
        {
            this.controlColumnStyle = controlColumnStyle;
            return this as TControl;
        }

        /// <summary>
        /// Sets the group column style that is used to determine the bootstrap layout for the form group element,
        /// which contains the label, control and any related validation.
        /// </summary>
        /// <param name="controlColumnStyle">The bootstrap label column style.  A default column style for the group does not exist.</param>
        /// <returns></returns>
        public TControl SetGroupColumnStyle(string groupColumnStyle)
        {
            this.groupColumnStyle = groupColumnStyle;
            return this as TControl;
        }

        /// <summary>
        /// Adds a CSS class to the control tag.
        /// </summary>
        /// <param name="styleClass">CSS class name to add.</param>
        /// <returns></returns>
        public TControl AddControlClass(string styleClass)
        {
            if (!this.controlClasses.Contains(styleClass))
            {
                this.controlClasses.Add(styleClass);
            }

            return this as TControl;
        }

        protected virtual IDictionary<string, object> GetLabelAttributes()
        {
            ViewSettings viewSettings = this.htmlHelper.ViewData.GetViewSettings();
            IDictionary<string, object> attributes = new Dictionary<string, object>();

            // For horizontal layout the corresponding width needs set for the label.
            if (viewSettings.FormLayout == ViewSettings.FormLayoutOption.Horizontal)
            {
                AddLabelClass(BootstrapCssClass.control_label);
                AddLabelClass(this.labelColumnStyle);
            }

            if (this.labelClasses.Count > 0)
            {
                attributes.Add("class", string.Join<string>(" ", this.labelClasses));
            }

            return attributes;
        }

        protected virtual IDictionary<string, object> GetControlAttributes()
        {
            ViewSettings viewSettings = this.htmlHelper.ViewData.GetViewSettings();

            IDictionary<string, object> attributes = new Dictionary<string, object>();

            this.controlClasses.Add(BootstrapCssClass.form_control);

            if (this.controlClasses.Count > 0)
            {
                attributes.Add("class", string.Join<string>(" ", this.controlClasses));
            }

            return attributes;
        }

        protected string GetLabelString()
        {
            IDictionary<string, object> labelAttributes = GetLabelAttributes();

            if (this.labelText != null)
            {
                return this.htmlHelper.LabelForModel(this.labelText, labelAttributes).ToString();
            }
            else if (this.expression != null)
            {
                return this.htmlHelper.LabelFor(expression, labelAttributes).ToString();
            }

            // No label should be generated, if an override label text does not exist, nor the control is bound to an expression.
            return "";
        } 

        protected string GetValidationHtml()
        {
            if (this.expression != null)
            {
                return this.htmlHelper.ValidationMessageFor(this.expression, "", new { @class = "text-danger" }).ToString();
            }

            // If not model bound, no default validation is emmitted.
            return "";
        }

        private bool UseOuterControlDiv(ViewSettings viewSettings)
        {
            return viewSettings.FormLayout == ViewSettings.FormLayoutOption.Horizontal;
        }

        public override string ToString()
        {
            ViewSettings viewSettings = this.htmlHelper.ViewData.GetViewSettings();

            if (this.groupColumnStyle != null)
            {
                WriteContent(@"<div class=""" + this.groupColumnStyle + @""">");
            }

            // Write out opening form-group div.
            WriteContent(@"<div class=""form-group"">");

            // Write out the label.
            WriteContent(LEVEL_1_SPACER + GetLabelString());

            bool useOuterControlDiv = UseOuterControlDiv(viewSettings);
            if (useOuterControlDiv)
            {
                WriteContent(LEVEL_1_SPACER + @"<div class=""" + this.controlColumnStyle + @""">");
            }

            // Write out the raw HTML directly to avoid HTML encoding.
            WriteContent(LEVEL_2_SPACER + GetControlHtml().ToString());

            // Write out validation controls.
            WriteContent(LEVEL_2_SPACER + GetValidationHtml());

            if (useOuterControlDiv)
            {
                // Write out closing horizontal layout control div.
                WriteContent(LEVEL_1_SPACER + "</div>");
            }

            // Write out closing form-group div.
            WriteContent(LEVEL_1_SPACER + "</div>");

            if (this.groupColumnStyle != null)
            {
                // Write out closing group column style div.
                WriteContent(LEVEL_1_SPACER + "</div>");
            }

            // Do not return any textual values from ToString to avoid encoding.
            return "";
        }

        private void WriteContent(string html)
        {
            MvcHtmlHelper.WriteUnencodedContent(this.htmlHelper, html);
        }
    }
}
