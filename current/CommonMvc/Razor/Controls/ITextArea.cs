using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMvc.Razor.Controls
{
    public interface ITextArea<TModel, TProperty> : IFormControl<TModel, TProperty, ITextArea<TModel, TProperty>>
    {
        /// <summary>
        /// Sets the number of rows to display for the text area.
        /// </summary>
        /// <param name="rows">Number of rows to display.</param>
        /// <returns></returns>
        ITextArea<TModel, TProperty> SetRows(int rows);

        /// <summary>
        /// Sets the number of columns to display in the text area.  Using the HTML TextArea cols attribute.
        /// Note that in some responsive design layouts such as Twitter Bootstrap this property may not have
        /// any effect, as the width may be specified via CSS and override this value.
        /// </summary>
        /// <param name="columns">The number of columns to display.</param>
        /// <returns></returns>
        ITextArea<TModel, TProperty> SetColumns(int columns);
    }
}
