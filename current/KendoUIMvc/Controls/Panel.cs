using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CommonMvc.Razor.Controls;
using KendoUIMvc.Util;

namespace KendoUIMvc.Controls
{
    public class Panel<TModel> : IPanel<TModel>
    {
        protected bool disposed;
        protected HtmlHelper<TModel> htmlHelper;
        protected string name;
        protected string title;
        protected string panelStyle;

        public Panel(HtmlHelper<TModel> htmlHelper, string name)
        {
            this.htmlHelper = htmlHelper;
            this.name = name;
        }

        /// <summary>
        /// Sets the title to display on the panel.
        /// </summary>
        /// <param name="title">Title to display.</param>
        /// <returns></returns>
        public IPanel<TModel> SetTitle(string title)
        {
            this.title = title;
            return this;
        }

        /// <summary>
        /// Sets the column style to use for the panel.  This can be used to adjust the width of the entire panel.
        /// </summary>
        /// <param name="panelStyle">The column style to apply to the panel.  The default value
        /// is to not have a column style on the panel (null).</param>
        /// <returns></returns>
        public IPanel<TModel> SetPanelStyle(string panelStyle)
        {
            this.panelStyle = panelStyle;
            return this;
        }

        /// <summary>
        /// Renders the opening tags for the panel.  This call should typically be used within a using block to ensure proper closing
        /// tags are emitted when the panel is disposed.
        /// </summary>
        /// <returns></returns>
        public IPanel<TModel> RenderBegin()
        {
            StringBuilder html = new StringBuilder();
            List<string> classes = new List<string>();

            classes.Add("well");
            classes.Add("clearfix");

            // Add the bootstrap column style if it is supplied.
            if (this.panelStyle != null)
            {
                classes.Add(this.panelStyle);
            }

            html.Append(@"
                <div id=""" + this.name + @""" class=""" + string.Join(" ", classes) + @""">");
            
            if (title != null)
            {
                html.Append(@"
                    <div class=""km-panel-title"">" + this.title + @"</div>");
            }

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

                MvcHtmlHelper.WriteUnencodedContent(this.htmlHelper, @"
                </div>");
            }
        }

        /// <summary>
        /// Writes out the ending tags for the panel.
        /// </summary>
        public virtual void EndPanel()
        {
            Dispose(true);
        }
    }
}
