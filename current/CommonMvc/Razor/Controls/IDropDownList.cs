using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CommonMvc.Razor.Controls
{
    public interface IDropDownList<TModel, TProperty> : IFormControl<TModel, TProperty, IDropDownList<TModel, TProperty>>
    {
        /// <summary>
        /// Sets the server side data source to use to populate the drop down list options.
        /// </summary>
        /// <param name="dataSource">List of select list items to use for the drop down options.</param>
        /// <returns></returns>
        IDropDownList<TModel, TProperty> SetDataSource(IList<SelectListItem> dataSource);
    }
}
