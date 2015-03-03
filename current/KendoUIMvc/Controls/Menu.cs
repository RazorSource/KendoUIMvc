using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CommonMvc.Razor.Controls;
using CommonMvc.Util;

namespace KendoUIMvc.Controls
{
    public class Menu<TModel> : IMenu<TModel>
    {
        protected HtmlHelper<TModel> htmlHelper;
        protected string name;
        protected IList<IMenuItem<TModel>> childMenus = new List<IMenuItem<TModel>>();

        public Menu(HtmlHelper<TModel> htmlHelper, string name)
        {
            this.htmlHelper = htmlHelper;
            this.name = name;
        }

        /// <summary>
        /// Adds a top level menu item that is not clickable.
        /// </summary>
        /// <param name="name">Name of the menu item.</param>
        /// <param name="text">Text to display on the menu.</param>
        /// <param name="childMenuItems">Collection of child menu items.</param>
        /// <returns></returns>
        public IMenu<TModel> AddMenuItem(string name, string text, IList<IMenuItem<TModel>> childMenuItems = null)
        {
            childMenus.Add(new MenuItem<TModel>(this.htmlHelper, name, text, null, childMenuItems));
            return this;
        }

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
        public IMenu<TModel> AddMenuItem(string name, string text, string action, string controller = null, string area = null,
            IList<IMenuItem<TModel>> childMenuItems = null)
        {
            string url = MvcHtmlHelper.GetActionUrl(this.htmlHelper, action, controller, area);
            childMenus.Add(new MenuItem<TModel>(this.htmlHelper, name, text, url, childMenuItems));

            return this;
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="name">Name of the menu item.</param>
        /// <param name="text">Text to display on the menu item.</param>
        /// <param name="url">Target URL for the menu.</param>
        /// <param name="childMenuItems">Collection of child menu items.</param>
        /// <returns></returns>
        public IMenu<TModel> AddMenuItem(string name, string text, string url, IList<IMenuItem<TModel>> childMenuItems = null)
        {
            childMenus.Add(new MenuItem<TModel>(this.htmlHelper, name, text, url, childMenuItems));

            return this;
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="menuItem">Menu item instance to add.</param>
        /// <returns></returns>
        public IMenu<TModel> AddMenuItem(IMenuItem<TModel> menuItem)
        {
            childMenus.Add(menuItem);

            return this;
        }

        public string GetControlString()
        {
            StringBuilder html = new StringBuilder();

            html.Append(@"
                <ul id=""" + this.name + @""">");

            foreach (MenuItem<TModel> nextMenuItem in childMenus)
            {
                html.Append(nextMenuItem.GetControlString());
            }

            html.Append(@"
                </ul>
                <script type=""text/javascript"">
                $(document).ready(function() {
                    $('#" + this.name + @"').kendoMenu();
                });
            </script>");

            return html.ToString();
        }

        public override string ToString()
        {
            MvcHtmlHelper.WriteUnencodedContent(this.htmlHelper, GetControlString());

            return "";
        }
    }
}
