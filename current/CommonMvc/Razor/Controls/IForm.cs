using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonMvc.Models;

namespace CommonMvc.Razor.Controls
{
    public interface IForm<TModel, TControl> : IDisposable where TControl : class
    {
        /// <summary>
        /// Sets the form layout to use for controls on the form.  Vertical will place labels above the form.  Horizontal will
        /// place labels beside controls.
        /// </summary>
        /// <param name="formLayout">Enum value for the form layout.</param>
        /// <returns></returns>
        TControl SetFormLayout(ViewSettings.FormLayoutOption formLayout);

        /// <summary>
        /// Sets the default style to apply to child controls.
        /// </summary>
        /// <param name="defaultControlStyle">Default style to apply to controls.</param>
        /// <returns></returns>
        TControl SetDefaultControlStyle(string defaultControlStyle);

        /// <summary>
        /// Sets the default style to apply to child label elements.
        /// </summary>
        /// <param name="defaultLabelStyle">Default style to apply to labels.</param>
        /// <returns></returns>
        TControl SetDefaultLabelStyle(string defaultLabelStyle);

        /// <summary>
        /// Sets the default style to apply to child group elements.  The group contains the label and the control.
        /// </summary>
        /// <param name="defaultLabelStyle">Default style to apply to control groups.</param>
        /// <returns></returns>
        TControl SetDefaultGroupStyle(string defaultGroupStyle);

        /// <summary>
        /// Sets the style to use for the form.  This can be used to adjust the width of the entire form.
        /// </summary>
        /// <param name="formStyle">The style to apply to the form.</param>
        /// <returns></returns>
        TControl SetFormStyle(string formStyle);

        /// <summary>
        /// Adds an action button to the footer of the form.  Buttons are right aligned to the bottom of the form
        /// and are displayed in the order added to the form.
        /// </summary>
        /// <param name="button">The button to add.</param>
        /// <returns></returns>
        TControl AddFooterActionButton(IButton<TModel> button);

        /// <summary>
        /// Sets a flag indicating if the form should be wrapped in a styled panel.  By default the form is wrapped in a panel.
        /// </summary>
        /// <param name="includePanel">True if the form should be wrapped in a styled panel (default).  False if the form should
        /// not be wrapped in a panel.</param>
        /// <returns></returns>
        TControl SetIncludePanel(bool includePanel);

        /// <summary>
        /// Sets the title to display.
        /// </summary>
        /// <param name="panelTitle">Title to display above the form.</param>
        /// <returns></returns>
        TControl SetTitle(string title);

        /// <summary>
        /// Sets a boolen flag indicating if the return URL should automatically be added as a hidden field to the form, so it is
        /// reposted on save events.  This is intended to allow redirects to the desired return URL to occur after edits are
        /// successfully saved via a post action.  The hidden field will only be added, if the form is loaded with a returnUrl query
        /// string parameter.  The default value is true.
        /// </summary>
        /// <param name="autoPostReturnUrl">Flag indicating if a hidden returnUrl parameter should automatically be set on the form.</param>
        /// <returns></returns>
        TControl SetAutoPostReturnUrl(bool autoPostReturnUrl);

        /// <summary>
        /// Renders the opening tags for the form.  This call should typically be used within a using block to ensure proper closing
        /// tags are emitted when the form is disposed.
        /// </summary>
        /// <returns></returns>
        TControl RenderBegin();

        /// <summary>
        /// Gets the MessageDisplay used to display messages related to the form.
        /// </summary>
        /// <returns></returns>
        IMessageDisplay<TModel> GetMessageDisplay();

        /// <summary>
        /// Writes out the ending tags for the form.
        /// </summary>
        void EndForm();
    }
}
