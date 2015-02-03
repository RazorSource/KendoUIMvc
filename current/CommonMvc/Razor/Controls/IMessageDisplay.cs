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
    }
}
