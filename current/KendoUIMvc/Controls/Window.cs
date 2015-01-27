using KendoUIMvc.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Mvc.Ajax;

namespace KendoUIMvc.Controls
{
    public class Window<TModel> : IDisposable
    {
        protected bool disposed;
        protected HtmlHelper<TModel> htmlHelper;
        protected AjaxHelper<TModel> ajaxHelper;
        protected string name;
        protected int width;
        protected bool showOnLoad;
        protected string title;
        protected AjaxForm<TModel> ajaxForm;

        public Window(HtmlHelper<TModel> htmlHelper, AjaxHelper<TModel> ajaxHelper, string name)
        {
            this.htmlHelper = htmlHelper;
            this.ajaxHelper = ajaxHelper;
            this.name = name;

            // Set default values
            // Default to 600 pixels wide.
            this.width = 600;
        }

        /// <summary>
        /// Sets the width of the window in pixels.  The default value is 600 pixels.
        /// </summary>
        /// <param name="width">Width of the window in pixels.</param>
        /// <returns></returns>
        public Window<TModel> SetWidth(int width)
        {
            this.width = width;

            return this;
        }

        /// <summary>
        /// Sets the title for the window.
        /// </summary>
        /// <param name="title">Title to display.</param>
        /// <returns></returns>
        public Window<TModel> SetTitle(string title)
        {
            this.title = title;

            return this;
        }

        /// <summary>
        /// Sets a flag indicating if the window should be shown when the page is initially loaded.
        /// </summary>
        /// <param name="showOnLoad">True if the window should be displayed when the page initially loads.  The
        /// default value is false.</param>
        /// <returns></returns>
        public Window<TModel> SetShowOnLoad(bool showOnLoad)
        {
            this.showOnLoad = showOnLoad;

            return this;
        }

        /// <summary>
        /// Sets ajax action that are used to post data from the form to an ajax request.  Adding the actions will automatically
        /// wrap all elements in the window in an AjaxForm.
        /// </summary>
        /// <param name="actionName">The action used to post data.</param>
        /// <param name="controllerName">The controller used to post data.  If null, the current controller is used.</param>
        /// <param name="ajaxOptions">AjaxOptions to add to the auto-included AjaxForm on the window.</param>
        /// <returns></returns>
        public Window<TModel> SetAjaxActions(string actionName, string controllerName = null, AjaxOptions ajaxOptions = null)
        {
            this.ajaxForm = new AjaxForm<TModel>(this.htmlHelper, this.ajaxHelper, this.name + "Form", actionName, controllerName);

            if (ajaxOptions != null)
            {
                this.ajaxForm.SetAjaxOptions(ajaxOptions);
            }

            return this;
        }        

        /// <summary>
        /// Sets the AjaxForm instance to embed in the window.  SetAjaxActions can be used for basic configuration.  For more detailed AjaxForm configuration
        /// this method can be used.
        /// </summary>
        /// <param name="ajaxForm">The AjaxForm to embed in the window.</param>
        /// <returns></returns>
        public Window<TModel> SetAjaxForm(AjaxForm<TModel> ajaxForm)
        {
            this.ajaxForm = ajaxForm;
            return this;
        }

        public Window<TModel> RenderBegin()
        {
            StringBuilder html = new StringBuilder();

            html.Append(@"
            <div id=""" + this.name + @""" style=""display: none;"">");

            MvcHtmlHelper.WriteUnencodedContent(this.htmlHelper, html.ToString());

            // If an AJAX action is configured add an AjaxForm to the window.
            if (this.ajaxForm != null)
            {
                this.ajaxForm.RenderBegin();
            }

            return this;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets the AjaxForm embedded within the window.
        /// </summary>
        /// <returns></returns>
        public AjaxForm<TModel> GetAjaxForm()
        {
            return this.ajaxForm;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                this.disposed = true;

                // If the window contained an AjaxForm close it out.
                if (this.ajaxForm != null)
                {
                    this.ajaxForm.EndForm();
                }

                // Write out closing div tag.
                MvcHtmlHelper.WriteUnencodedContent(this.htmlHelper, @"
            </div>
            <script type=""text/javascript"">
                function show" + this.name + @"() {
                    $('#" + this.name + @"').data('kendoWindow').open();
                }

                $(document).ready(function() {
                    var kendoWindowDiv = $('#" + this.name + @"');
    
                    if (!kendoWindowDiv.data('kendoWindow')) {
                        $('#" + this.name + @"').kendoWindow({
                            " + GetAttributes() + @"
                        }).data('kendoWindow').center();
                    }");
           
            // Close out document ready function.
            MvcHtmlHelper.WriteUnencodedContent(this.htmlHelper, @"
                });
            </script>");
            }
        }

        /// <summary>
        /// Writes out the attributes based on the Window configuration settings.
        /// </summary>
        /// <returns></returns>
        protected string GetAttributes()
        {
            StringBuilder attributes = new StringBuilder();

            if (this.ajaxForm != null && this.ajaxForm.GetNotification() != null)
            {
                // Hide any error message when the window is closed.
                attributes.Append(@"
                            close: function () { " + this.ajaxForm.GetNotification().GetCallHideScript() + @" },");
            }

            attributes.Append(@"
                            width: '" + this.width + @"',
                            title: '" + this.title + @"',
                            actions: ['Pin', 'Minimize', 'Maximize', 'Close'],
                            visible: " + MvcHtmlHelper.GetJavascriptBoolean(this.showOnLoad) + @"");

            return attributes.ToString();
        }
    }
}