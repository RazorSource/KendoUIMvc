using KendoUIMvc.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Linq.Expressions;

namespace KendoUIMvc.Controls
{
    public class RazorGrid<TModel>
    {
        protected const string EDIT_COLUMN_ID = "_edit";
        protected const string DELETE_COLUMN_ID = "_delete";

        protected HtmlHelper<TModel> htmlHelper;
        protected AjaxHelper<TModel> ajaxHelper;
        protected string name;
        protected IList<Column> columns = new List<Column>();
        protected string remoteDataSourceUrl;
        protected string remoteEditUrl;
        protected string remoteCreateUrl;
        protected string remoteDeleteUrl;
        protected bool pageData;
        protected int pageSize;
        protected bool serverPaging;
        protected string containerColumnStyle;
        protected string keyProperty;
        protected RazorGridWindow<TModel> editWindow;
        protected RazorGridWindow<TModel> addWindow;
        protected string deleteConfirmationTitle = "Confirm Delete";
        protected string deleteConfirmationMessage = "Are you sure you want to delete this item?";
        protected string editTooltip = "edit";
        protected string deleteTooltip = "delete";

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="htmlHelper">MVC HTML Helper used to generate controls.</param>
        /// <param name="name">Name of the grid.</param>
        /// <param name="keyProperty">The key property on the model.</param>
        public RazorGrid(HtmlHelper<TModel> htmlHelper, AjaxHelper<TModel> ajaxHelper, string name, string keyProperty)
        {
            this.htmlHelper = htmlHelper;
            this.ajaxHelper = ajaxHelper;
            this.name = name;

            // By default page data.
            this.pageData = true;
            this.pageSize = 10;
            this.serverPaging = true;
            this.containerColumnStyle = "col-md-12";
            this.keyProperty = keyProperty;
        }

        /// <summary>
        /// Adds a new column to the grid.
        /// </summary>
        /// <param name="name">Name of the property to bind from the model data.</param>
        /// <param name="label">Label to display on the column.</param>
        /// <param name="width">Optional fixed column width to apply to the column.</param>
        /// <returns></returns>
        public RazorGrid<TModel> AddColumn(string name, string label, int? width = null)
        {
            BoundColumn boundColumn = new BoundColumn(label, name);
            boundColumn.Width = width;
            columns.Add(boundColumn);

            return this;
        }

        /// <summary>
        /// Adds a new column to the grid.
        /// </summary>
        /// <typeparam name="TProperty">Type of the property being bound.</typeparam>
        /// <param name="expression">The expression to bind.</param>
        /// <param name="width">Optional fixed column width to apply to the column.</param>
        /// <param name="label">Optonal label override for the expression.  If the label override is not provided, the label
        /// will be extracted from the bound model property.</param>
        /// <returns></returns>
        public RazorGrid<TModel> AddColumnFor<TProperty>(Expression<Func<TModel, TProperty>> expression, int? width = null, string label = null)
        {
            AddColumn(MvcHtmlHelper.GetExpressionPropertyId(this.htmlHelper, expression),
                label != null ? label : MvcHtmlHelper.GetDisplayText(this.htmlHelper, expression),
                width);

            return this;
        }

        /// <summary>
        /// Adds a date column to the grid.  The column will be formatted using the provided format.
        /// </summary>
        /// <param name="name">Name of the property to bind from the model data.</param>
        /// <param name="label">Label to display on the column.</param>
        /// <param name="format">Format for the date.  The default format is "MM/dd/yyyy"</param>
        /// <param name="width">Optional fixed column width to apply to the column.</param>
        /// <returns></returns>
        public RazorGrid<TModel> AddDateColumn(string name, string label, string format = "MM/dd/yyyy", int? width = null)
        {
            BoundColumn dateColumn = new BoundColumn(label, "kendo.toString(kendo.parseDate(" + name + "), '" + format + "')");
            dateColumn.Width = width;
            columns.Add(dateColumn);

            return this;
        }

        /// <summary>
        /// Adds a date column to the grid.  The column will be formatted using the provided format.
        /// </summary>
        /// <param name="expression">The expression to bind.</param>
        /// <param name="format">Format for the date.  The default format is "MM/dd/yyyy"</param>
        /// <param name="width">Optional fixed column width to apply to the column.</param>
        /// <param name="label">Optonal label override for the expression.  If the label override is not provided, the label
        /// will be extracted from the bound model property.</param>
        /// <returns></returns>
        public RazorGrid<TModel> AddDateColumnFor(Expression<Func<TModel, DateTime>> expression, string format = "MM/dd/yyyy", int? width = null,
            string label = null)
        {
            AddDateColumn(MvcHtmlHelper.GetExpressionPropertyId(this.htmlHelper, expression),
                          label != null ? label : MvcHtmlHelper.GetDisplayText(this.htmlHelper, expression),
                          format, width);

            return this;
        }

        /// <summary>
        /// Adds a date column to the grid.  The column will be formatted using the provided format.
        /// </summary>
        /// <param name="expression">The expression to bind.</param>
        /// <param name="format">Format for the date.  The default format is "MM/dd/yyyy"</param>
        /// <param name="width">Optional fixed column width to apply to the column.</param>
        /// <param name="label">Optonal label override for the expression.  If the label override is not provided, the label
        /// will be extracted from the bound model property.</param>
        /// <returns></returns>
        public RazorGrid<TModel> AddDateColumnFor(Expression<Func<TModel, DateTime?>> expression, string format = "MM/dd/yyyy", int? width = null,
            string label = null)
        {
            AddDateColumn(MvcHtmlHelper.GetExpressionPropertyId(this.htmlHelper, expression),
                          label != null ? label : MvcHtmlHelper.GetDisplayText(this.htmlHelper, expression),
                          format, width);

            return this;
        }

        protected string GetEditWindowName()
        {
            return this.name + "_editWindow";
        }

        /// <summary>
        /// Adds an edit column that is used to show an edit form associated with the grid data.
        /// </summary>
        /// <param name="columnLabel"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public RazorGrid<TModel> AddWindowEditColumn(string columnLabel, string windowTitle, string formHeader,
            string actionName, string controllerName = null, string areaName = null)
        {
            string script = "bind" + this.name + @"Row('" + GetEditWindowName() + @"', '#: " + this.keyProperty + @" #')";
            ActionColumn editColumn = new ActionColumn(columnLabel, script, @"<span class=""k-icon k-i-pencil""></span>");
            editColumn.ColumnId = EDIT_COLUMN_ID;
            editColumn.Width = 15;
            editColumn.Tooltip = editTooltip;
            columns.Add(editColumn);

            //TODO:  Do something to handle multiple edit columns, or only allow one
            this.remoteEditUrl = MvcHtmlHelper.GetActionUrl(this.htmlHelper, actionName, controllerName, areaName);

            editWindow = new RazorGridWindow<TModel>(this.htmlHelper, this.ajaxHelper, GetEditWindowName(), this)
                    .SetModal(true)
                    .SetAjaxForm(new AjaxForm<TModel>(this.htmlHelper, this.ajaxHelper, this.name + "_editForm", actionName, controllerName)
                    .SetAjaxOptions(new AjaxOptions() { OnSuccess = this.name + "_saveSuccess", OnFailure = this.name + "_showError" })
                        .SetTitle(formHeader)
                        .SetIncludePanel(false)
                        .AddFooterActionButton(new Button<TModel>(this.htmlHelper, this.name + "_cancelButton", "Cancel").SetOnClick(this.name + "_cancel()"))
                        .AddFooterActionButton(new Button<TModel>(this.htmlHelper, this.name + "_saveButton", "Save").SetOnClick(this.name + "_save()"))
                    )
                    .SetTitle(windowTitle) as RazorGridWindow<TModel>;

            return this;
        }

        public RazorGrid<TModel> SetWindowAddButton(string actionName, string controllerName = null, string areaName = null)
        {
            this.remoteCreateUrl = MvcHtmlHelper.GetActionUrl(this.htmlHelper, actionName, controllerName, areaName);

            this.addWindow = new RazorGridWindow<TModel>(this.htmlHelper, this.ajaxHelper, this.name + "_addWindow", this);
            return this;
        }

        public RazorGrid<TModel> AddDeleteColumn(string columnLabel, string actionName, string controllerName = null, string areaName = null)
        {
            this.remoteDeleteUrl = MvcHtmlHelper.GetActionUrl(this.htmlHelper, actionName, controllerName, areaName);

            ActionColumn deleteColumn = new ActionColumn(columnLabel, GetPromptDeleteFunctionName() + "('#: " + this.keyProperty + @" #')", @"<span class=""k-icon k-i-cancel""></span>");
            deleteColumn.ColumnId = DELETE_COLUMN_ID;
            deleteColumn.Width = 15;
            deleteColumn.Tooltip = deleteTooltip;
            columns.Add(deleteColumn);

            return this;
        }

        protected string GetPromptDeleteFunctionName()
        {
            return this.name + "_deletePrompt";
        }

        protected string GetConfirmedDeleteFunctionName()
        {
            return this.name + "_delete";
        }

        /// <summary>
        /// Sets the location for the remote data source used to retrieve grid data.
        /// </summary>
        /// <param name="action">Action used to retrieve the data.</param>
        /// <param name="controller">Optional controller name.  If not specified the current controller name is used.</param>
        /// <param name="area">Optional area name.  If not specified the current area name is used.</param>
        /// <returns></returns>
        public RazorGrid<TModel> SetRemoteDataSource(string action, string controller = null, string area = null)
        {
            this.remoteDataSourceUrl = MvcHtmlHelper.GetActionUrl(this.htmlHelper, action, controller, area);

            return this;
        }

        /// <summary>
        /// Sets a flag indicating if data should be paged.  The default value is true.  If true, a pager control is shown at the bottom of the grid.
        /// </summary>
        /// <param name="pageData">Flag indicating if data should be paged.</param>
        /// <returns></returns>
        public RazorGrid<TModel> SetPageData(bool pageData)
        {
            this.pageData = pageData;

            return this;
        }

        /// <summary>
        /// Sets the number of records to display on a page.  The default page size is 10.
        /// </summary>
        /// <param name="pageSize">The number of records to display on a page.</param>
        /// <returns></returns>
        public RazorGrid<TModel> SetPageSize(int pageSize)
        {
            this.pageSize = pageSize;

            return this;
        }

        /// <summary>
        /// Sets the container column style that is used to determine the bootstrap layout for the grids container.  This can be used to effectively set the width of the grid.
        /// The default container column style is col-md-12.
        /// </summary>
        /// <param name="containerColumnStyle">The bootstrap column style to apply to the container.</param>
        /// <returns></returns>
        public RazorGrid<TModel> SetContainerColumnStyle(string containerColumnStyle)
        {
            this.containerColumnStyle = containerColumnStyle;

            return this;
        }

        /// <summary>
        /// Sets a flag indicating if server side paging occurs.  If server side paging is turned on, only a single page of data
        /// is expected to be returned from the server.  If server side paging is turned off, all data is expected to be included in the
        /// initial data load, and all paging will take place client side.  The default setting is to use server side paging.
        /// </summary>
        /// <param name="serverPaging">True if paging will occur server side.</param>
        /// <returns></returns>
        public RazorGrid<TModel> SetServerPaging(bool serverPaging)
        {
            this.serverPaging = serverPaging;

            return this;
        }

        /// <summary>
        /// Sets the title to display on a delete confirmation.
        /// </summary>
        /// <param name="deleteConfirmationTitle">Text to display on the delete confirmation title.</param>
        /// <returns></returns>
        public RazorGrid<TModel> SetDeleteConfirmationTitle(string deleteConfirmationTitle)
        {
            this.deleteConfirmationTitle = deleteConfirmationTitle;

            return this;
        }

        /// <summary>
        /// Sets the message to display on a delete confirmation.
        /// </summary>
        /// <param name="deleteConfirmationMessage">Message to display on the delete confirmation.</param>
        /// <returns></returns>
        public RazorGrid<TModel> SetDeleteConfirmationMessage(string deleteConfirmationMessage)
        {
            this.deleteConfirmationMessage = deleteConfirmationMessage;

            return this;
        }

        /// <summary>
        /// Sets the tooltip to display on the edit action column.  Setting this value to null will cause the tooltip to not show.  The default
        /// value for the tooltip is "edit".
        /// </summary>
        /// <param name="editTooltip">Text to display on the edit tooltip.  A value of null will cause the tooltip to not display.</param>
        /// <returns></returns>
        public RazorGrid<TModel> SetEditTooltip(string editTooltip)
        {
            this.editTooltip = editTooltip;

            // Check to determine if the delete column has already been added, if it has, update the tooltip.
            Column editColumn = columns.FirstOrDefault(c => c.GetId() == EDIT_COLUMN_ID);
            if (editColumn != null)
            {
                editColumn.Tooltip = editTooltip;
            }

            return this;
        }

        /// <summary>
        /// Sets the tooltip to display on the delete action column.  Setting this value to null will cause the tooltip to not show.  The default
        /// value for the tooltip is "delete".
        /// </summary>
        /// <param name="deleteTooltip">Text to display on the delete tooltip.  A value of null will cause the tooltip to not display.</param>
        /// <returns></returns>
        public RazorGrid<TModel> SetDeleteTooltip(string deleteTooltip)
        {
            this.deleteTooltip = deleteTooltip;

            // Check to determine if the delete column has already been added, if it has, update the tooltip.
            Column deleteColumn = columns.FirstOrDefault(c => c.GetId() == DELETE_COLUMN_ID);
            if (deleteColumn != null)
            {
                deleteColumn.Tooltip = deleteTooltip;
            }

            return this;
        }

        /// <summary>
        /// Appends the HTML needed for the notification popup that is used to display any errors back to the user.
        /// </summary>
        /// <param name="html">StringBuilder to use for the HTML.</param>
        /// <returns>The name of the notification control.</returns>
        protected string AppendNotification(StringBuilder html)
        {
            string notificationName = this.name + "_errorDisplay";
            Notification<TModel> notification = new Notification<TModel>(this.htmlHelper, notificationName)
                .SetNotificationType(KendoUIMvc.Controls.Notification.NotificationType.error)
                .SetAppendTo("#" + this.name + "Wrapper")
                .SetAutoHideAfter(0)
                .SetStacking(KendoUIMvc.Controls.Notification.StackingType.up);

            html.Append(notification.GetControlString());

            return notificationName;
        }

        public MvcHtmlString Render()
        {
            StringBuilder html = new StringBuilder();
            string dataTemplateName = name + "_layoutTemplate";
            string dataSourceName = name + "_dataSource";
            // Used to track the current action for the record.  Either add or edit.
            string actionMode = name + "_actionMode";

            string notificationName = AppendNotification(html);
            ConfirmationDialog<TModel> deleteConfirmation = null;

            if (this.remoteDeleteUrl != null)
            {
                // Add the delete confirmation dialog box if deleting is supported.
                deleteConfirmation = new ConfirmationDialog<TModel>(this.htmlHelper, this.name + "deleteConfirmation", deleteConfirmationTitle, deleteConfirmationMessage,
                    GetConfirmedDeleteFunctionName() + "()");
                html.Append(deleteConfirmation.GetControlString());
            }

            html.Append(@"
            <div id=""" + this.name + @"Container"" class=""container"">
                <div class=""row"">
                    <div class=""" + this.containerColumnStyle + @""" id=""" + this.name + @"Wrapper"" style=""position: relative;"">
                        <table id=""" + this.name + @""" class=""table table-striped table-bordered table-hover"">
                            <thead>
                                <tr>
                                    <th colspan=""" + this.columns.Count() + @""" class=""k-header"">
                                        <div class=""col-md-9"">The Title</div>");
            
            // Add the add record button.
            if (addWindow != null)
            {
                Button<TModel> addButton = new Button<TModel>(this.htmlHelper, this.name + "_addButton", "Add")
                        .SetIcon("plus")
                        .AddClass("pull-right")
                        .SetOnClick("bind" + this.name + @"Row('" + GetEditWindowName() + @"')");
                html.Append(@"
                                        <div class=""col-md-3"">" + addButton.GetControlString() + @"</div>");
            }

            html.Append(@"
                                    </th>
                                </tr>
                                <tr>");

            // Render column headers.
            foreach (Column nextColumn in columns)
            {
                html.Append(@"
                                    <th class=""k-header""");
                if (nextColumn.Width != null)
                {
                    html.Append(@" width=""" + nextColumn.Width.Value + @"px;""");
                }
                
                html.Append(@">" + nextColumn.Label + @"</th>");
            }

            html.Append(@"
                                </tr>
                            </thead>
                            <tbody data-bind=""source: " + dataSourceName + @""" data-template=""" + dataTemplateName + @""">
                            </tbody>
                        </table>");

            if (this.pageData)
            {
                html.Append(@"                        
                        <div id=""" + this.name + @"Pager"" class=""km-grid-pager""></div>
                ");
            }

            // Close out grid container divs
            html.Append(@"
                    </div>
                </div>
            </div>");

            html.Append(@"
            <script type=""text/x-kendo-template"" id=""" + dataTemplateName + @""">
                                <tr key-value=""#: " + this.keyProperty + @" #"">");

            // Render column data templates
            foreach (Column nextColumn in columns)
            {
                html.Append(@"
                                    <td data-property=""" + nextColumn.GetId() + @"""");

                if (nextColumn.Tooltip != null)
                {
                    html.Append(@" title=""" + nextColumn.Tooltip + @"""");
                }

                html.Append(@">" + nextColumn.RenderContent() + "</td>");
            }

            html.Append(@"
                                </tr>
            </script>
            <script type=""text/javascript"">
                var " + dataSourceName + @";
                var " + dataSourceName + @"Collection;
                var " + actionMode + @";

                $(document).ready(function() {
                    var wrapper = $('#" + this.name + @"Wrapper');
                    var gridContainer = $('#" + this.name + @"Container');
                    var pager = $('#" + this.name + @"Pager');                    

                    " + dataSourceName + @" = new kendo.data.DataSource({");

            if (this.pageData)
            {
                html.Append(@"
                        pageSize: " + this.pageSize + @",
                        serverPaging: " + MvcHtmlHelper.GetJavascriptBoolean(this.serverPaging) + @",");
            }

            html.Append(@"
                        type: 'json',
                        transport: {");

            // Add update option if an edit column was created.
            if (this.remoteEditUrl != null)
            {
                html.Append(@"
                            update: {
                                url: '" + this.remoteEditUrl + @"',
                                type: 'post',
                                complete: function(jqXHR, textStatus) { 
                                    if (textStatus == 'success') {
                                        " + this.name + @"_saveSuccess()
                                    }
                                }
                            },");
            }

            // Add create option if the add button is shown.
//            if (this.remoteCreateUrl != null)
//            {
//                html.Append(@"
//                            create: {
//                                url: '" + this.remoteCreateUrl + @"',
//                                type: 'post'
//                            },");
//            }

            if (this.remoteDeleteUrl != null)
            {
                html.Append(@"
                            delete: {
                                url: '" + this.remoteDeleteUrl + @"',
                                type: 'delete'
                            },");
            }

            html.Append(@"
                            read: {
                                url: '" + this.remoteDataSourceUrl + @"',
                                dataType: 'json'
                            }                            
                        },
                        schema: {
                            data: 'data',
                            total: 'total',
                            model: {
                                id: '" + this.keyProperty + @"'
                            }
                        },
                        requestStart: function (e) {
                            kendo.ui.progress(wrapper, true);
                        },
                        requestEnd: function (e) {
                            kendo.ui.progress(wrapper, false);
                        },
                        error: function (e) {
                            " + this.name + @"_showError(e.xhr);
                        }
                    });

                    " + dataSourceName + @"Collection = kendo.observable({
                        " + dataSourceName + @" : " + dataSourceName + @"
                    });

                    pager.kendoPager({
                        autoBind: false,
                        dataSource: " + dataSourceName + @",
                        refresh: true
                    });

                    kendo.bind(gridContainer, " + dataSourceName + @"Collection);
                });              

                function " + this.name + @"_saveSuccess() {
                    $('#" + GetEditWindowName() + @"').data('kendoWindow').close();
                }

                function " + this.name + @"_showError(response) {
                    var message = null;

                    if (response.responseText != undefined) {
                        // Get a detailed error if available.
                        try {
                            message = JSON.parse(response.responseText).errors;
                        } catch (ex) { }
                    }

                    if (message == null) {
                        message = ""Error Completing Request."";
                    }");

                if (this.editWindow != null && this.editWindow.GetAjaxForm() != null &&
                    this.editWindow.GetAjaxForm().GetNotification() != null)
                {
                    html.Append(@"
                    " + this.editWindow.GetAjaxForm().GetNotification().GetCallShowScript("message"));
                }
                else
                {
                    // Default to show errors on the grid notification, if an edit window does not exist.
                    html.Append(@"
                    show" + notificationName + @"(message);");
                }

                html.Append(@"
                }

                function " + this.name + @"_save() {
                    if (" + actionMode + @" == 'add') {
                        $('#" + GetEditWindowName() + @" form').submit();
                        " + dataSourceName + @".read();
                    } else {
                        " + dataSourceName + @".sync();
                    }                                       
                }

                function " + this.name + @"_cancel() {
                    " + dataSourceName + @".cancelChanges();
                    $('#" + GetEditWindowName() + @"').data('kendoWindow').close();
                }

                function bind" + this.name + @"Row(windowName, key) {
                    var model = " + dataSourceName + @".get(key);
                    var window = $('#' + windowName);

                    // Bind the model to the window.
                    kendo.bind(window, model);
                    // Show the window after binding.
                    window.data('kendoWindow').open();

                    var form = $('#" + GetEditWindowName() + @" form');

                    // Set the action mode for the current window.
                    if (model != undefined) {
                        form.attr('action', '" + this.remoteEditUrl + @"');
                        " + actionMode + @" = 'edit';
                    } else {
                        form.attr('action', '" + this.remoteCreateUrl + @"');
                        " + actionMode + @" = 'add';
                    }

                    return false;
                }");

            // Generate delete function if supported.
            if (this.remoteDeleteUrl != null)
            {
                string currentDeleteKeyVariableName = this.name + "_currentDeleteKey";

                html.Append(@"
                var " + currentDeleteKeyVariableName + @";
                function " + GetPromptDeleteFunctionName() + @"(key) {
                    " + currentDeleteKeyVariableName + @" = key;
                    " + deleteConfirmation.GetCallShowScript() + @"
                }

                function " + GetConfirmedDeleteFunctionName() + @"() {
                    var succeeded;
                    $.ajax({
                        type: 'post',
                        dataType: 'json',
                        url: '" + this.remoteDeleteUrl + @"',
                        data: { """ + this.keyProperty + @""" : " + currentDeleteKeyVariableName + @"},
                        async: false,
                        success: function (data) {
                            succeeded = true;
                            " + dataSourceName + @".read();
                        },
                        error: function (data) {
                            succeeded = false;
                        }
                    })

                    return succeeded;
                }");
            }

            html.Append(@"
            </script>");

            return new MvcHtmlString(html.ToString());
        }

        public override string ToString()
        {
            MvcHtmlHelper.WriteUnencodedContent(this.htmlHelper, Render().ToString());

            return "";
        }

        /// <summary>
        /// Represents a column in the grid.
        /// </summary>
        public abstract class Column
        {
            /// <summary>
            /// Renders the content for one cell in the grid for the column.
            /// </summary>
            /// <returns></returns>
            public abstract string RenderContent();
            public abstract string GetId();

            public Column()
            {
            }

            public Column(string label)
            {
                this.Label = label;
            }
            
            public string Label { get; set; }
            public int? Width { get; set; }
            public string Tooltip;
        }

        /// <summary>
        /// Bound columns are used to bind columns to specific data.
        /// </summary>
        public class BoundColumn : Column
        {
            public BoundColumn(string label, string property) :
                base(label)
            {
                this.Property = property;
            }

            /// <summary>
            /// Property that is bound in the data model.
            /// </summary>
            public string Property { get; set; }

            public override string RenderContent()
            {
                return "#: " + this.Property + @" #";
            }

            public override string GetId()
            {
                return this.Property;
            }
        }       

        /// <summary>
        /// Action columns invoke a javascript function when the cell content is clicked.
        /// </summary>
        public class ActionColumn : Column
        {
            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="label">Label for the column header.</param>
            /// <param name="script">Script to invoke when the action column is clicked.</param>
            /// <param name="actionLabel">Action label to display in the column.  This could be either text or html to emit an icon, etc.</param>
            public ActionColumn(string label, string script, string actionLabel)
                : base(label)
            {
                this.Script = script;
                this.ActionLabel = actionLabel;
            }

            /// <summary>
            /// Script to invoke when the action column is clicked.
            /// </summary>
            public string Script { get; set; }

            /// <summary>
            /// Action label to display in the column.  This could be either text or html to emit an icon, etc.
            /// </summary>
            public string ActionLabel { get; set; }

            /// <summary>
            /// Used as a tag to identify the column uniquely when rendered.
            /// </summary>
            public string ColumnId { get; set; }

            public override string RenderContent()
            {                
                return @"<a href=""\\#"" onclick=""return " + this.Script + @";"">" + this.ActionLabel + @"</a>";
                //return @"<a href=""javascript: " + this.Script + @"();"">" + this.ActionLabel + @"</a>";
                //return @"<button onclick=""return " + this.Script + @"();"">" + this.ActionLabel + @"</button>";
            }

            public override string GetId()
            {
                return this.ColumnId;
            }
        }

        /// <summary>
        /// Gets the edit window control for the grid.
        /// </summary>
        /// <returns></returns>
        public Window<TModel> GetEditWindow()
        {
            return this.editWindow;
        }
    }
}
