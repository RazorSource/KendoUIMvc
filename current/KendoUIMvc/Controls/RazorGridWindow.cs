using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KendoUIMvc.Controls
{
    public class RazorGridWindow<TModel> : Window<TModel>
    {
        protected RazorGrid<TModel> parentGrid;

        public RazorGridWindow(HtmlHelper<TModel> htmlHelper, AjaxHelper<TModel> ajaxHelper, string name,
             RazorGrid<TModel> parentGrid)
            : base(htmlHelper, ajaxHelper, name)
        {
            this.parentGrid = parentGrid;
        }

        /// <summary>
        /// Gets the RazorGrid that is used as the parent grid for this window.
        /// </summary>
        /// <returns></returns>
        public RazorGrid<TModel> GetParentGrid()
        {
            return this.parentGrid;
        }
    }
}
