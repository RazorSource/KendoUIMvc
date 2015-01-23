using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using KendoUIMvc.Util;

namespace KendoUIMvc.Controls
{
    public class Button<TModel>
    {
        protected HtmlHelper<TModel> htmlHelper;
        protected string name;
        protected string label;
        protected bool acceptsReturn;
        protected bool submit;
        protected string onClick;
        protected bool primary;
        protected IList<string> additionalClasses = new List<string>();
        protected string icon;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Name of the button.</param>
        /// <param name="label">Label to display.</param>
        public Button (HtmlHelper<TModel> htmlHelper, string name, string label)
        {
            this.htmlHelper = htmlHelper;
            this.name = name;
            this.label = label;
        }

        /// <summary>
        /// Sets a flag indicating if the button should be invoked when the return key is pressed.
        /// </summary>
        /// <param name="acceptsReturn">True if the return key invoke the button action.</param>
        /// <returns></returns>
        public Button<TModel> SetAcceptsReturn(bool acceptsReturn)
        {
            this.acceptsReturn = acceptsReturn;
            return this;
        }

        /// <summary>
        /// Sets a flag indicating if this should be a submit button.
        /// </summary>
        /// <param name="submit">True if the button should be marked as a submit button for a form.</param>
        /// <returns></returns>
        public Button<TModel> SetSubmit(bool submit)
        {
            this.submit = submit;
            return this;
        }

        /// <summary>
        /// Sets the javascript function to call on the click event of the button.
        /// </summary>
        /// <param name="onClick">Function to call.  As an example DoAction()</param>
        /// <returns></returns>
        public Button<TModel> SetOnClick(string onClick)
        {
            this.onClick = onClick;
            return this;
        }

        /// <summary>
        /// Sets a flag indicating if this button should be styled as a primary button.
        /// </summary>
        /// <param name="primary">True if the button should be displayed as a primary button.</param>
        /// <returns></returns>
        public Button<TModel> SetPrimary(bool primary)
        {
            this.primary = primary;
            return this;
        }

        /// <summary>
        /// Adds a CSS class to add to the button. 
        /// </summary>
        /// <param name="cssClass">CSS class to add.</param>
        /// <returns></returns>
        public Button<TModel> AddClass(string cssClass)
        {
            additionalClasses.Add(cssClass);
            return this;
        }

        /// <summary>
        /// Sets the Kendo Icon to use with the button.
        /// </summary>
        /// <param name="icon">Name of the Kendo Icon (i.e. "plus")</param>
        /// <returns></returns>
        public Button<TModel> SetIcon(string icon)
        {
            this.icon = icon;
            return this;
        }

        protected IList<string> GetClasses()
        {
            // Start from the additional CSS classes that have been specified.
            List<string> classes = new List<string>(additionalClasses);

            if (primary)
            {
                classes.Add("k-primary");
            }

            return classes;
        }

        /// <summary>
        /// Gets the HTML necessary to render the control.
        /// </summary>
        /// <returns>The HTML as a string.</returns>
        public string GetControlString()
        {
            StringBuilder html = new StringBuilder();
            string tag = (acceptsReturn || this.submit) ? "button" : "a";
            IList<string> classes = GetClasses();

            html.Append(@"
                <" + tag + @" id=""" + this.name + @"""");

            if (this.submit)
            {
                html.Append(@" type=""submit""");
            }

            if (this.onClick != null)
            {
                html.Append(@" onclick=""" + this.onClick + @"""");
            }

            // Append any necessary css classes.
            if (classes.Count > 0)
            {
                html.Append(@" class=""" + string.Join(" ", classes) + @"""");
            }

            html.Append(@">" + this.label + "</" + tag + @">
                <script type=""text/javascript"">
                    $(document).ready(function () {
                        $('#" + this.name + @"').kendoButton(" + GetButtonAttributes() + @");
                    });
                </script>");

            return html.ToString();
        }

        /// <summary>
        /// Gets all attributes from various propery settings for the button.
        /// </summary>
        /// <returns></returns>
        protected string GetButtonAttributes()
        {
            StringBuilder attributes = new StringBuilder();

            if (icon != null)
            {
                attributes.Append(@"
                            icon: '" + this.icon + "'");
            }

            if (attributes.Length > 0)
            {
                return "{" + attributes.ToString() + "}";
            }
            else
            {
                return "";
            }
        }

        public override string ToString()
        {
            MvcHtmlHelper.WriteUnencodedContent(this.htmlHelper, GetControlString());

            return "";
        }
    }
}