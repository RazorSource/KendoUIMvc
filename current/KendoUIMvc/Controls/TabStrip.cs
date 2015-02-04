using KendoUIMvc.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CommonMvc.Razor.Controls;

namespace KendoUIMvc.Controls
{
    public class TabStrip<TModel> : ITabStrip<TModel>
    {
        protected HtmlHelper<TModel> htmlHelper;
        protected string name;
        protected IList<ITab> tabs = new List<ITab>();
        protected string activeTabName;

        public TabStrip(HtmlHelper<TModel> htmlHelper, string name)
        {
            this.htmlHelper = htmlHelper;
            this.name = name;
        }

        /// <summary>
        /// Builds the HTML necessary to render the control.
        /// </summary>
        /// <returns>An MvcHtmlString that contains all HTML and script necessary to render the grid control.</returns>
        public MvcHtmlString Render()
        {
            StringBuilder html = new StringBuilder();

            html.Append(@"
        <div id=""" + this.name + @""">");

            // Write out tab headers.
            html.Append(@"
            <ul>");

            foreach (ITab nextTab in tabs)
            {
                html.Append(@"
                <li id=""" + nextTab.Name + @"TabHeader""" + (activeTabName == nextTab.Name ? "class=\"k-state-active\"" : "") + @">
                    " + nextTab.Label + @"
                </li>");
            }

            html.Append(@"
            </ul>");

            // Write out tab content.
            foreach (ITab nextTab in tabs)
            {
                html.Append(@"
            <div id=""" + nextTab.Name + @""">
                " + nextTab.Content + @"
            </div>");
            }

            html.Append(@"
        </div>
        <script>
            $(document).ready(function() {
                $('#" + this.name + @"').kendoTabStrip();
            });
        </script>");

            return new MvcHtmlString(html.ToString());
        }

        public override string ToString()
        {
            MvcHtmlHelper.WriteUnencodedContent(this.htmlHelper, Render().ToString());

            // Do not return any textual values from ToString to avoid encoding.
            return "";
        }

        /// <summary>
        /// Sets the name of the tab that should initially be active when the page first loads.  By default
        /// the first tab loaded is set as the default tab.
        /// </summary>
        /// <param name="activeTabName"></param>
        /// <returns></returns>
        public ITabStrip<TModel> SetActiveTabName(string activeTabName)
        {
            this.activeTabName = activeTabName;

            return this;
        }

        /// <summary>
        /// Adds a tab to the tab strip that is rendered from a partial page.
        /// </summary>
        /// <param name="partialPageTab"></param>
        /// <returns></returns>
        public ITabStrip<TModel> AddPartialPageTab(IPartialPageTab partialPageTab)
        {
            return AddPartialPageTab(partialPageTab.Name, partialPageTab.Label,
                partialPageTab.PartialPageName, partialPageTab.Model, partialPageTab.ViewData);
        }

        /// <summary>
        /// Adds a tab to the tab strip that is rendered from a partial page.
        /// </summary>
        /// <param name="name">Name for the emmitted HTML elements.  The header element will have "TabHeader" appended
        /// to the provided name.</param>
        /// <param name="label">The label to display on the tab.</param>
        /// <param name="partialPageName">The name of the partial page that is used to capture content.</param>
        /// <param name="model">Model to pass to the partial view.</param>
        /// <param name="viewData">ViewData to pass to the partial view.</param>
        /// <returns></returns>
        public ITabStrip<TModel> AddPartialPageTab(string name, string label, string partialPageName, object model = null,
                ViewDataDictionary viewData = null)
        {
            AddHtmlTab(new HtmlTab(name, label,
                this.htmlHelper.Partial(partialPageName, model, viewData).ToHtmlString()));

            return this;
        }

        /// <summary>
        /// Adds a tab to the tab strip.
        /// </summary>
        /// <param name="name">Name of the tab strip.</param>
        /// <param name="label">Label text that is displayed.</param>
        /// <param name="htmlContent">The HTML content contained on the tab.</param>
        /// <returns></returns>
        public ITabStrip<TModel> AddTab(string name, string label, string htmlContent)
        {
            AddHtmlTab(new HtmlTab(name, label, htmlContent));
            return this;
        }

        private void AddHtmlTab(HtmlTab htmlTab)
        {
            tabs.Add(htmlTab);

            if (this.activeTabName == null)
            {
                this.activeTabName = htmlTab.Name;
            }
        }

        /// <summary>
        /// Required interface for a tab.
        /// </summary>
        protected interface ITab
        {
            string Name { get; }
            string Label { get; }
            string Content { get; }
        }

        /// <summary>
        /// Tab element and content to display on a tab within a tab strip.
        /// </summary>
        public class HtmlTab : ITab
        {
            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="name">Name for the emmitted HTML elements.  The header element will have "TabHeader" appended
            /// to the provided name.</param>
            /// <param name="label">The label to display on the tab.</param>
            /// <param name="html">Tab content as HTML.</param>
            public HtmlTab(string name, string label, string html)
            {
                this.Name = name;
                this.Label = label;
                this.Html = html;
            }

            /// <summary>
            /// Name for the emmitted HTML elements.  The header element will have "TabHeader" appended
            /// to the provided name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// The label to display on the tab.
            /// </summary>
            public string Label { get; set; }

            /// <summary>
            /// Tab content as HTML.
            /// </summary>
            public string Html { get; set; }

            public string Content
            {
                get
                {
                    return Html;
                }
            }
        }

        /// <summary>
        /// Represents a partial page to display on a tab within a tab strip.
        /// </summary>
        public class PartialPageTab : IPartialPageTab
        {
            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="name">Name for the emmitted HTML elements.  The header element will have "TabHeader" appended
            /// to the provided name.</param>
            /// <param name="label">The label to display on the tab.</param>
            /// <param name="partialPageName">The name of the partial page that is used to capture content.</param>
            /// <param name="model">Model to pass to the partial view.</param>
            /// <param name="viewData">ViewData to pass to the partial view.</param>
            public PartialPageTab(string name, string label, string partialPageName, object model = null,
                ViewDataDictionary viewData = null)
            {
                this.Name = name;
                this.Label = label;
                this.PartialPageName = partialPageName;
                this.Model = model;
                this.ViewData = viewData;
            }

            /// <summary>
            /// Name for the emmitted HTML elements.  The header element will have "TabHeader" appended
            /// to the provided name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// The label to display on the tab.
            /// </summary>
            public string Label { get; set; }

            /// <summary>
            /// The name of the partial page that is used to capture content.
            /// </summary>
            public string PartialPageName { get; set; }

            /// <summary>
            /// Model to pass to the partial view.
            /// </summary>
            public object Model { get; set; }

            /// <summary>
            /// ViewData to pass to the partial view.
            /// </summary>
            public ViewDataDictionary ViewData { get; set; }
        }
    }
}