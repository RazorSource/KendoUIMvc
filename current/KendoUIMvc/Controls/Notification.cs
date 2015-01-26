using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using KendoUIMvc.Util;

namespace KendoUIMvc.Controls
{
    public class Notification<TModel>
    {
        protected HtmlHelper<TModel> htmlHelper;
        protected string name;
        protected string appendTo;
        protected KendoUIMvc.Controls.Notification.NotificationType notificationType;
        protected int? autoHideAfter;
        protected string width;
        protected KendoUIMvc.Controls.Notification.StackingType? stackingType;
        protected bool canGrow;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper instance used to write view elements.</param>
        /// <param name="name">Name of the notification.</param>
        public Notification(HtmlHelper<TModel> htmlHelper, string name)
        {
            this.htmlHelper = htmlHelper;
            this.name = name;

            this.notificationType = KendoUIMvc.Controls.Notification.NotificationType.info;
            this.canGrow = true;
        }

        /// <summary>
        /// Sets the element that notification should be appended to when displayed.
        /// </summary>
        /// <param name="appendTo">HTML ID of the element to append the notification to.</param>
        /// <returns></returns>
        public Notification<TModel> SetAppendTo(string appendTo)
        {
            this.appendTo = appendTo;
            return this;
        }

        /// <summary>
        /// Sets the default notification type, which controls the styling of the notification control.  The default notification type is info.
        /// The notification type can also be overridden in the generated show method.
        /// </summary>
        /// <param name="notificationType">Notification type.</param>
        /// <returns></returns>
        public Notification<TModel> SetNotificationType(KendoUIMvc.Controls.Notification.NotificationType notificationType)
        {
            this.notificationType = notificationType;
            return this;
        }

        /// <summary>
        /// Sets the time in milliseconds in which the notification should automatically hide.  Setting the value to zero will cause the notifcation
        /// to not automatically hide.  The default value is 5 seconds (5000).
        /// </summary>
        /// <param name="autoHideAfter">The time in milliseconds to wait before the notification automatically hides.  Setting the value to zero disables the auto-hide functionality.</param>
        /// <returns></returns>
        public Notification<TModel> SetAutoHideAfter(int autoHideAfter)
        {
            this.autoHideAfter = autoHideAfter;
            return this;
        }

        /// <summary>
        /// Sets the stacking type that is used to display the notification relative to the appendTo element.
        /// </summary>
        /// <param name="stackingType">Stacking type value.</param>
        /// <returns></returns>
        public Notification<TModel> SetStacking(KendoUIMvc.Controls.Notification.StackingType stackingType)
        {
            this.stackingType = stackingType;
            return this;
        }

        /// <summary>
        /// Sets the width of the notification.  Desired units may specified with the value.
        /// </summary>
        /// <param name="width">The width of the notification.</param>
        /// <returns></returns>
        public Notification<TModel> SetWidth(string width)
        {
            this.width = width;
            return this;
        }

        /// <summary>
        /// Sets a boolean flag indicating if the notification can grow in size to accommodate longer messages.  The default value for this is true.
        /// </summary>
        /// <param name="canGrow">True if the notification can grow.</param>
        /// <returns></returns>
        public Notification<TModel> SetCanGrow(bool canGrow)
        {
            this.canGrow = canGrow;
            return this;
        }

        protected virtual string GetAttributes()
        {
            StringBuilder attributes = new StringBuilder();
            bool firstAttribute = true;

            if (this.appendTo != null)
            {
                attributes.Append(@"appendTo: '" + this.appendTo + "'");
                firstAttribute = false;
            }

            if (this.autoHideAfter != null)
            {
                attributes.Append((firstAttribute ? "" : ",") + @"autoHideAfter: " + this.autoHideAfter);
                firstAttribute = false;
            }

            if (this.width != null)
            {
                // Note: width can be a string value with units of measurement.
                attributes.Append((firstAttribute ? "" : ",") + @"width: '" + this.width + "'");
                firstAttribute = false;
            }

            if (this.stackingType != null)
            {
                attributes.Append((firstAttribute ? "" : ",") + @"stacking: '" + Notification.GetStackingTypeAsString(this.stackingType.Value) + "'");
                firstAttribute = false;
            }

            if (this.canGrow)
            {
                attributes.Append((firstAttribute ? "" : ",") + @"
                    templates: [{
                            type: 'info',
                            template: $('#" + this.name + @"canGrowTemplate').html()
                        }, {
                            type: 'error',
                            template: $('#" + this.name + @"canGrowTemplate').html()
                        }, {
                            type: 'success',
                            template: $('#" + this.name + @"canGrowTemplate').html()
                        }, {
                            type: 'warning',
                            template: $('#" + this.name + @"canGrowTemplate').html()
                        }]

                ");
            }

            return attributes.ToString();
        }

        /// <summary>
        /// Gets the HTML used to render the control.
        /// </summary>
        /// <returns></returns>
        public string GetControlString()
        {
            StringBuilder html = new StringBuilder();
            string popupVariableName = this.name + "_popup";

            if (this.canGrow)
            {
                html.Append(@"
                <script id=""" + this.name + @"canGrowTemplate"" type=""text/x-kendo-template"">
                    <div class=""k-notification-wrap"">
                        <span class=""k-icon k-i-note"">error</span>
                        <span style=""white-space: normal;"">#= message #</span>
                    </div>  
                </script>
                ");
            }

            html.Append(@"
                <span id=""" + this.name + @"""></span>");

            string messageParam;

            if (this.canGrow)
            {
                // If using a template the message text must be named.
                messageParam = "{ message: message }";
            }
            else
            {
                messageParam = "message";
            }

            html.Append(@"
                <script type=""text/javascript"">
                    var " + popupVariableName + @" = $('#" + this.name + @"')
                        .kendoNotification({
                            " + GetAttributes() + @" })
                            .data('kendoNotification');

                    function show" + this.name + @"(message, notificationType) {
                        if (notificationType == undefined) {
                            notificationType = '" + Notification.GetNotificationTypeAsString(this.notificationType) + @"';
                        }

                        " + popupVariableName + @".show(" + messageParam + @", notificationType);
                    }

                    function hide" + this.name + @"() {
                        " + popupVariableName + @".hide();
                    }
                </script>");
            return html.ToString();
        }

        public override string ToString()
        {
            MvcHtmlHelper.WriteUnencodedContent(this.htmlHelper, GetControlString());

            return "";
        }
    }

    public class Notification
    {
        public enum NotificationType
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

        public static string GetNotificationTypeAsString(NotificationType notificationType)
        {
            switch (notificationType)
            {
                case NotificationType.success:
                    return "success";
                case NotificationType.warning:
                    return "warning";
                case NotificationType.error:
                    return "error";
                default:
                    return "info";
            }
        }

        public static string GetStackingTypeAsString(StackingType stackingType)
        {
            switch (stackingType)
            {
                case StackingType.up:
                    return "up";
                case StackingType.right:
                    return "right";
                case StackingType.left:
                    return "left";
                default:
                    return "down";
            }
        }
    }
}