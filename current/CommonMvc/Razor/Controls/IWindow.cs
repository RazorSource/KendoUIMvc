using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace CommonMvc.Razor.Controls
{
    public interface IWindow<TModel> : IDisposable
    {
        /// <summary>
        /// Sets the HtmlHelper instance for the current view.
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper that can be used to render content.</param>
        /// <returns></returns>
        IWindow<TModel> SetHtmlHelper(HtmlHelper<TModel> htmlHelper);

        /// <summary>
        /// Sets the AjaxHelper instance for the current view.
        /// </summary>
        /// <param name="ajaxHelper">AjaxHelper that can be used to render content.</param>
        /// <returns></returns>
        IWindow<TModel> SetAjaxHelper(AjaxHelper<TModel> ajaxHelper);

        /// <summary>
        /// Sets the name used to identify the window.
        /// </summary>
        /// <param name="name">Name of the window.</param>
        /// <returns></returns>
        IWindow<TModel> SetName(string name);

        /// <summary>
        /// Sets the width of the window in pixels.  The default value is 600 pixels.
        /// </summary>
        /// <param name="width">Width of the window in pixels.</param>
        /// <returns></returns>
        IWindow<TModel> SetWidth(int width);

        /// <summary>
        /// Sets the title for the window.
        /// </summary>
        /// <param name="title">Title to display.</param>
        /// <returns></returns>
        IWindow<TModel> SetTitle(string title);

        /// <summary>
        /// Sets a flag indicating if the window should be shown when the page is initially loaded.
        /// </summary>
        /// <param name="showOnLoad">True if the window should be displayed when the page initially loads.  The
        /// default value is false.</param>
        /// <returns></returns>
        IWindow<TModel> SetShowOnLoad(bool showOnLoad);

        /// <summary>
        /// Sets ajax action that are used to post data from the form to an ajax request.  Adding the actions will automatically
        /// wrap all elements in the window in an AjaxForm.
        /// </summary>
        /// <param name="actionName">The action used to post data.</param>
        /// <param name="controllerName">The controller used to post data.  If null, the current controller is used.</param>
        /// <param name="ajaxOptions">AjaxOptions to add to the auto-included AjaxForm on the window.</param>
        /// <returns></returns>
        IWindow<TModel> SetAjaxActions(string actionName, string controllerName = null, AjaxOptions ajaxOptions = null);

        /// <summary>
        /// Sets the AjaxForm instance to embed in the window.  SetAjaxActions can be used for basic configuration.  For more detailed AjaxForm configuration
        /// this method can be used.
        /// </summary>
        /// <param name="ajaxForm">The AjaxForm to embed in the window.</param>
        /// <returns></returns>
        //!!IWindow<TModel> SetAjaxForm(AjaxForm<TModel> ajaxForm);

        /// <summary>
        /// Gets the AjaxForm embedded within the window.
        /// </summary>
        /// <returns></returns>
        //!!public AjaxForm<TModel> GetAjaxForm()

        /// <summary>
        /// Sets a flag indicating if the window should open as a modal window.  The default value is false.
        /// </summary>
        /// <param name="modal">True if the window should open as a modal window and disable the page when opening.</param>
        /// <returns></returns>
        IWindow<TModel> SetModal(bool modal);

        /// <summary>
        /// Renders the opening tags for the window.  This call should typically be used within a using block to ensure proper closing
        /// tags are emitted when the window is disposed.
        /// </summary>
        /// <returns></returns>
        IWindow<TModel> RenderBegin();
    }
}
