using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMvc.Razor.Controls
{
    public interface IFormControl<TModel, TProperty, TControl> where TControl : class
    {
        /// <summary>
        /// Sets a override label text for the control.  For bound controls the label is derived from the model bound by the expression.
        /// If the control is not bound to a model by an expression, the LabelText can be used to display a label with the unbound control.
        /// </summary>
        /// <returns></returns>
        TControl SetLabelText(string labelText);

        /// <summary>
        /// Adds a CSS class to the label tag.
        /// </summary>
        /// <param name="styleClass">CSS class name to add.</param>
        /// <returns></returns>
        TControl AddLabelClass(string styleClass);

        /// <summary>
        /// Sets the label style.
        /// </summary>
        /// <param name="labelStyle">The label style.</param>
        /// <returns></returns>
        TControl SetLabelStyle(string labelStyle);

        /// <summary>
        /// Sets the control style.
        /// </summary>
        /// <param name="controlStyle">The control style.</param>
        /// <returns></returns>
        TControl SetControlStyle(string controlStyle);

        /// <summary>
        /// Sets the group style for the form group element; which contains the label, control and any related validation.
        /// </summary>
        /// <param name="groupStyle">The group style.</param>
        /// <returns></returns>
        TControl SetGroupStyle(string groupStyle);

        /// <summary>
        /// Adds a CSS class to the control tag.
        /// </summary>
        /// <param name="styleClass">CSS class name to add.</param>
        /// <returns></returns>
        TControl AddControlClass(string styleClass);
    }
}
