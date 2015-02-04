using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMvc.Razor.Controls
{
    public interface IPanel<TModel> : IDisposable
    {
        /// <summary>
        /// Sets the title to display on the panel.
        /// </summary>
        /// <param name="title">Title to display.</param>
        /// <returns></returns>
        IPanel<TModel> SetTitle(string title);

        /// <summary>
        /// Sets the style to use for the panel.  This can be used to adjust the width of the entire panel.
        /// </summary>
        /// <param name="panelStyle">The style to apply to the panel.  The default value
        /// is to not have a style on the panel (null).</param>
        /// <returns></returns>
        IPanel<TModel> SetPanelStyle(string panelStyle);

        /// <summary>
        /// Renders the opening tags for the panel.  This call should typically be used within a using block to ensure proper closing
        /// tags are emitted when the panel is disposed.
        /// </summary>
        /// <returns></returns>
        IPanel<TModel> RenderBegin();

        /// <summary>
        /// Writes out the ending tags for the panel.
        /// </summary>
        void EndPanel();
    }
}
