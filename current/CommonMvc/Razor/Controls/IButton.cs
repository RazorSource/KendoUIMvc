using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMvc.Razor.Controls
{
    public interface IButton<TModel>
    {
        /// <summary>
        /// Sets the name of the button.
        /// </summary>
        /// <param name="name">Name for the control instance.</param>
        /// <returns></returns>
        IButton<TModel> SetName(string name);

        /// <summary>
        /// Sets the text to display on the button.
        /// </summary>
        /// <param name="label">Text to display.</param>
        /// <returns></returns>
        IButton<TModel> SetLabel(string label);

        /// <summary>
        /// Sets a flag indicating if the button should be invoked when the return key is pressed.
        /// </summary>
        /// <param name="acceptsReturn">True if the return key invoke the button action.</param>
        /// <returns></returns>
        IButton<TModel> SetAcceptsReturn(bool acceptsReturn);

        /// <summary>
        /// Sets a flag indicating if this should be a submit button.
        /// </summary>
        /// <param name="submit">True if the button should be marked as a submit button for a form.</param>
        /// <returns></returns>
        IButton<TModel> SetSubmit(bool submit);

        /// <summary>
        /// Sets the javascript function to call on the click event of the button.
        /// </summary>
        /// <param name="onClick">Function to call.  As an example DoAction()</param>
        /// <returns></returns>
        IButton<TModel> SetOnClick(string onClick);

        /// <summary>
        /// Sets a flag indicating if this button should be styled as a primary button.
        /// </summary>
        /// <param name="primary">True if the button should be displayed as a primary button.</param>
        /// <returns></returns>
        IButton<TModel> SetPrimary(bool primary);

        /// <summary>
        /// Adds a CSS class to add to the button. 
        /// </summary>
        /// <param name="cssClass">CSS class to add.</param>
        /// <returns></returns>
        IButton<TModel> AddClass(string cssClass);

        /// <summary>
        /// Sets the Icon to use with the button.
        /// </summary>
        /// <param name="icon">Name of the icon.</param>
        /// <returns></returns>
        IButton<TModel> SetIcon(string icon);

        /// <summary>
        /// Gets the HTML necessary to render the control.
        /// </summary>
        /// <returns>The HTML as a string.</returns>
        string GetControlString();
    }
}
