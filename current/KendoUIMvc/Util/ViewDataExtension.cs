using KendoUIMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KendoUIMvc.Util
{
    public static class ViewDataExtension
    {
        private const string CURRENT_VIEW_SETTINGS = "CurrentViewSettings";

        /// <summary>
        /// Gets the current ViewSettings instance for the current request being processed.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="viewData">ViewDataDictionary that the extension method is extending.</param>
        /// <returns>ViewSettings instance to use for the current view.</returns>
        public static ViewSettings GetViewSettings<TModel>(this ViewDataDictionary<TModel> viewData)
        {
            ViewSettings viewSettings;

            if (viewData.ContainsKey(CURRENT_VIEW_SETTINGS))
            {
                viewSettings = (ViewSettings)viewData[CURRENT_VIEW_SETTINGS];
            }
            else
            {
                viewSettings = new ViewSettings();
                viewData.Add(CURRENT_VIEW_SETTINGS, viewSettings);
            }

            return viewSettings;
        }
    }
}
