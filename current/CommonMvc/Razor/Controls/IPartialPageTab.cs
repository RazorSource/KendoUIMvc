using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CommonMvc.Razor.Controls
{
    public interface IPartialPageTab
    {
        /// <summary>
        /// Name for the emmitted HTML elements.  The header element will have "TabHeader" appended
        /// to the provided name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The label to display on the tab.
        /// </summary>
        string Label { get; set; }

        /// <summary>
        /// The name of the partial page that is used to capture content.
        /// </summary>
        string PartialPageName { get; set; }

        /// <summary>
        /// Model to pass to the partial view.
        /// </summary>
        object Model { get; set; }

        /// <summary>
        /// ViewData to pass to the partial view.
        /// </summary>
        ViewDataDictionary ViewData { get; set; }
    }
}
