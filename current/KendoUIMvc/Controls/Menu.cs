using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using KendoUIMvc.Util;

namespace KendoUIMvc.Controls
{
    public class Menu<TModel>
    {
        protected HtmlHelper<TModel> htmlHelper;
        protected string name;
        protected IList<MenuItem<TModel>> childMenus = new List<MenuItem<TModel>>();

        public Menu(HtmlHelper<TModel> htmlHelper, string name)
        {
            this.htmlHelper = htmlHelper;
            this.name = name;
        }

        public Menu<TModel> AddParentMenuItem(string name, string text, IList<MenuItem<TModel>> childMenuItems = null)
        {
            childMenus.Add(new MenuItem<TModel>(this.htmlHelper, name, text, null, childMenuItems));
            return this;
        }

        public Menu<TModel> AddMenuItem(string name, string text, string action, string controller = null, string area = null,
            IList<MenuItem<TModel>> childMenuItems = null)
        {
            string url = MvcHtmlHelper.GetActionUrl(this.htmlHelper, action, controller, area);
            childMenus.Add(new MenuItem<TModel>(this.htmlHelper, name, text, url, childMenuItems));

            return this;
        }

        public Menu<TModel> AddMenuItem(string name, string text, string url, IList<MenuItem<TModel>> childMenuItems = null)
        {
            childMenus.Add(new MenuItem<TModel>(this.htmlHelper, name, text, url, childMenuItems));

            return this;
        }

        public Menu<TModel> AddMenuItem(MenuItem<TModel> menuItem)
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
