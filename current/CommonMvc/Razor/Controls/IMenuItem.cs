using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMvc.Razor.Controls
{
    public interface IMenuItem<TModel>
    {
        /// <summary>
        /// Sets the name of the menu item.  The name can be used to identify the menu item client side.
        /// </summary>        
        string Name { get; set; }

        /// <summary>
        /// Sets the text to display on the menu.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Sets the target URL for the menu.
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// Adds a child menu item.
        /// </summary>
        /// <param name="menuItem"></param>
        IMenuItem<TModel> AddMenuItem(IMenuItem<TModel> menuItem);

        /// <summary>
        /// Gets the HTML for the menu item and any containing menu items.
        /// </summary>
        /// <returns>HTML used to render the menu item.</returns>
        string GetControlString();
    }
}
