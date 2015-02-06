using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMvc.Models
{
    /// <summary>
    /// Contains various settings used when rendering controls in a view.  This class is used to pass
    /// view setting information between controls and containers as they are rendered.
    /// </summary>
    public class ViewSettings
    {
        /// <summary>
        /// Constant for the returnUrl param that is used between different pages to specify the URL that should be used
        /// for redirecting upon the completion of the page.
        /// </summary>
        public const string RETURN_URL_PARAM = "returnUrl";

        /// <summary>
        /// Layout options for form controls.
        /// </summary>
        public enum FormLayoutOption
        {
            /// <summary>
            /// Horizontal layout in which labels are beside their corresponding control.
            /// </summary>
            Horizontal,
            /// <summary>
            /// Vertical layout in which labels are above their corresponding control.
            /// </summary>
            Vertical
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ViewSettings()
        {
            RestoreDefaultViewSettings();
        }

        /// <summary>
        /// Restores default view settings.
        /// </summary>
        public void RestoreDefaultViewSettings()
        {
            FormLayout = FormLayoutOption.Horizontal;
            DefaultControlStyle = "col-md-10";
            DefaultLabelStyle = "col-md-2";
            DefaultGroupStyle = null;
        }

        /// <summary>
        /// Gets or sets the current Form Layout option.
        /// </summary>
        public FormLayoutOption FormLayout
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the default label style.
        /// </summary>
        public string DefaultLabelStyle
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the default control style.
        /// </summary>
        public string DefaultControlStyle
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the default group style.
        /// </summary>
        public string DefaultGroupStyle
        {
            get;
            set;
        }
    }
}
