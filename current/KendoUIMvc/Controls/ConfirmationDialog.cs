using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using KendoUIMvc.Util;

namespace KendoUIMvc.Controls
{
    public class ConfirmationDialog<TModel>
    {
        protected const string YES = "yes";
        protected const string NO = "no";
        protected const string CANCEL = "cancel";

        protected HtmlHelper<TModel> htmlHelper;
        protected string name;
        protected string title;
        protected string message;
        protected string yesAction;
        protected string noAction;
        protected string cancelAction;
        protected int width;
        protected bool showCancel;
        protected string yesText;
        protected string noText;
        protected string cancelText;

        public ConfirmationDialog(HtmlHelper<TModel> htmlHelper, string name, string title, string message, string yesAction)
        {
            this.htmlHelper = htmlHelper;
            this.name = name;
            this.title = title;
            this.message = message;
            this.yesAction = yesAction;
            this.yesText = "Yes";
            this.noText = "No";
            this.cancelText = "Cancel";
            this.width = 400;
        }

        /// <summary>
        /// Sets the javascript function to invoke when the no option is clicked.  If a cancel action is not
        /// specified the confirmation dialog will just close when cancel is clicked.  If a javascript function is specified,
        /// false should be returned if the confirmation dialog should remain open, or true should be returned to cause
        /// the confirmation dialog to close.
        /// </summary>
        /// <param name="noAction">Javascript function to call.</param>
        /// <returns></returns>
        public ConfirmationDialog<TModel> SetNoAction(string noAction)
        {
            this.noAction = noAction;
            return this;
        }

        /// <summary>
        /// Sets the javascript function to invoke when the cancel option is clicked.  If a cancel action is not
        /// specified the confirmation dialog will just close when cancel is clicked.  If a javascript function is specified,
        /// false should be returned if the confirmation dialog should remain open, or true should be returned to cause
        /// the confirmation dialog to close.
        /// </summary>
        /// <param name="cancelAction">Cancel function to call.</param>
        /// <returns></returns>
        public ConfirmationDialog<TModel> SetCancelAction(string cancelAction)
        {
            this.cancelAction = cancelAction;
            return this;
        }

        /// <summary>
        /// Sets a boolean flag indicating if the cancel button should be shown.  The default is false.
        /// </summary>
        /// <param name="showCancel">True if the cancel button should be shown.</param>
        /// <returns></returns>
        public ConfirmationDialog<TModel> SetShowCancel(bool showCancel)
        {
            this.showCancel = showCancel;
            return this;
        }

        /// <summary>
        /// Sets the width for the confirmation dialog.
        /// </summary>
        /// <param name="width">Width in pixels.</param>
        /// <returns></returns>
        public ConfirmationDialog<TModel> SetWidth(int width)
        {
            this.width = width;
            return this;
        }

        /// <summary>
        /// Sets the text to display on the Yes button.  The default value is Yes.
        /// </summary>
        /// <param name="yesText">Text to display on Yes button.</param>
        /// <returns></returns>
        public ConfirmationDialog<TModel> SetYesText(string yesText)
        {
            this.yesText = yesText;
            return this;
        }

        /// <summary>
        /// Sets the text to display on the No button.  The default value is No.
        /// </summary>
        /// <param name="noText">Text to display on No button.</param>
        /// <returns></returns>
        public ConfirmationDialog<TModel> SetNoText(string noText)
        {
            this.noText = noText;
            return this;
        }

        /// <summary>
        /// Sets the text to display on the Cancel button.  The default value is Cancel.
        /// </summary>
        /// <param name="cancelText">Text to display on Cancel button.</param>
        /// <returns></returns>
        public ConfirmationDialog<TModel> SetCancelText(string cancelText)
        {
            this.cancelText = cancelText;
            return this;
        }

        public string GetControlString()
        {
            StringBuilder html = new StringBuilder();

            html.Append(@"
                <div id=""" + this.name + @""" style=""display: none;"">
                    <div class=""km-margin-bottom"">" + this.message + @"</div>
                    <div class=""center-block"">");

            // Add the Yes Button.
            Button<TModel> yesButton = new Button<TModel>(this.htmlHelper, this.name + "_buttonYes", this.yesText)
                .SetOnClick(GetWrappedActionFunctionName(YES) + "()");
            html.Append(@"
                " + yesButton.GetControlString());

            // Add the No Button.
            Button<TModel> noButton = new Button<TModel>(this.htmlHelper, this.name + "_buttonNo", this.noText)
                .SetOnClick(this.noAction != null ? (GetWrappedActionFunctionName(NO) + "()") : ("hide" + this.name + @"()"));
            html.Append(@"
                " + noButton.GetControlString());

            if (showCancel)
            {
                // Add the Cancel Button.
                Button<TModel> cancelButton = new Button<TModel>(this.htmlHelper, this.name + "_buttonCancel", this.cancelText)
                    .SetOnClick(this.cancelAction != null ? (GetWrappedActionFunctionName(CANCEL) + "()") : ("hide" + this.name + @"()"));
                html.Append(@"
                " + cancelButton.GetControlString());
            }

            html.Append(@"
                    </div>
                </div>
                <script type=""text/javascript"">
                    function show" + this.name + @"() {
                        $('#" + this.name + @"').data('kendoWindow').open();
                    }

                    function hide" + this.name + @"() {
                        $('#" + this.name + @"').data('kendoWindow').close();
                    }");

            html.Append(@"
                    " + GetWrappedActionFunction(YES, this.yesAction));

            if (this.noAction != null)
            {
                html.Append(@"
                    " + GetWrappedActionFunction(NO, this.noAction));
            }

            if (this.cancelAction != null)
            {
                html.Append(@"
                    " + GetWrappedActionFunction(CANCEL, this.cancelAction));
            }

            html.Append(@"
                    $(document).ready(function() {
                        var kendoWindowDiv = $('#" + this.name + @"');
    
                        if (!kendoWindowDiv.data('kendoWindow')) {
                            $('#" + this.name + @"').kendoWindow({
                                " + GetAttributes() + @"
                            }).data('kendoWindow').center();
                        }
                    });
                </script>");

            return html.ToString();
        }

        /// <summary>
        /// Gets the name of the function used to call a provided action.  If the wrapped function returns true the confirmation dialog will close, otherwise
        /// the confirmation dialog will remain open.
        /// </summary>
        /// <param name="actionToCall">Action type to call.</param>
        /// <returns></returns>
        protected string GetWrappedActionFunctionName(string actionToCall)
        {
            return this.name + "_call" + actionToCall;
        }

        protected string GetWrappedActionFunction(string actionToCall, string functionToCall)
        {
            return @"
                function " + GetWrappedActionFunctionName(actionToCall) + @"() {
                    if (" + functionToCall + @") {
                        $('#" + this.name + @"').data('kendoWindow').close();
                    }
                }";
        }

        protected string GetAttributes()
        {
            StringBuilder attributes = new StringBuilder();

//            if (this.ajaxForm != null && this.ajaxForm.GetNotification() != null)
//            {
//                // Hide any error message when the window is closed.
//                attributes.Append(@"
//                            close: function () { " + this.ajaxForm.GetNotification().GetCallHideScript() + @" },");
//            }

            attributes.Append(@"
                            width: '" + this.width + @"',
                            title: '" + this.title + @"',
                            modal: true,
                            visible: false");

            return attributes.ToString();
        }

        /// <summary>
        /// Gets the javascript string used to invoke the genererated "show" javascript function.
        /// </summary>
        /// <returns>The javascript function call as a string.</returns>
        public string GetCallShowScript()
        {
            return "show" + this.name + "();";
        }

        /// <summary>
        /// Gets the javascript string used to invoke the genererated "hide" javascript function.
        /// </summary>
        /// <returns>The javascript function call as a string.</returns>
        public string GetCallHideScript()
        {
            return "hide" + this.name + "();";
        }

        public override string ToString()
        {
            MvcHtmlHelper.WriteUnencodedContent(this.htmlHelper, GetControlString());

            return "";
        }
    }
}
