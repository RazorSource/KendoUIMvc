using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc.Ajax;
using System.Web.Routing;

namespace CommonMvc.Razor.Controls
{
    public interface IAjaxForm<TModel> : IForm<TModel, IAjaxForm<TModel>>
    {
        /// <summary>
        /// Sets AjaxOptions for the form.
        /// </summary>
        /// <param name="ajaxOptions">AjaxOptions instance.</param>
        /// <returns></returns>
        IAjaxForm<TModel> SetAjaxOptions(AjaxOptions ajaxOptions);

        /// <summary>
        /// Sets additional route values to use on the AJAX form.
        /// </summary>
        /// <param name="routeValues">Dictionary of route values.</param>
        /// <returns></returns>
        IAjaxForm<TModel> SetRouteValues(RouteValueDictionary routeValues);
    }
}
