using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CommonMvc.Razor.Controls;

namespace CommonMvc.Razor
{
    /// <summary>
    /// Base common interface an HTML Helper library should implement to support Razor web controls.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface IRazorHtmlHelper<TModel>
    {
        /// <summary>
        /// Renders a date picker control.
        /// </summary>
        /// <param name="expression">Expression used to bind a control to a property.</param>
        /// <returns></returns>
        IDatePicker<TModel, DateTime?> DatePickerFor(Expression<Func<TModel, DateTime?>> expression);

        /// <summary>
        /// Renders a date picker control.
        /// </summary>
        /// <param name="expression">Expression used to bind a control to a property.</param>
        /// <returns></returns>
        IDatePicker<TModel, DateTime> DatePickerFor(Expression<Func<TModel, DateTime>> expression);

        /// <summary>
        /// Renders a date picker control that is not bound.
        /// </summary>
        /// <param name="name">Name of the control.</param>
        /// <param name="value">Initial value of the control.</param>
        /// <returns></returns>
        IDatePicker<TModel, DateTime> DatePicker(string name, DateTime? value = null);

        /// <summary>
        /// Renders a text box control.
        /// </summary>
        /// <typeparam name="TProperty">The data type of the property being bound.</typeparam>
        /// <param name="expression">Expression used to bind a control to a property.</param>
        /// <returns></returns>
        ITextBox<TModel, TProperty> TextBoxFor<TProperty>(Expression<Func<TModel, TProperty>> expression);

        /// <summary>
        /// Renders a text box control that is not bound. 
        /// </summary>
        /// <param name="name">Name of the control.</param>
        /// <param name="value">Initial value of the control.</param>
        /// <returns></returns>
        ITextBox<TModel, object> TextBox(string name, object value);

        /// <summary>
        /// Renders the beginning of an HTML form tag.  The tag should be used within a using block to ensure
        /// appropriate closing tags are emitted.
        /// </summary>
        /// <param name="formId">ID of the form.</param>
        /// <param name="actionName">Action to invoke when the form is submitted.</param>
        /// <param name="controllerName">Name of the controller containing the action.</param>
        /// <returns></returns>
        IHtmlForm<TModel> BeginForm(string formId, string actionName = null, string controllerName = null);

        /// <summary>
        /// Creates a new HTML form instance.  RenderBegin should be called after all configuration settings are configured
        /// and the beginning of the form needs to be rendered.  The RenderBegin call should be used within a using block to ensure
        /// appropriate closing tags are emitted.
        /// </summary>
        /// <param name="formId">ID of the form.</param>
        /// <param name="actionName">Action to invoke when the form is submitted.</param>
        /// <param name="controllerName">Name of the controller containing the action.</param>
        /// <returns></returns>
        IHtmlForm<TModel> Form(string formId, string actionName = null, string controllerName = null);

        /// <summary>
        /// Renders the beginning of an AJAX form tag.  The tag should be used within a using block to ensure
        /// appropriate closing tags are emitted.
        /// </summary>
        /// <param name="formId">ID of the form.</param>
        /// <param name="actionName">Action to invoke when the form is submitted.</param>
        /// <param name="controllerName">Name of the controller containing the action.</param>
        /// <returns></returns>
        IAjaxForm<TModel> BeginAjaxForm(string formId, string actionName = null, string controllerName = null);

        /// <summary>
        /// Creates a new AJAX form instance.  RenderBegin should be called after all configuration settings are configured
        /// and the beginnin of the form needs to be rendered.  The RenderBegin call should be used within a using block to ensure
        /// appropriate closing tags are emitted.
        /// </summary>
        /// <param name="formId">ID of the form.</param>
        /// <param name="actionName">Action to invoke when the form is submitted.</param>
        /// <param name="controllerName">Name of the controller containing the action.</param>
        /// <returns></returns>
        IAjaxForm<TModel> AjaxForm(string formId, string actionName = null, string controllerName = null);

        /// <summary>
        /// Renders a drop down list control.
        /// </summary>
        /// <typeparam name="TProperty">The data type of the property being bound.</typeparam>
        /// <param name="expression">Expression used to bind a control to a property.</param>
        /// <returns></returns>
        IDropDownList<TModel, TProperty> DropDownListFor<TProperty>(Expression<Func<TModel, TProperty>> expression);

        /// <summary>
        /// Renders a drop down list control that is not bound.
        /// </summary>
        /// <param name="name">Name of the control.</param>
        /// <param name="value">Initial value of the control.</param>
        /// <returns></returns>
        IDropDownList<TModel, object> DropDownList(string name, object value = null);

        /// <summary>
        /// Renders a tab strip container.
        /// </summary>
        /// <param name="name">Name of the tab strip.</param>
        /// <returns></returns>
        ITabStrip<TModel> TabStrip(string name);

        /// <summary>
        /// Renders the beginning of a window tag.  The tag should be used within a using block to ensure
        /// appropriate closing tags are emitted.
        /// </summary>
        /// <param name="name">Name of the window.</param>
        /// <returns></returns>
        IWindow<TModel> BeginWindow(string name);

        /// <summary>
        /// Creates a new window instance.  RenderBegin should be called after all configuration settings are configured
        /// and the beginning of the window needs to be rendered.  The RenderBegin call should be used within a using block to ensure
        /// appropriate closing tags are emitted.
        /// </summary>
        /// <param name="name">Name of the window.</param>
        /// <returns></returns>
        IWindow<TModel> Window(string name);

        /// <summary>
        /// Creates a new Grid control.
        /// </summary>
        /// <param name="name">Name of the grid.</param>
        /// <param name="keyProperty">The name of the property that is the key to the grid.</param>
        /// <returns></returns>
        IGrid<TModel> Grid(string name, string keyProperty);

        /// <summary>
        /// Creates a new Grid control.
        /// </summary>
        /// <typeparam name="TProperty">Data type of the key property.</typeparam>
        /// <param name="name">Name of the grid.</param>
        /// <param name="keyExpression">Expression that represents the key field.</param>
        /// <returns></returns>
        IGrid<TModel> Grid<TProperty>(string name, Expression<Func<TModel, TProperty>> keyExpression);

        /// <summary>
        /// Creates a button.
        /// </summary>
        /// <param name="name">Name of the button.</param>
        /// <param name="label">Text to display on the label.</param>
        /// <returns></returns>
        IButton<TModel> Button(string name, string label);

        /// <summary>
        /// Creates a submit button for a form.
        /// </summary>
        /// <param name="name">Name of the button.</param>
        /// <param name="label">Text to display on the label.</param>
        /// <returns></returns>
        IButton<TModel> SubmitButton(string name, string label);

        /// <summary>
        /// Creates a new panel container.  RenderBegin should be called after all configuration settings are configured
        /// and the beginning of the panel needs to be rendered.  The RenderBegin call should be used within a using block to ensure
        /// appropriate closing tags are emitted.
        /// </summary>
        /// <param name="name">Name of the panel.</param>
        /// <returns></returns>
        IPanel<TModel> Panel(string name);

        /// <summary>
        /// Renders a control that is used to display messages.
        /// </summary>
        /// <param name="name">Name of the display message.</param>
        /// <returns></returns>
        IMessageDisplay<TModel> MessageDisplay(string name);

        /// <summary>
        /// Renders a confirmation dialog control.
        /// </summary>
        /// <param name="name">Name of the confirmation dialog.</param>
        /// <param name="title">Title to display on the dialog.</param>
        /// <param name="message">Message to display.</param>
        /// <param name="yesAction">Javascript to invoke if the yes option is selected.</param>
        /// <returns></returns>
        IConfirmationDialog<TModel> ConfirmationDialog(string name, string title, string message, string yesAction);

        /// <summary>
        /// Renders a checkbox control.
        /// </summary>
        /// <param name="expression">Expression used to bind a control to a property.</param>
        /// <returns></returns>
        ICheckbox<TModel, bool> CheckboxFor(Expression<Func<TModel, bool>> expression);

        /// <summary>
        /// Renders a checkbox control.
        /// </summary>
        /// <param name="expression">Expression used to bind a control to a property.</param>
        /// <returns></returns>
        ICheckbox<TModel, bool?> CheckboxFor(Expression<Func<TModel, bool?>> expression);

        /// <summary>
        /// Renders a checkbox control that is not bound.
        /// </summary>
        /// <param name="name">Name of the control.</param>
        /// <param name="value">Initial value of the control.</param>
        /// <returns></returns>
        ICheckbox<TModel, bool?> Checkbox(string name, bool? value);

        /// <summary>
        /// Creates a top level menu control to which IMenuItems may be added.
        /// </summary>
        /// <param name="name">Name of the menu.</param>
        /// <returns></returns>
        IMenu<TModel> Menu(string name);

        /// <summary>
        /// Creates a menu item to add to a menu or parent menu item.
        /// </summary>
        /// <param name="name">Name of the menu item.</param>
        /// <param name="text">Text to display.</param>
        /// <param name="url">URL that should be loaded when the menu item is selected.</param>
        /// <param name="childMenuItems">Collection of child menu items.</param>
        /// <returns></returns>
        IMenuItem<TModel> MenuItem(string name, string text, string url, IList<IMenuItem<TModel>> childMenuItems = null);

        /// <summary>
        /// Creates a menu item to add to a menu or parent menu item.
        /// </summary>
        /// <param name="name">Name of the menu item.</param>
        /// <param name="text">Text to display.</param>
        /// <param name="action">Action to invoke when the menu item is selected.</param>
        /// <param name="controller">Controller containing the action.</param>
        /// <param name="area">Area containing the controller.</param>
        /// <param name="childMenuItems">Collection of child menu items.</param>
        /// <returns></returns>
        IMenuItem<TModel> MenuItem(string name, string text, string action, string controller, string area = null, IList<IMenuItem<TModel>> childMenuItems = null);

        /// <summary>
        /// Renders a hidden control.
        /// </summary>
        /// <typeparam name="TProperty">The data type of the property being bound.</typeparam>
        /// <param name="expression">Expression used to bind a control to a property.</param>
        /// <returns></returns>
        IHidden<TModel, TProperty> HiddenFor<TProperty>(Expression<Func<TModel, TProperty>> expression);

        /// <summary>
        /// Renders a hidden control that is not bound.
        /// </summary>
        /// <param name="name">Name of the control.</param>
        /// <param name="value">Initial value of the control.</param>
        /// <returns></returns>
        IHidden<TModel, object> Hidden(string name, object value);
    }
}
