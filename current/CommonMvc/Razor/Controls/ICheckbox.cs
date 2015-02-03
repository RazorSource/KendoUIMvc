using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMvc.Razor.Controls
{
    public interface ICheckbox<TModel, TProperty> : IFormControl<TModel, TProperty, ICheckbox<TModel, TProperty>>
    {
        /// <summary>
        /// Sets a flag indicating if the checkbox should be checked by default.  The default value is false.
        /// </summary>
        /// <param name="checkedByDefault">True if the checkbox should initially be checked.</param>
        /// <returns></returns>
        ICheckbox<TModel, TProperty> SetCheckedByDefault(bool checkedByDefault);

        /// <summary>
        /// Forces the label to be shown on the right hand side of the checkbox.  The default value is false.
        /// </summary>
        /// <param name="showLabelRight">True if the label should be shown to the right of the checkbox.</param>
        /// <returns></returns>
        ICheckbox<TModel, TProperty> SetShowLabelRight(bool showLabelRight);
    }
}
