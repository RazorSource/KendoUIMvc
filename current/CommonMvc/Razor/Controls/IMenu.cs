using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMvc.Razor.Controls
{
    public interface IMenu<TModel>
    {
        /// <summary>
        /// Adds a top level menu item that is not selectable, but just serves as a container for other menu items.
        /// </summary>
        /// <param name="name">Name of the menu item.</param>
        /// <param name="text">Text to display on the menu item.</param>
        /// <param name="childMenuItems">Collection of child menu items.</param>
        /// <returns></returns>
        IMenu<TModel> AddMenuItem(string name, string text, IList<IMenuItem<TModel>> childMenuItems = null);

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="name">Name of the menu item.</param>
        /// <param name="text">Text to display on the menu item.</param>
        /// <param name="action">Action to invoke when the menu is clicked.</param>
        /// <param name="controller">Controller containing the action.</param>
        /// <param name="area">Area containing the controller.</param>
        /// <param name="childMenuItems">Collection of child menu items.</param>
        /// <returns></returns>
        IMenu<TModel> AddMenuItem(string name, string text, string action, string controller = null, string area = null,
            IList<IMenuItem<TModel>> childMenuItems = null);


        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="name">Name of the menu item.</param>
        /// <param name="text">Text to display on the menu item.</param>
        /// <param name="url">Target URL for the menu.</param>
        /// <param name="childMenuItems">Collection of child menu items.</param>
        /// <returns></returns>
        IMenu<TModel> AddMenuItem(string name, string text, string url, IList<IMenuItem<TModel>> childMenuItems = null);

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="menuItem">Menu item instance to add.</param>
        /// <returns></returns>
        IMenu<TModel> AddMenuItem(IMenuItem<TModel> menuItem);

        /// <summary>
        /// Gets the HTML necessary to render the control.
        /// </summary>
        /// <returns>The HTML as a string.</returns>
        string GetControlString();
    }
}
