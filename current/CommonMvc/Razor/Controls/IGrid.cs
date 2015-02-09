using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CommonMvc.Razor.Controls
{
    /// <summary>
    /// Interface to support common grid functionality.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface IGrid<TModel>
    {
        /// <summary>
        /// Sets the HtmlHelper instance for the current view.
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper that can be used to render content.</param>
        /// <returns></returns>
        IGrid<TModel> SetHtmlHelper(HtmlHelper<TModel> htmlHelper);

        /// <summary>
        /// Sets the AjaxHelper instance for the current view.
        /// </summary>
        /// <param name="ajaxHelper">AjaxHelper that can be used to render content.</param>
        /// <returns></returns>
        IGrid<TModel> SetAjaxHelper(AjaxHelper<TModel> ajaxHelper);

        /// <summary>
        /// Sets the name used to identify the grid.
        /// </summary>
        /// <param name="name">Name of the grid.</param>
        /// <returns></returns>
        IGrid<TModel> SetName(string name);

        /// <summary>
        /// The key property on the model.
        /// </summary>
        /// <param name="keyProperty">Name of the key property.</param>
        /// <returns></returns>
        IGrid<TModel> SetKeyProperty(string keyProperty);

        /// <summary>
        /// Adds a new column to the grid.
        /// </summary>
        /// <param name="name">Name of the property to bind from the model data.</param>
        /// <param name="label">Label to display on the column.</param>
        /// <param name="width">Optional fixed column width to apply to the column.</param>
        /// <returns></returns>
        IGrid<TModel> AddColumn(string name, string label, int? width = null);

        /// <summary>
        /// Adds a new column to the grid.
        /// </summary>
        /// <typeparam name="TProperty">Type of the property being bound.</typeparam>
        /// <param name="expression">The expression to bind.</param>
        /// <param name="width">Optional fixed column width to apply to the column.</param>
        /// <param name="label">Optonal label override for the expression.  If the label override is not provided, the label
        /// will be extracted from the bound model property.</param>
        /// <returns></returns>
        IGrid<TModel> AddColumnFor<TProperty>(Expression<Func<TModel, TProperty>> expression, int? width = null, string label = null);

        /// <summary>
        /// Adds a date column to the grid.  The column will be formatted using the provided format.
        /// </summary>
        /// <param name="name">Name of the property to bind from the model data.</param>
        /// <param name="label">Label to display on the column.</param>
        /// <param name="format">Format for the date.  The default format is "MM/dd/yyyy"</param>
        /// <param name="width">Optional fixed column width to apply to the column.</param>
        /// <returns></returns>
        IGrid<TModel> AddDateColumn(string name, string label, string format = "MM/dd/yyyy", int? width = null);

        /// <summary>
        /// Adds a date column to the grid.  The column will be formatted using the provided format.
        /// </summary>
        /// <param name="expression">The expression to bind.</param>
        /// <param name="format">Format for the date.  The default format is "MM/dd/yyyy"</param>
        /// <param name="width">Optional fixed column width to apply to the column.</param>
        /// <param name="label">Optonal label override for the expression.  If the label override is not provided, the label
        /// will be extracted from the bound model property.</param>
        /// <returns></returns>
        IGrid<TModel> AddDateColumnFor(Expression<Func<TModel, DateTime>> expression, string format = "MM/dd/yyyy", int? width = null,
            string label = null);

        /// <summary>
        /// Adds a date column to the grid.  The column will be formatted using the provided format.
        /// </summary>
        /// <param name="expression">The expression to bind.</param>
        /// <param name="format">Format for the date.  The default format is "MM/dd/yyyy"</param>
        /// <param name="width">Optional fixed column width to apply to the column.</param>
        /// <param name="label">Optonal label override for the expression.  If the label override is not provided, the label
        /// will be extracted from the bound model property.</param>
        /// <returns></returns>
        IGrid<TModel> AddDateColumnFor(Expression<Func<TModel, DateTime?>> expression, string format = "MM/dd/yyyy", int? width = null,
            string label = null);

        /// <summary>
        /// Adds a lookup column to the grid.  Lookup columns can be used to allow the grid to replace key lookup values with their
        /// corresponding lookup values in the UI layer.
        /// </summary>
        /// <param name="name">Name of the property to bind from the model data.</param>
        /// <param name="label">Label to display on the column.</param>
        /// <param name="lookupOptions">List of SelectListItems that are used to find the corresponding value to display.</param>
        /// <param name="width">Optional fixed column width to apply to the column.</param>
        /// <returns></returns>
        IGrid<TModel> AddLookupColumn(string name, string label, IEnumerable<SelectListItem> lookupOptions, int? width = null);

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
        IGrid<TModel> AddLookupColumnFor<TProperty>(Expression<Func<TModel, TProperty>> expression,
            IEnumerable<SelectListItem> lookupOptions, int? width = null, string label = null);

        /// <summary>
        /// Adds an edit column that is used to show an edit form associated with the grid data.
        /// </summary>
        /// <param name="windowTitle">Title to display on the edit window.</param>
        /// <param name="formHeader">Header caption to display on the form for the edit window.</param>
        /// <param name="actionName">Name of the action to invoke to save an edit.</param>
        /// <param name="controllerName">Name of the controller to invoke to save an edit.</param>
        /// <param name="areaName">Name of the area for the controller to invoke to save an edit.</param>
        /// <returns></returns>
        IGrid<TModel> AddEditColumnModal(string windowTitle, string formHeader,
            string actionName, string controllerName = null, string areaName = null);

        /// <summary>
        /// Adds an edit column that links to a new page.  A query string parameter is appeneded to the URL, with the keyProperty defined as the name and the associated row
        /// key as the value.
        /// </summary>
        /// <param name="baseUrl">Base URL to use for editing. A query string parameter is appeneded to the URL, with the keyProperty defined as the name and the associated row
        /// key as the value.</param>
        /// <returns></returns>
        IGrid<TModel> AddEditColumnNewPage(string baseUrl);

        /// <summary>
        /// Adds an edit column that links to a new page.  A query string parameter is appeneded to the URL, with the keyProperty defined as the name and the associated row
        /// key as the value.
        /// </summary>
        /// <param name="actionName">Name of the action to invoke to display the edit page.</param>
        /// <param name="controllerName">Name of the controller containing the action.</param>
        /// <param name="areaName">Name of hte area containing the controller.</param>
        /// <returns></returns>
        IGrid<TModel> AddEditColumnNewPage(string actionName, string controllerName = null, string areaName = null);

        /// <summary>
        /// Adds an add button to the grid to add new records.
        /// </summary>
        // <param name="actionName">Name of the action to invoke to save a new record.</param>
        /// <param name="controllerName">Name of the controller to invoke to save a new record.</param>
        /// <param name="areaName">Name of the area for the controller to invoke to save a new record.</param>
        /// <returns></returns>
        IGrid<TModel> SetWindowAddButton(string actionName, string controllerName = null, string areaName = null);

        /// <summary>
        /// Adds a delete column that is used to delete a record.
        /// </summary>
        /// <param name="columnLabel">Label for the column header.</param>
        /// <param name="actionName">Name of the action to invoke the delete.</param>
        /// <param name="controllerName">Name of the controller to invoke the delete.</param>
        /// <param name="areaName">Name of the area for the controller to invoke the delete.</param>
        /// <returns></returns>
        IGrid<TModel> AddDeleteColumn(string columnLabel, string actionName, string controllerName = null, string areaName = null);

        /// <summary>
        /// Sets the location for the remote data source used to retrieve grid data.
        /// </summary>
        /// <param name="action">Action used to retrieve the data.</param>
        /// <param name="controller">Controller used to retrieve the data.</param>
        /// <param name="area">Area for the controller used to retrieve the data.</param>
        /// <returns></returns>
        IGrid<TModel> SetRemoteDataSource(string action, string controller = null, string area = null);

        /// <summary>
        /// Sets a flag indicating if data should be paged.  The default value is true.  If true, a pager control is shown at the bottom of the grid.
        /// </summary>
        /// <param name="pageData">Flag indicating if data should be paged.</param>
        /// <returns></returns>
        IGrid<TModel> SetPageData(bool pageData);

        /// <summary>
        /// Sets the number of records to display on a page.  The default page size is 10.
        /// </summary>
        /// <param name="pageSize">The number of records to display on a page.</param>
        /// <returns></returns>
        IGrid<TModel> SetPageSize(int pageSize);

        /// <summary>
        /// Sets the container CSS class that is used to determine the layout for the grid's container.  This can be used to effectively set the width of the grid, or apply
        /// another other styling.
        /// </summary>
        /// <param name="containerCssClass">The CSS class to apply to the container.</param>
        /// <returns></returns>
        IGrid<TModel> SetGridContainerClass(string containerCssClass);

        /// <summary>
        /// Sets a flag indicating if server side paging occurs.  If server side paging is turned on, only a single page of data
        /// is expected to be returned from the server.  If server side paging is turned off, all data is expected to be included in the
        /// initial data load, and all paging will take place client side.  The default setting is to use server side paging.
        /// </summary>
        /// <param name="serverPaging">True if paging will occur server side.</param>
        /// <returns></returns>
        IGrid<TModel> SetServerPaging(bool serverPaging);

        /// <summary>
        /// Sets the title to display on a delete confirmation.
        /// </summary>
        /// <param name="deleteConfirmationTitle">Text to display on the delete confirmation title.</param>
        /// <returns></returns>
        IGrid<TModel> SetDeleteConfirmationTitle(string deleteConfirmationTitle);

        /// <summary>
        /// Sets the message to display on a delete confirmation.
        /// </summary>
        /// <param name="deleteConfirmationMessage">Message to display on the delete confirmation.</param>
        /// <returns></returns>
        IGrid<TModel> SetDeleteConfirmationMessage(string deleteConfirmationMessage);

        /// <summary>
        /// Sets the tooltip to display on the edit action column.  Setting this value to null will cause the tooltip to not show.  The default
        /// value for the tooltip is "edit".
        /// </summary>
        /// <param name="editTooltip">Text to display on the edit tooltip.  A value of null will cause the tooltip to not display.</param>
        /// <returns></returns>
        IGrid<TModel> SetEditTooltip(string editTooltip);

        /// <summary>
        /// Sets the tooltip to display on the delete action column.  Setting this value to null will cause the tooltip to not show.  The default
        /// value for the tooltip is "delete".
        /// </summary>
        /// <param name="deleteTooltip">Text to display on the delete tooltip.  A value of null will cause the tooltip to not display.</param>
        /// <returns></returns>
        IGrid<TModel> SetDeleteTooltip(string deleteTooltip);

        /// <summary>
        /// Sets the title to display on the grid.
        /// </summary>
        /// <param name="title">Text for the title.</param>
        /// <returns></returns>
        IGrid<TModel> SetTitle(string title);

        /// <summary>
        /// Gets the edit window control for the grid.
        /// </summary>
        /// <returns>The edit window instance.</returns>
        IWindow<TModel> GetEditWindow();

        /// <summary>
        /// Builds the HTML necessary to render the control.
        /// </summary>
        /// <returns>An MvcHtmlString that contains all HTML and script necessary to render the grid control.</returns>
        MvcHtmlString Render();
    }
}
