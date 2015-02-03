using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMvc.Razor.Controls
{
    public interface IConfirmationDialog<TModel>
    {
        /// <summary>
        /// Sets the javascript function to invoke when the no option is clicked.  If a cancel action is not
        /// specified the confirmation dialog will just close when cancel is clicked.  If a javascript function is specified,
        /// false should be returned if the confirmation dialog should remain open, or true should be returned to cause
        /// the confirmation dialog to close.
        /// </summary>
        /// <param name="noAction">Javascript function to call.</param>
        /// <returns></returns>
        IConfirmationDialog<TModel> SetNoAction(string noAction);

        /// <summary>
        /// Sets the javascript function to invoke when the cancel option is clicked.  If a cancel action is not
        /// specified the confirmation dialog will just close when cancel is clicked.  If a javascript function is specified,
        /// false should be returned if the confirmation dialog should remain open, or true should be returned to cause
        /// the confirmation dialog to close.
        /// </summary>
        /// <param name="cancelAction">Cancel function to call.</param>
        /// <returns></returns>
        IConfirmationDialog<TModel> SetCancelAction(string cancelAction);

        /// <summary>
        /// Sets a boolean flag indicating if the cancel button should be shown.  The default is false.
        /// </summary>
        /// <param name="showCancel">True if the cancel button should be shown.</param>
        /// <returns></returns>
        IConfirmationDialog<TModel> SetShowCancel(bool showCancel);

        /// <summary>
        /// Sets the width for the confirmation dialog.
        /// </summary>
        /// <param name="width">Width in pixels.</param>
        /// <returns></returns>
        IConfirmationDialog<TModel> SetWidth(int width);

        /// <summary>
        /// Sets the text to display on the Yes button.  The default value is Yes.
        /// </summary>
        /// <param name="yesText">Text to display on Yes button.</param>
        /// <returns></returns>
        IConfirmationDialog<TModel> SetYesText(string yesText);

        /// <summary>
        /// Sets the text to display on the No button.  The default value is No.
        /// </summary>
        /// <param name="noText">Text to display on No button.</param>
        /// <returns></returns>
        IConfirmationDialog<TModel> SetNoText(string noText);

        /// <summary>
        /// Sets the text to display on the Cancel button.  The default value is Cancel.
        /// </summary>
        /// <param name="cancelText">Text to display on Cancel button.</param>
        /// <returns></returns>
        IConfirmationDialog<TModel> SetCancelText(string cancelText);

        /// <summary>
        /// Gets the HTML necessary to render the control.
        /// </summary>
        /// <returns>The HTML as a string.</returns>
        string GetControlString();

        /// <summary>
        /// Gets the javascript string used to invoke the genererated "show" javascript function.
        /// </summary>
        /// <returns>The javascript function call as a string.</returns>
        string GetCallShowScript();

        /// <summary>
        /// Gets the javascript string used to invoke the genererated "hide" javascript function.
        /// </summary>
        /// <returns>The javascript function call as a string.</returns>
        string GetCallHideScript();
    }
}
