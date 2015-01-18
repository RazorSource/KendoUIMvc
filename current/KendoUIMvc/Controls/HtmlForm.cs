using KendoUIMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using KendoUIMvc.Util;

namespace KendoUIMvc.Controls
{
    public class HtmlForm<TModel> : IDisposable
    {
        protected bool disposed;
        protected HtmlHelper<TModel> htmlHelper;
        protected string formId;
        protected string actionName;
        protected string controllerName;
        protected MvcForm mvcForm;
        protected ViewSettings.FormLayoutOption? formLayout;

        public HtmlForm(HtmlHelper<TModel> htmlHelper, string formId, string actionName, string controllerName)
        {
            this.htmlHelper = htmlHelper;
            this.formId = formId;
            this.actionName = actionName;
            this.controllerName = controllerName;
            // Restore Default View Settings each time a form is constructed to support multiple forms on a view.
            RestoreDefaultViewSettings();
        }

        protected virtual void RestoreDefaultViewSettings()
        {
            ViewSettings viewSettings = this.htmlHelper.ViewData.GetViewSettings();
            viewSettings.RestoreDefaultViewSettings();
        }

        /// <summary>
        /// Sets the form layout to use for controls on the form.  Vertical will place labels above the form.  Horizontal will
        /// place labels beside controls.
        /// </summary>
        /// <param name="formLayout">Enum value for the form layout.</param>
        /// <returns></returns>
        public HtmlForm<TModel> SetFormLayout(ViewSettings.FormLayoutOption formLayout)
        {
            this.formLayout = formLayout;
            return this;
        }

        /// <summary>
        /// Sets the default control column style to apply to child elements.
        /// </summary>
        /// <param name="defaultControlColumnStyle">Default column style to apply to controls.  The default value
        /// is col-md-10.</param>
        /// <returns></returns>
        public HtmlForm<TModel> SetDefaultControlColumnStyle(string defaultControlColumnStyle)
        {
            this.htmlHelper.ViewData.GetViewSettings().DefaultControlColumnStyle = defaultControlColumnStyle;
            return this;
        }

        /// <summary>
        /// Sets the default label column style to apply to child elements.
        /// </summary>
        /// <param name="defaultLabelColumnStyle">Default column style to apply to labels.  The default value
        /// is col-md-2.</param>
        /// <returns></returns>
        public HtmlForm<TModel> SetDefaultLabelColumnStyle(string defaultLabelColumnStyle)
        {
            this.htmlHelper.ViewData.GetViewSettings().DefaultLabelColumnStyle = defaultLabelColumnStyle;
            return this;
        }

        /// <summary>
        /// Sets the default group column style to apply to child elements.
        /// </summary>
        /// <param name="defaultLabelColumnStyle">Default column style to apply to control groups.  The default value
        /// is to not have a column style on the control group (null).</param>
        /// <returns></returns>
        public HtmlForm<TModel> SetDefaultGroupColumnStyle(string defaultGroupColumnStyle)
        {
            this.htmlHelper.ViewData.GetViewSettings().DefaultGroupColumnStyle = defaultGroupColumnStyle;
            return this;
        }

        public HtmlForm<TModel> RenderBegin()
        {
            ViewSettings viewSettings = this.htmlHelper.ViewData.GetViewSettings();

            IDictionary<string, object> attributes = new Dictionary<string, object>();
            attributes.Add("id", this.formId);

            if (this.formLayout != null && this.formLayout.Value == ViewSettings.FormLayoutOption.Horizontal)
            {
                attributes.Add("class", BootstrapCssClass.form_horizontal);
                // Set the view settings form layout, so nested controls know how to render.
                viewSettings.FormLayout = ViewSettings.FormLayoutOption.Horizontal;
            }
            else
            {
                viewSettings.FormLayout = ViewSettings.FormLayoutOption.Vertical;
            }

            mvcForm = this.htmlHelper.BeginForm(actionName, controllerName, FormMethod.Post, attributes);
            return this;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                this.disposed = true;

                if (this.mvcForm != null)
                {
                    this.mvcForm.EndForm();
                }
            }
        }
    }
}
