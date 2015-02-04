using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMvc.Razor.Controls
{
    public interface IMessageDisplay<TModel>
    {
        /// <summary>
        /// Sets the element that message display should be appended to when displayed.
        /// </summary>
        /// <param name="appendTo">HTML ID of the element to append the notification to.</param>
        /// <returns></returns>
        IMessageDisplay<TModel> SetAppendTo(string appendTo);

        /// <summary>
        /// Sets the default message type, which controls the styling of the message display control.  The default message type is info.
        /// The message type can also be overridden in the generated show method.
        /// </summary>
        /// <param name="messageType">Message type.</param>
        /// <returns></returns>
        IMessageDisplay<TModel> SetMessageType(MessageType messageType);


        /// <summary>
        /// Sets the time in milliseconds in which the message display should automatically hide.  Setting the value to zero will cause the message display
        /// to not automatically hide.  The default value is 5 seconds (5000).
        /// </summary>
        /// <param name="autoHideAfter">The time in milliseconds to wait before the message display automatically hides.  Setting the value to zero disables the auto-hide functionality.</param>
        /// <returns></returns>
        IMessageDisplay<TModel> SetAutoHideAfter(int autoHideAfter);

        /// <summary>
        /// Sets the stacking type that is used to display the message display relative to the appendTo element.
        /// </summary>
        /// <param name="stackingType">Stacking type value.</param>
        /// <returns></returns>
        IMessageDisplay<TModel> SetStacking(StackingType stackingType);

        /// <summary>
        /// Sets the width of the message display.  Desired units may specified with the value.
        /// </summary>
        /// <param name="width">The width of the message display.</param>
        /// <returns></returns>
        IMessageDisplay<TModel> SetWidth(string width);

        /// <summary>
        /// Sets a boolean flag indicating if the message display can grow in size to accommodate longer messages.  The default value for this is true.
        /// </summary>
        /// <param name="canGrow">True if the notification can grow.</param>
        /// <returns></returns>
        IMessageDisplay<TModel> SetCanGrow(bool canGrow);

        /// <summary>
        /// Gets the javascript string used to invoke the genererated "ShowAjaxResponse" javascript function.  This is intended to 
        /// display an AJAX response from the server.
        /// </summary>
        /// <param name="response">The javascript variable containing the response.</param>
        /// <param name="defaultMessage">The default message to display if a message cannot be extracted from the server response.</param>
        /// <returns>The javascript function call as a string.</returns>
        string GetCallShowAjaxResponseScript(string response, string defaultMessage = "Error Completing Request.");

        /// <summary>
        /// Gets the javascript string used to invoke the genererated "hide" javascript function.
        /// </summary>
        /// <returns>The javascript function call as a string.</returns>
        string GetCallHideScript();

        /// <summary>
        /// Gets the javascript string used to invoke the genererated "show" javascript function.  This method shows a method from local
        /// javascript variables.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="notificationType">Notification type.  This is used to style the notification.  The parameter default value of
        /// undefined will cause the default notification type to be used.</param>
        /// <returns>The javascript function call as a string.</returns>
        string GetCallShowScript(string message, string notificationType = "undefined");

        /// <summary>
        /// Gets the HTML used to render the control.
        /// </summary>
        /// <returns>The HTML as a string.</returns>
        string GetControlString();
    }

    public enum MessageType
    {
        info,
        success,
        warning,
        error
    }

    public enum StackingType
    {
        up,
        right,
        down,
        left
    }
}
