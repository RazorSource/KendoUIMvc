using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KendoUIMvc.Controls
{
    /// <summary>
    /// Represents on menu item within the menu.
    /// </summary>
    public class MenuItem<TModel>
    {
        protected HtmlHelper<TModel> htmlHelper;
        protected List<MenuItem<TModel>> childMenus = new List<MenuItem<TModel>>();

        public MenuItem(HtmlHelper<TModel> htmlHelper)
        {
            this.htmlHelper = htmlHelper;
        }

        public MenuItem(HtmlHelper<TModel> htmlHelper, string name, string text, string url, IList<MenuItem<TModel>> childMenuItems)
        {
            this.htmlHelper = htmlHelper;
            this.Name = name;
            this.Text = text;
            this.Url = url;

            if (childMenuItems != null)
            {
                this.childMenus.AddRange(childMenuItems);
            }
        }

        /// <summary>
        /// Sets the name of the menu item.  The name can be used to identify the menu item client side.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Sets the text to display on the menu.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Sets the target URL for the menu.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Adds a child menu item.
        /// </summary>
        /// <param name="menuItem"></param>
        public MenuItem<TModel> AddMenuItem(MenuItem<TModel> menuItem)
        {
            this.childMenus.Add(menuItem);
            return this;
        }

        /// <summary>
        /// Gets the HTML for the menu item and any containing menu items.
        /// </summary>
        /// <returns>HTML used to render the menu item.</returns>
        public string GetControlString()
        {
            StringBuilder html = new StringBuilder();

            html.Append(@"
                    <li id=""" + this.Name + @""">");

            if (this.Url != null)
            {
                html.Append(@"
                        <a href=""" + this.Url + @""">" + this.Text + "</a>");
            }
            else
            {
                html.Append(@"
                        " + this.Text);
            }

            // If there are chlid menu items, render out the child menus.
            if (this.childMenus.Count > 0)
            {
                html.Append(@"
                        <ul>");

                foreach (MenuItem<TModel> nextMenuItem in childMenus)
                {
                    html.Append(nextMenuItem.GetControlString());
                }

                html.Append(@"
                        </ul>");
            }

            html.Append(@"
                    </li>");

            return html.ToString();
        }
    }
}
