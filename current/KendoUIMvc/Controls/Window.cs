using KendoUIMvc.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KendoUIMvc.Controls
{
    public class Window<TModel> : IDisposable
    {
        protected bool disposed;
        protected HtmlHelper<TModel> htmlHelper;
        protected string name;
        protected int width;
        protected bool showOnLoad;
        protected string title;

        public Window(HtmlHelper<TModel> htmlHelper, string name)
        {
            this.htmlHelper = htmlHelper;
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

        public Window<TModel> RenderBegin()
        {
            StringBuilder html = new StringBuilder();

            html.Append(@"
            <div id=""" + this.name + @""">");

            MvcHtmlHelper.WriteUnencodedContent(this.htmlHelper, html.ToString());

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
                            width: '" + this.width + @"',
                            title: '" + this.title + @"',
                            actions: ['Pin', 'Minimize', 'Maximize', 'Close']
                        });
                    }");

            if (!this.showOnLoad)
            {
                MvcHtmlHelper.WriteUnencodedContent(this.htmlHelper, @"
                    kendoWindowDiv.data('kendoWindow').close();");
            }

            // Close out document ready function.
            MvcHtmlHelper.WriteUnencodedContent(this.htmlHelper, @"
                });
            </script>");
            }
        }
    }
}