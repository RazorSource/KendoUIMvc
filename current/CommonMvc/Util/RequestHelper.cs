using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CommonMvc.Models;
using System.Collections;

namespace CommonMvc.Util
{
    /// <summary>
    /// Utility class used to share information for a single request across all views and controls
    /// used within the request.
    /// </summary>
    public class RequestHelper
    {
        private const string CURRENT_VIEW_SETTINGS = "CurrentViewSettings";

        /// <summary>
        /// Gets the current ViewSettings instance for the current request being processed.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="viewData">ViewDataDictionary that the extension method is extending.</param>
        /// <returns>ViewSettings instance to use for the current view.</returns>
        public static ViewSettings ViewSettings
        {
            get
            {
                IDictionary requestItems = System.Web.HttpContext.Current.Items;

                ViewSettings viewSettings;

                if (requestItems.Contains(CURRENT_VIEW_SETTINGS))
                {
                    viewSettings = (ViewSettings)requestItems[CURRENT_VIEW_SETTINGS];
                }
                else
                {
                    viewSettings = new ViewSettings();
                    requestItems.Add(CURRENT_VIEW_SETTINGS, viewSettings);
                }

                return viewSettings;
            }
        }
    }
}
