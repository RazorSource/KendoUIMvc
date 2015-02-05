using KendoUIMvc.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Linq.Expressions;
using CommonMvc.Razor.Controls;
using CommonMvc.Util;
using Newtonsoft.Json;
using System.Web;

namespace KendoUIMvc.Controls
{
    public class RazorGrid<TModel, TKeyProperty> : RazorGrid<TModel>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="htmlHelper">MVC HTML Helper used to generate controls.</param>
        /// <param name="name">Name of the grid.</param>
        /// <param name="keyProperty">The key property on the model.</param>
        public RazorGrid(HtmlHelper<TModel> htmlHelper, AjaxHelper<TModel> ajaxHelper, string name, Expression<Func<TModel, TKeyProperty>> keyExpression)
            : base(htmlHelper, ajaxHelper, name, MvcHtmlHelper.GetExpressionPropertyId(htmlHelper, keyExpression))
        {
        }
    }

    public class RazorGrid<TModel> : IGrid<TModel>
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
        protected string containerCssClass;
        protected string keyProperty;
        protected RazorGridWindow<TModel> editWindow;
        protected RazorGridWindow<TModel> addWindow;
        protected string deleteConfirmationTitle = "Confirm Delete";
        protected string deleteConfirmationMessage = "Are you sure you want to delete this item?";
        protected string editTooltip = "edit";
        protected string deleteTooltip = "delete";
        protected string title;

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
            this.containerCssClass = "col-md-12";
            this.keyProperty = keyProperty;
        }

        /// <summary>
        /// Adds a new column to the grid.
        /// </summary>
        /// <param name="name">Name of the property to bind from the model data.</param>
        /// <param name="label">Label to display on the column.</param>
        /// <param name="width">Optional fixed column width to apply to the column.</param>
        /// <returns></returns>
        public IGrid<TModel> AddColumn(string name, string label, int? width = null)
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
        public IGrid<TModel> AddColumnFor<TProperty>(Expression<Func<TModel, TProperty>> expression, int? width = null, string label = null)
        {
            AddColumn(MvcHtmlHelper.GetExpressionPropertyId(this.htmlHelper, expression),
                label != null ? label : MvcHtmlHelper.GetDisplayText(this.htmlHelper, expression),
                width);

            return this;
        }

        /// <summary>
        /// Adds a lookup column to the grid.  Lookup columns can be used to allow the grid to replace key lookup values with their
        /// corresponding lookup values in the UI layer.
        /// </summary>
        /// <param name="name">Name of the property to bind from the model data.</param>
        /// <param name="label">Label to display on the column.</param>
        /// <param name="lookupOptions">List of SelectListItems that are used to find the corresponding value to display.</param>
        /// <param name="width">Optional fixed column width to apply to the column.</param>
        /// <returns></returns>
        public IGrid<TModel> AddLookupColumn(string name, string label, IEnumerable<SelectListItem> lookupOptions, int? width = null)
        {
            BoundLookupColumn boundLookupColumn = new BoundLookupColumn(label, name, lookupOptions);

            boundLookupColumn.Width = width;
            columns.Add(boundLookupColumn);

            return this;
        }

        /// <summary>
        /// Adds a lookup column to the grid.  Lookup columns can be used to allow the grid to replace key lookup values with their
        /// corresponding lookup values in the UI layer.
        /// </summary>
        /// <typeparam name="TProperty">Type of the property being bound.  This should correspond to the key value.</typeparam>
        /// <param name="expression">The expression to bind.</param>
        /// <param name="lookupOptions">List of SelectListItems that are used to find the corresponding value to display.</param>
        /// <param name="width">Optional fixed column width to apply to the column.</param>
        /// <param name="label">Optonal label override for the expression.  If the label override is not provided, the label
        /// will be extracted from the bound model property.</param>
        /// <returns></returns>
        public IGrid<TModel> AddLookupColumnFor<TProperty>(Expression<Func<TModel, TProperty>> expression,
            IEnumerable<SelectListItem> lookupOptions, int? width = null, string label = null)
        {
            BoundLookupColumn boundLookupColumn = new BoundLookupColumn(
                label != null ? label : MvcHtmlHelper.GetDisplayText(this.htmlHelper, expression),
                MvcHtmlHelper.GetExpressionPropertyId(this.htmlHelper, expression),
                lookupOptions);

            boundLookupColumn.Width = width;
            columns.Add(boundLookupColumn);

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
        public IGrid<TModel> AddDateColumn(string name, string label, string format = "MM/dd/yyyy", int? width = null)
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
        public IGrid<TModel> AddDateColumnFor(Expression<Func<TModel, DateTime>> expression, string format = "MM/dd/yyyy", int? width = null,
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
        public IGrid<TModel> AddDateColumnFor(Expression<Func<TModel, DateTime?>> expression, string format = "MM/dd/yyyy", int? width = null,
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
        /// <param name="columnLabel">Label for the column header.</param>
        /// <param name="windowTitle">Title to display on the edit window.</param>
        /// <param name="formHeader">Header caption to display on the form for the edit window.</param>
        /// <param name="actionName">Name of the action to invoke to save an edit.</param>
        /// <param name="controllerName">Name of the controller to invoke to save an edit.</param>
        /// <param name="areaName">Name of the area for the controller to invoke to save an edit.</param>
        /// <returns></returns>
        public IGrid<TModel> AddWindowEditColumn(string columnLabel, string windowTitle, string formHeader,
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

            // TODO:  Chain calls again when AjaxForm interface is refactored.
            editWindow = new RazorGridWindow<TModel>(this.htmlHelper, this.ajaxHelper, GetEditWindowName(), this);
            editWindow.SetModal(true);


            editWindow.SetAjaxForm(new AjaxForm<TModel>(this.htmlHelper, this.ajaxHelper, this.name + "_editForm", actionName, controllerName)
                    .SetAjaxOptions(new AjaxOptions() { OnSuccess = this.name + "_saveSuccess", OnFailure = this.name + "_showError" })
                        .SetTitle(formHeader)
                        .SetIncludePanel(false)
                        .AddFooterActionButton(new Button<TModel>(this.htmlHelper, this.name + "_cancelButton", "Cancel").SetOnClick(this.name + "_cancel()"))
                        .AddFooterActionButton(new Button<TModel>(this.htmlHelper, this.name + "_saveButton", "Save").SetOnClick(this.name + "_save()"))
                    );
            
            editWindow.SetTitle(windowTitle);

            return this;
        }

        /// <summary>
        /// Adds an add button to the grid to add new records.
        /// </summary>
        // <param name="actionName">Name of the action to invoke to save a new record.</param>
        /// <param name="controllerName">Name of the controller to invoke to save a new record.</param>
        /// <param name="areaName">Name of the area for the controller to invoke to save a new record.</param>
        /// <returns></returns>
        public IGrid<TModel> SetWindowAddButton(string actionName, string controllerName = null, string areaName = null)
        {
            this.remoteCreateUrl = MvcHtmlHelper.GetActionUrl(this.htmlHelper, actionName, controllerName, areaName);

            this.addWindow = new RazorGridWindow<TModel>(this.htmlHelper, this.ajaxHelper, this.name + "_addWindow", this);
            return this;
        }

        /// <summary>
        /// Adds a delete column that is used to delete a record.
        /// </summary>
        /// <param name="columnLabel">Label for the column header.</param>
        /// <param name="actionName">Name of the action to invoke the delete.</param>
        /// <param name="controllerName">Name of the controller to invoke the delete.</param>
        /// <param name="areaName">Name of the area for the controller to invoke the delete.</param>
        /// <returns></returns>
        public IGrid<TModel> AddDeleteColumn(string columnLabel, string actionName, string controllerName = null, string areaName = null)
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
        public IGrid<TModel> SetRemoteDataSource(string action, string controller = null, string area = null)
        {
            this.remoteDataSourceUrl = MvcHtmlHelper.GetActionUrl(this.htmlHelper, action, controller, area);

            return this;
        }

        /// <summary>
        /// Sets a flag indicating if data should be paged.  The default value is true.  If true, a pager control is shown at the bottom of the grid.
        /// </summary>
        /// <param name="pageData">Flag indicating if data should be paged.</param>
        /// <returns></returns>
        public IGrid<TModel> SetPageData(bool pageData)
        {
            this.pageData = pageData;

            return this;
        }

        /// <summary>
        /// Sets the number of records to display on a page.  The default page size is 10.
        /// </summary>
        /// <param name="pageSize">The number of records to display on a page.</param>
        /// <returns></returns>
        public IGrid<TModel> SetPageSize(int pageSize)
        {
            this.pageSize = pageSize;

            return this;
        }

        /// <summary>
        /// Sets the container column style that is used to determine the bootstrap layout for the grids container.  This can be used to effectively set the width of the grid.
        /// The default container column style is col-md-12.
        /// </summary>
        /// <param name="containerCssClass">The bootstrap column style to apply to the container.</param>
        /// <returns></returns>
        public IGrid<TModel> SetGridContainerClass(string containerCssClass)
        {
            this.containerCssClass = containerCssClass;

            return this;
        }

        /// <summary>
        /// Sets a flag indicating if server side paging occurs.  If server side paging is turned on, only a single page of data
        /// is expected to be returned from the server.  If server side paging is turned off, all data is expected to be included in the
        /// initial data load, and all paging will take place client side.  The default setting is to use server side paging.
        /// </summary>
        /// <param name="serverPaging">True if paging will occur server side.</param>
        /// <returns></returns>
        public IGrid<TModel> SetServerPaging(bool serverPaging)
        {
            this.serverPaging = serverPaging;

            return this;
        }

        /// <summary>
        /// Sets the title to display on a delete confirmation.
        /// </summary>
        /// <param name="deleteConfirmationTitle">Text to display on the delete confirmation title.</param>
        /// <returns></returns>
        public IGrid<TModel> SetDeleteConfirmationTitle(string deleteConfirmationTitle)
        {
            this.deleteConfirmationTitle = deleteConfirmationTitle;

            return this;
        }

        /// <summary>
        /// Sets the message to display on a delete confirmation.
        /// </summary>
        /// <param name="deleteConfirmationMessage">Message to display on the delete confirmation.</param>
        /// <returns></returns>
        public IGrid<TModel> SetDeleteConfirmationMessage(string deleteConfirmationMessage)
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
        public IGrid<TModel> SetEditTooltip(string editTooltip)
        {
            this.editTooltip = editTooltip;

            // Check to determine if the delete column has already been added, if it has, update the tooltip.
            Column editColumn = columns.FirstOrDefault(c => c.Id == EDIT_COLUMN_ID);
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
        public IGrid<TModel> SetDeleteTooltip(string deleteTooltip)
        {
            this.deleteTooltip = deleteTooltip;

            // Check to determine if the delete column has already been added, if it has, update the tooltip.
            Column deleteColumn = columns.FirstOrDefault(c => c.Id == DELETE_COLUMN_ID);
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
            IMessageDisplay<TModel> notification = new Notification<TModel>(this.htmlHelper, notificationName)
                .SetMessageType(MessageType.error)
                .SetAppendTo("#" + this.name + "Wrapper")
                .SetAutoHideAfter(0)
                .SetStacking(StackingType.up);

            html.Append(notification.GetControlString());

            return notificationName;
        }

        /// <summary>
        /// Sets the title to display on the grid.
        /// </summary>
        /// <param name="title">Text for the title.</param>
        /// <returns></returns>
        public IGrid<TModel> SetTitle(string title)
        {
            this.title = title;

            return this;
        }

        /// <summary>
        /// Gets the edit window control for the grid.
        /// </summary>
        /// <returns></returns>
        public IWindow<TModel> GetEditWindow()
        {
            return this.editWindow;
        }

        public IGrid<TModel> SetHtmlHelper(HtmlHelper<TModel> htmlHelper)
        {
            this.htmlHelper = htmlHelper;
            return this;
        }

        public IGrid<TModel> SetAjaxHelper(AjaxHelper<TModel> ajaxHelper)
        {
            this.ajaxHelper = ajaxHelper;
            return this;
        }

        public IGrid<TModel> SetName(string name)
        {
            this.name = name;
            return this;
        }

        public IGrid<TModel> SetKeyProperty(string keyProperty)
        {
            this.keyProperty = keyProperty;
            return this;
        }

        /// <summary>
        /// Builds the HTML necessary to render the control.
        /// </summary>
        /// <returns>An MvcHtmlString that contains all HTML and script necessary to render the grid control.</returns>
        public MvcHtmlString Render()
        {
            StringBuilder html = new StringBuilder();
            string dataTemplateName = name + "_layoutTemplate";
            string dataSourceName = name + "_dataSource";
            // Used to track the current action for the record.  Either add or edit.
            string actionMode = name + "_actionMode";
            string lastDataSourceActionName = "last" + this.name + "_dataSourceAction";

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
                <div class=""row " + this.containerCssClass + @""">
                    <div class=""km-razor-grid-wrapper"" id=""" + this.name + @"Wrapper"">
                        <table id=""" + this.name + @""" class=""table table-striped table-bordered table-hover"">
                            <thead>
                                <tr>
                                    <th colspan=""" + this.columns.Count() + @""" class=""k-header"">
                                        <div class=""col-md-9"">" + this.title + @"</div>");
            
            // Add the add record button.
            if (addWindow != null)
            {
                IButton<TModel> addButton = new Button<TModel>(this.htmlHelper, this.name + "_addButton", "Add")
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
                                    <td data-property=""" + nextColumn.Id + @"""");

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
                var " + lastDataSourceActionName + @";

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
                                cache: false,
                                dataType: 'json',
                                complete: function(jqXHR, textStatus) { 
                                    if (jqXHR.responseJSON != null &&
                                        jqXHR.responseJSON.data != null &&
                                        jqXHR.responseJSON.data.length == 0) {
                                        $('#" + this.name + @" tbody').html('<td class=""km-razor-grid-no-data"" colspan=""" + columns.Count + @""">No Data Found</td>');
                                    }
                                }
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
                            lastDataSourceActionName = e.type;
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

                    // If in add mode, refresh the grid after the confirmed save.
                    if (" + actionMode + @" == 'add') {
                        " + dataSourceName + @".read();
                    }
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
                    this.editWindow.GetAjaxForm().GetMessageDisplay() != null)
                {
                    html.Append(@"
                    if (lastDataSourceActionName == 'read') {
                        show" + notificationName + @"(message);
                    } else {
                        " + this.editWindow.GetAjaxForm().GetMessageDisplay().GetCallShowScript("message") + @"
                    }");
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

            // Generate any script helper functions for lookup columns.
            foreach (Column nextColumn in columns)
            {
                if (nextColumn is ILookupColumn)
                {
                    html.Append( ((ILookupColumn)nextColumn).GetLookupScript());
                }
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

        protected interface ILookupColumn : IColumn
        {
            string GetLookupScript();
        }

        protected interface IColumn
        {
            string Id { get; }
            string Label { get; set; }
            int? Width { get; set; }
            string Tooltip { get; set; }
        }

        /// <summary>
        /// Represents a column in the grid.
        /// </summary>
        public abstract class Column : IColumn
        {
            /// <summary>
            /// Renders the content for one cell in the grid for the column.
            /// </summary>
            /// <returns></returns>
            public abstract string RenderContent();
            public abstract string Id { get; }

            public Column()
            {
            }

            public Column(string label)
            {
                this.Label = label;
            }
            
            public string Label { get; set; }
            public int? Width { get; set; }
            public string Tooltip { get; set; }
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
                return "#: " + this.Property + @" != null ? " + this.Property + @" : '' #";
            }

            public override string Id
            {
                get
                {
                    return this.Property;
                }
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
            }

            public override string Id
            {
                get
                {
                    return this.ColumnId;
                }
            }
        }

        public class BoundLookupColumn : BoundColumn, ILookupColumn
        {
            protected IEnumerable<SelectListItem> lookupOptions;

            public BoundLookupColumn(string label, string property, IEnumerable<SelectListItem> lookupOptions)
                : base(label, property)
            {
                this.lookupOptions = lookupOptions;
            }

            /// <summary>
            /// Generates script necessary to resolve lookup values client side when data binding.
            /// </summary>
            /// <returns></returns>
            public string GetLookupScript()
            {
                StringBuilder script = new StringBuilder();

                // Define a global variable for the lookup values.
                script.Append(@"
                var "  + this.Id + @"LookupValues = {};");

                foreach (SelectListItem nextSelectListItem in this.lookupOptions)
                {
                    script.Append(@"
                " + this.Id + @"LookupValues['" + HttpUtility.JavaScriptStringEncode(nextSelectListItem.Value) + "'] = '" + 
                        HttpUtility.JavaScriptStringEncode(nextSelectListItem.Text) + @"';");
                }
                
                script.Append(@"
                function " + GetLookupFunctionName() + @"(value) {
                    var lookupValue = " + this.Id + @"LookupValues[value];
                    if (lookupValue != undefined) {
                        return lookupValue;
                    } else {
                        return '';
                    }
                }");

                return script.ToString();
            }

            /// <summary>
            /// Gets the javascript necessary to invoke the lookup function.
            /// </summary>
            /// <param name="parameter">Parameter variable to pass into the function.</param>
            /// <returns>Javascript to call as a string.</returns>
            public string GetCallLookupScript(string parameter)
            {
                return GetLookupFunctionName() + "(" + parameter + ")";
            }

            protected string GetLookupFunctionName()
            {
                return this.Id + "_LookupValue";
            }

            /// <summary>
            /// Renders the content necessary to display the corresponding lookup value for a column.
            /// </summary>
            /// <returns></returns>
            public override string RenderContent()
            {
                return "#: " + GetCallLookupScript(this.Property) + " #";
            }
        }
    }
}
