using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CommonMvc.Razor.Controls
{
    public interface ITabStrip<TModel>
    {
        /// <summary>
        /// Builds the HTML necessary to render the control.
        /// </summary>
        /// <returns>An MvcHtmlString that contains all HTML and script necessary to render the tab strip control.</returns>
        MvcHtmlString Render();

        /// <summary>
        /// Sets the name of the tab that should initially be active when the page first loads.  By default
        /// the first tab loaded is set as the default tab.
        /// </summary>
        /// <param name="activeTabName"></param>
        /// <returns></returns>
        ITabStrip<TModel> SetActiveTabName(string activeTabName);

        /// <summary>
        /// Adds a tab to the tab strip that is rendered from a partial page.
        /// </summary>
        /// <param name="partialPageTab"></param>
        /// <returns></returns>
        ITabStrip<TModel> AddPartialPageTab(IPartialPageTab partialPageTab);

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
        ITabStrip<TModel> AddPartialPageTab(string name, string label, string partialPageName, object model = null,
                ViewDataDictionary viewData = null);

        /// <summary>
        /// Adds a tab to the tab strip.
        /// </summary>
        /// <param name="name">Name of the tab strip.</param>
        /// <param name="label">Label text that is displayed.</param>
        /// <param name="htmlContent">The HTML content contained on the tab.</param>
        /// <returns></returns>
        ITabStrip<TModel> AddTab(string name, string label, string htmlContent);
    }
}
