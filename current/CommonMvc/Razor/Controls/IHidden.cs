using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMvc.Razor.Controls
{
    public interface IHidden<TModel, TProperty>
    {
        /// <summary>
        /// Gets the HTML necessary to render the control.
        /// </summary>
        /// <returns>The HTML as a string.</returns>
        string GetControlString();
    }
}
