using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KendoUIMvc.Models
{
    /// <summary>
    /// Contains various settings used when rendering controls in a view.  This class is used to pass
    /// view setting information between controls and containers as they are rendered.
    /// </summary>
    public class ViewSettings
    {
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
            DefaultControlColumnStyle = "col-md-10";
            DefaultLabelColumnStyle = "col-md-2";
            DefaultGroupColumnStyle = null;
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
        /// Gets or sets the default label column style.
        /// </summary>
        public string DefaultLabelColumnStyle
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the default control column style.
        /// </summary>
        public string DefaultControlColumnStyle
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the default group column style.
        /// </summary>
        public string DefaultGroupColumnStyle
        {
            get;
            set;
        }
    }
}
