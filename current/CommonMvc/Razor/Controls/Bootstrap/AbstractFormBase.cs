﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CommonMvc.Models;
using CommonMvc.Razor.Controls;
using CommonMvc.Util;
using CommonMvc.Models.Bootstrap;

namespace CommonMvc.Razor.Controls.Bootstrap
{
    /// <summary>
    /// Abstract base class that can be used for HTML forms that are layed out with the Bootstrap column model.  The
    /// implementing class must provide implmenetations to create buttons and message displays for use within the form.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TControl"></typeparam>
    public abstract class AbstractFormBase<TModel, TControl> : IDisposable where TControl : class
    {
        protected bool disposed;
        protected MvcForm mvcForm;
        protected HtmlHelper<TModel> htmlHelper;
        protected string formId;
        protected string actionName;
        protected string controllerName;
        protected ViewSettings.FormLayoutOption? formLayout;
        protected string formStyle;
        protected IList<IButton<TModel>> footerActionButtons = new List<IButton<TModel>>();
        protected bool includePanel;
        protected string title;
        protected IPanel<TModel> panel;
        protected IMessageDisplay<TModel> messageDisplay;
        protected bool autoPostReturnUrl;

        /// <summary>
        /// Renders the begin form tag for the form.
        /// </summary>
        /// <param name="layoutAttributes"></param>
        /// <returns></returns>
        protected abstract MvcForm RenderBeginForm(IDictionary<string, object> layoutAttributes);

        /// <summary>
        /// Creates an IMessageDisplay instance to use for the form to display messages to the user.
        /// </summary>
        /// <returns></returns>
        protected abstract IMessageDisplay<TModel> CreateMessageDisplay();

        /// <summary>
        /// Creates an IPanel instance used to contain the form content.
        /// </summary>
        /// <returns></returns>
        protected abstract IPanel<TModel> CreatePanel();

        public AbstractFormBase(HtmlHelper<TModel> htmlHelper, string formId, string actionName, string controllerName)
        {
            this.htmlHelper = htmlHelper;
            this.formId = formId;
            this.actionName = actionName;
            this.controllerName = controllerName;
            // Restore Default View Settings each time a form is constructed to support multiple forms on a view.
            RestoreDefaultViewSettings();

            // By default include the panel around the form.
            this.includePanel = true;

            // By default the returnUrl parameter is autoposted.
            this.autoPostReturnUrl = true;

            // Initialize the notification control.
            messageDisplay = CreateMessageDisplay();
        }

        protected virtual void RestoreDefaultViewSettings()
        {
            ViewSettings viewSettings = RequestHelper.ViewSettings;
            viewSettings.RestoreDefaultViewSettings();
        }

        /// <summary>
        /// Sets the form layout to use for controls on the form.  Vertical will place labels above the form.  Horizontal will
        /// place labels beside controls.
        /// </summary>
        /// <param name="formLayout">Enum value for the form layout.</param>
        /// <returns></returns>
        public TControl SetFormLayout(ViewSettings.FormLayoutOption formLayout)
        {
            this.formLayout = formLayout;
            return this as TControl;
        }

        /// <summary>
        /// Sets the default control column style to apply to child elements.
        /// </summary>
        /// <param name="defaultControlColumnStyle">Default column style to apply to controls.  The default value
        /// is col-md-10.</param>
        /// <returns></returns>
        public TControl SetDefaultControlStyle(string defaultControlStyle)
        {
            RequestHelper.ViewSettings.DefaultControlStyle = defaultControlStyle;
            return this as TControl;
        }

        /// <summary>
        /// Sets the default style to apply to child label elements.
        /// </summary>
        /// <param name="defaultLabelColumnStyle">Default style to apply to labels.  The default value
        /// is col-md-2.</param>
        /// <returns></returns>
        public TControl SetDefaultLabelStyle(string defaultLabelStyle)
        {
            RequestHelper.ViewSettings.DefaultLabelStyle = defaultLabelStyle;
            return this as TControl;
        }

        /// <summary>
        /// Sets the default style to apply to child group elements.  The group contains the label and the control.
        /// </summary>
        /// <param name="defaultLabelStyle">Default style to apply to control groups.  The default value
        /// is to not have a column style on the control group (null).</param>
        /// <returns></returns>
        public TControl SetDefaultGroupStyle(string defaultGroupStyle)
        {
            RequestHelper.ViewSettings.DefaultGroupStyle = defaultGroupStyle;
            return this as TControl;
        }

        /// <summary>
        /// Sets the style to use for the form.  This can be used to adjust the width of the entire form.
        /// </summary>
        /// <param name="formStyle">The style to apply to the form.  The default value
        /// is to not have a column style on the form (null).</param>
        /// <returns></returns>
        public TControl SetFormStyle(string formStyle)
        {
            this.formStyle = formStyle;
            return this as TControl;
        }

        /// <summary>
        /// Adds an action button to the footer of the form.  Buttons are right aligned to the bottom of the form
        /// and are displayed in the order added to the form.
        /// </summary>
        /// <param name="button">The button to add.</param>
        /// <returns></returns>
        public TControl AddFooterActionButton(IButton<TModel> button)
        {
            this.footerActionButtons.Add(button);
            return this as TControl;
        }

        /// <summary>
        /// Sets a flag indicating if the form should be wrapped in a styled panel.  By default the form is wrapped in a panel.
        /// </summary>
        /// <param name="includePanel">True if the form should be wrapped in a styled panel (default).  False if the form should
        /// not be wrapped in a panel.</param>
        /// <returns></returns>
        public TControl SetIncludePanel(bool includePanel)
        {
            this.includePanel = includePanel;
            return this as TControl;
        }

        /// <summary>
        /// Sets the title to display.
        /// </summary>
        /// <param name="panelTitle">Title to display above the form.</param>
        /// <returns></returns>
        public TControl SetTitle(string title)
        {
            this.title = title;
            return this as TControl;
        }

        /// <summary>
        /// Sets a boolen flag indicating if the return URL should automatically be added as a hidden field to the form, so it is
        /// reposted on save events.  This is intended to allow redirects to the desired return URL to occur after edits are
        /// successfully saved via a post action.  The hidden field will only be added, if the form is loaded with a returnUrl query
        /// string parameter.  The default value is true.
        /// </summary>
        /// <param name="autoPostReturnUrl">Flag indicating if a hidden returnUrl parameter should automatically be set on the form.</param>
        /// <returns></returns>
        public TControl SetAutoPostReturnUrl(bool autoPostReturnUrl)
        {
            this.autoPostReturnUrl = autoPostReturnUrl;
            return this as TControl;
        }

        /// <summary>
        /// Renders the opening tags for the form.  This call should typically be used within a using block to ensure proper closing
        /// tags are emitted when the form is disposed.
        /// </summary>
        /// <returns></returns>
        public TControl RenderBegin()
        {
            ViewSettings viewSettings = RequestHelper.ViewSettings;
            List<string> classes = new List<string>();

            IDictionary<string, object> attributes = new Dictionary<string, object>();
            attributes.Add("id", this.formId);

            // If a bootstrap column style has been specified for the form, add the class, unless a panel
            // is included,  If a panel is included, the specified style is applied to the panel.
            if (formStyle != null && !includePanel)
            {
                classes.Add(formStyle);
            }

            if (this.formLayout != null && this.formLayout.Value == ViewSettings.FormLayoutOption.Horizontal)
            {
                classes.Add(BootstrapCssClass.form_horizontal);
                // Set the view settings form layout, so nested controls know how to render.
                viewSettings.FormLayout = ViewSettings.FormLayoutOption.Horizontal;
            }
            else
            {
                viewSettings.FormLayout = ViewSettings.FormLayoutOption.Vertical;
            }

            // Add CSS classes.
            if (classes.Count > 0)
            {
                attributes.Add("class", string.Join(" ", classes));
            }

            if (includePanel)
            {
                // If a panel if included, render the start to the panel.  The panel
                // will be close out in the dispose method.
                this.panel = CreatePanel()
                    .SetPanelStyle(this.formStyle) // Make the panels style the same as the forms.
                    .RenderBegin();
            }

            if (title != null)
            {
                string titleClass = includePanel ? "km-panel-title" : "km-form-title";

                MvcHtmlHelper.WriteUnencodedContent(this.htmlHelper, @"
                    <div class=""" + titleClass + @""">" + this.title + @"</div>");
            }

            this.mvcForm = RenderBeginForm(attributes);

            if (this.autoPostReturnUrl)
            {
                string returnUrl = MvcHtmlHelper.GetRequestParameter(this.htmlHelper, ViewSettings.RETURN_URL_PARAM);
                if (returnUrl != null)
                {
                    MvcHtmlHelper.WriteUnencodedContent(this.htmlHelper, 
                        this.htmlHelper.Hidden(ViewSettings.RETURN_URL_PARAM, returnUrl).ToString());
                }
            }

            return this as TControl;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected string GetFooterActionButtons()
        {
            StringBuilder html = new StringBuilder();
            
            html.Append(@"
                <div style=""clear: both;"" class=""km-panel-action-footer"">");

            foreach (IButton<TModel> nextButton in this.footerActionButtons)
            {
                html.Append(nextButton.GetControlString());
            }

            html.Append(@"
                </div>");

            return html.ToString();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                this.disposed = true;

                // Write out footer action buttons if included.
                if (this.footerActionButtons.Count > 0)
                {
                    MvcHtmlHelper.WriteUnencodedContent(this.htmlHelper, GetFooterActionButtons());
                }

                // Close out form.
                if (this.mvcForm != null)
                {
                    this.mvcForm.EndForm();
                }

                // Write out notification panel and related javascript to show errors.
                if (messageDisplay != null)
                {
                    MvcHtmlHelper.WriteUnencodedContent(this.htmlHelper, messageDisplay.GetControlString());
                }
                
                // Close out the panel if included.
                if (this.panel != null)
                {
                    this.panel.EndPanel();
                }
            }
        }

        /// <summary>
        /// Gets the MessageDisplay used to display messages related to the form.
        /// </summary>
        /// <returns></returns>
        public IMessageDisplay<TModel> GetMessageDisplay()
        {
            return messageDisplay;
        }

        /// <summary>
        /// Writes out the ending tags for the form.
        /// </summary>
        public virtual void EndForm()
        {
            Dispose(true);
        }
    }
}
