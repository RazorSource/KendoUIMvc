using KendoUIMvc.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using KendoUIMvc.Util;
using CommonMvc.Razor.Controls;

namespace KendoUIMvc
{
    public class KendoUIHtmlHelper<TModel>
    {
        protected HtmlHelper<TModel> htmlHelper;
        protected AjaxHelper<TModel> ajaxHelper;
        protected TModel model;

        public KendoUIHtmlHelper(TModel model, HtmlHelper<TModel> htmlHelper, AjaxHelper<TModel> ajaxHelper)
        {
            this.model = model;
            this.htmlHelper = htmlHelper;
            this.ajaxHelper = ajaxHelper;
        }

        /// <summary>
        /// Renders a date picker control.
        /// </summary>
        /// <param name="expression">Expression used to bind a control to a property.</param>
        /// <returns></returns>
        public IDatePicker<TModel, DateTime?> DatePickerFor(Expression<Func<TModel, DateTime?>> expression)
        {
            return new DatePicker<TModel, DateTime?>(this.htmlHelper, expression);
        }

        /// <summary>
        /// Renders a date picker control.
        /// </summary>
        /// <param name="expression">Expression used to bind a control to a property.</param>
        /// <returns></returns>
        public IDatePicker<TModel, DateTime> DatePickerFor(Expression<Func<TModel, DateTime>> expression)
        {
            return new DatePicker<TModel, DateTime>(this.htmlHelper, expression);
        }

        /// <summary>
        /// Renders a date picker control that is not bound.
        /// </summary>
        /// <param name="name">Name of the control.</param>
        /// <param name="value">Initial value of the control.</param>
        /// <returns></returns>
        public IDatePicker<TModel, DateTime> DatePicker(string name, DateTime? value = null)
        {
            return new DatePicker<TModel, DateTime>(this.htmlHelper, name, value);
        }

        /// <summary>
        /// Renders a text box control.
        /// </summary>
        /// <typeparam name="TProperty">The data type of the property being bound.</typeparam>
        /// <param name="expression">Expression used to bind a control to a property.</param>
        /// <returns></returns>
        public TextBox<TModel, TProperty> TextBoxFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return new TextBox<TModel, TProperty>(this.htmlHelper, expression);
        }

        /// <summary>
        /// Renders a text box control that is not bound. 
        /// </summary>
        /// <param name="name">Name of the control.</param>
        /// <param name="value">Initial value of the control.</param>
        /// <returns></returns>
        public TextBox<TModel, object> TextBox(string name, object value)
        {
            return new TextBox<TModel, object>(this.htmlHelper, name, value);
        }

        /// <summary>
        /// Renders the beginning of an HTML form tag.  The tag should be used within a using block to ensure
        /// appropriate closing tags are emitted.
        /// </summary>
        /// <param name="formId">ID of the form.</param>
        /// <param name="actionName">Action to invoke when the form is submitted.</param>
        /// <param name="controllerName">Name of the controller containing the action.</param>
        /// <returns></returns>
        public IHtmlForm<TModel> BeginForm(string formId, string actionName = null, string controllerName = null)
        {
            return new HtmlForm<TModel>(this.htmlHelper, formId, actionName, controllerName)
                .RenderBegin();
        }

        /// <summary>
        /// Creates a new HTML form instance.  RenderBegin should be called after all configuration settings are configured
        /// and the beginnin of the form needs to be rendered.  The RenderBegin call should be used within a using block to ensure
        /// appropriate closing tags are emitted.
        /// </summary>
        /// <param name="formId">ID of the form.</param>
        /// <param name="actionName">Action to invoke when the form is submitted.</param>
        /// <param name="controllerName">Name of the controller containing the action.</param>
        /// <returns></returns>
        public IHtmlForm<TModel> Form(string formId, string actionName = null, string controllerName = null)
        {
            return new HtmlForm<TModel>(this.htmlHelper, formId, actionName, controllerName);
        }

        /// <summary>
        /// Renders the beginning of an AJAX form tag.  The tag should be used within a using block to ensure
        /// appropriate closing tags are emitted.
        /// </summary>
        /// <param name="formId">ID of the form.</param>
        /// <param name="actionName">Action to invoke when the form is submitted.</param>
        /// <param name="controllerName">Name of the controller containing the action.</param>
        /// <returns></returns>
        public IAjaxForm<TModel> BeginAjaxForm(string formId, string actionName = null, string controllerName = null)
        {
            return new AjaxForm<TModel>(this.htmlHelper, this.ajaxHelper, formId, actionName, controllerName)
                .RenderBegin();
        }

        /// <summary>
        /// Creates a new AJAX form instance.  RenderBegin should be called after all configuration settings are configured
        /// and the beginnin of the form needs to be rendered.  The RenderBegin call should be used within a using block to ensure
        /// appropriate closing tags are emitted.
        /// </summary>
        /// <param name="formId">ID of the form.</param>
        /// <param name="actionName">Action to invoke when the form is submitted.</param>
        /// <param name="controllerName">Name of the controller containing the action.</param>
        /// <returns></returns>
        public IAjaxForm<TModel> AjaxForm(string formId, string actionName = null, string controllerName = null)
        {
            return new AjaxForm<TModel>(this.htmlHelper, this.ajaxHelper, formId, actionName, controllerName);
        }

        /// <summary>
        /// Renders a drop down list control.
        /// </summary>
        /// <typeparam name="TProperty">The data type of the property being bound.</typeparam>
        /// <param name="expression">Expression used to bind a control to a property.</param>
        /// <returns></returns>
        public IDropDownList<TModel, TProperty> DropDownListFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return new DropDownList<TModel, TProperty>(this.htmlHelper, expression);
        }

        /// <summary>
        /// Renders a drop down list control that is not bound.
        /// </summary>
        /// <param name="name">Name of the control.</param>
        /// <param name="value">Initial value of the control.</param>
        /// <returns></returns>
        public IDropDownList<TModel, TProperty> DropDownList<TProperty>(string name, TProperty value = default(TProperty))
        {
            return new DropDownList<TModel, TProperty>(this.htmlHelper, name, value);
        }

        /// <summary>
        /// Renders a tab strip container.
        /// </summary>
        /// <param name="name">Name of the tab strip.</param>
        /// <returns></returns>
        public ITabStrip<TModel> TabStrip(string name)
        {
            return new TabStrip<TModel>(this.htmlHelper, name);
        }

        /// <summary>
        /// Renders the beginning of a window tag.  The tag should be used within a using block to ensure
        /// appropriate closing tags are emitted.
        /// </summary>
        /// <param name="name">Name of the window.</param>
        /// <returns></returns>
        public IWindow<TModel> BeginWindow(string name)
        {
            return new Window<TModel>(this.htmlHelper, this.ajaxHelper, name)
                .RenderBegin();
        }

        /// <summary>
        /// Creates a new window instance.  RenderBegin should be called after all configuration settings are configured
        /// and the beginning of the window needs to be rendered.  The RenderBegin call should be used within a using block to ensure
        /// appropriate closing tags are emitted.
        /// </summary>
        /// <param name="name">Name of the window.</param>
        /// <returns></returns>
        public Window<TModel> Window(string name)
        {
            return new Window<TModel>(this.htmlHelper, this.ajaxHelper, name);
        }

        /// <summary>
        /// Creates a new Grid control.
        /// </summary>
        /// <param name="name">Name of the grid.</param>
        /// <param name="keyProperty">The name of the property that is the key to the grid.</param>
        /// <returns></returns>
        public IGrid<TModel> Grid(string name, string keyProperty)
        {
            return new RazorGrid<TModel>(this.htmlHelper, this.ajaxHelper, name, keyProperty);
        }

        /// <summary>
        /// Creates a new Grid control.
        /// </summary>
        /// <typeparam name="TProperty">Data type of the key property.</typeparam>
        /// <param name="name">Name of the grid.</param>
        /// <param name="keyExpression">Expression that represents the key field.</param>
        /// <returns></returns>
        IGrid<TModel> Grid<TProperty>(string name, Expression<Func<TModel, TProperty>> keyExpression)
        {
            return new RazorGrid<TModel, TProperty>(this.htmlHelper, this.ajaxHelper, name, keyExpression);
        }

        /// <summary>
        /// Creates a button.
        /// </summary>
        /// <param name="name">Name of the button.</param>
        /// <param name="label">Text to display on the label.</param>
        /// <returns></returns>
        public IButton<TModel> Button(string name, string label)
        {
            return new Button<TModel>(this.htmlHelper, name, label);
        }

        /// <summary>
        /// Creates a submit button for a form.
        /// </summary>
        /// <param name="name">Name of the button.</param>
        /// <param name="label">Text to display on the label.</param>
        /// <returns></returns>
        public IButton<TModel> SubmitButton(string name, string label)
        {
            return new Button<TModel>(this.htmlHelper, name, label)
                .SetSubmit(true)
                .SetPrimary(true);
        }

        /// <summary>
        /// Creates a new panel container.  RenderBegin should be called after all configuration settings are configured
        /// and the beginning of the panel needs to be rendered.  The RenderBegin call should be used within a using block to ensure
        /// appropriate closing tags are emitted.
        /// </summary>
        /// <param name="name">Name of the panel.</param>
        /// <returns></returns>
        public Panel<TModel> Panel(string name)
        {
            return new Panel<TModel>(this.htmlHelper, name);
        }

        /// <summary>
        /// Renders a control that is used to display messages.
        /// </summary>
        /// <param name="name">Name of the message display.</param>
        /// <returns></returns>
        public IMessageDisplay<TModel> MessageDisplay(string name)
        {
            return new Notification<TModel>(this.htmlHelper, name);
        }

        /// <summary>
        /// Renders a confirmation dialog control.
        /// </summary>
        /// <param name="name">Name of the confirmation dialog.</param>
        /// <param name="title">Title to display on the dialog.</param>
        /// <param name="message">Message to display.</param>
        /// <param name="yesAction">Javascript to invoke if the yes option is selected.</param>
        /// <returns></returns>
        public IConfirmationDialog<TModel> ConfirmationDialog(string name, string title, string message, string yesAction)
        {
            return new ConfirmationDialog<TModel>(this.htmlHelper, name, title, message, yesAction);
        }

        /// <summary>
        /// Renders a checkbox control.
        /// </summary>
        /// <param name="expression">Expression used to bind a control to a property.</param>
        /// <returns></returns>
        public ICheckbox<TModel, bool> CheckboxFor(Expression<Func<TModel, bool>> expression)
        {
            return new Checkbox<TModel, bool>(this.htmlHelper, expression);
        }

        /// <summary>
        /// Renders a checkbox control.
        /// </summary>
        /// <param name="expression">Expression used to bind a control to a property.</param>
        /// <returns></returns>
        public ICheckbox<TModel, bool?> CheckboxFor(Expression<Func<TModel, bool?>> expression)
        {
            return new Checkbox<TModel, bool?>(this.htmlHelper, expression);
        }

        /// <summary>
        /// Renders a checkbox control that is not bound.
        /// </summary>
        /// <param name="name">Name of the control.</param>
        /// <param name="value">Initial value of the control.</param>
        /// <returns></returns>
        public ICheckbox<TModel, bool?> Checkbox(string name, bool? value)
        {
            return new Checkbox<TModel, bool?>(this.htmlHelper, name, value);
        }

        /// <summary>
        /// Creates a top level menu control to which IMenuItems may be added.
        /// </summary>
        /// <param name="name">Name of the menu.</param>
        /// <returns></returns>
        public IMenu<TModel> Menu(string name)
        {
            return new Menu<TModel>(this.htmlHelper, name);
        }

        /// <summary>
        /// Creates a menu item to add to a menu or parent menu item.
        /// </summary>
        /// <param name="name">Name of the menu item.</param>
        /// <param name="text">Text to display.</param>
        /// <param name="url">URL that should be loaded when the menu item is selected.</param>
        /// <param name="childMenuItems">Collection of child menu items.</param>
        /// <returns></returns>
        public IMenuItem<TModel> MenuItem(string name, string text, string url, IList<IMenuItem<TModel>> childMenuItems = null)
        {
            return new MenuItem<TModel>(this.htmlHelper, name, text, url, childMenuItems);
        }

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
        public IMenuItem<TModel> MenuItem(string name, string text, string action, string controller, string area = null, IList<IMenuItem<TModel>> childMenuItems = null)
        {
            return new MenuItem<TModel>(this.htmlHelper, name, text, MvcHtmlHelper.GetActionUrl(this.htmlHelper, action, controller, area), childMenuItems);
        }

        /// <summary>
        /// Renders a hidden control.
        /// </summary>
        /// <typeparam name="TProperty">The data type of the property being bound.</typeparam>
        /// <param name="expression">Expression used to bind a control to a property.</param>
        /// <returns></returns>
        public IHidden<TModel, TProperty> HiddenFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return new Hidden<TModel, TProperty>(this.htmlHelper, expression);
        }

        /// <summary>
        /// Renders a hidden control that is not bound.
        /// </summary>
        /// <param name="name">Name of the control.</param>
        /// <param name="value">Initial value of the control.</param>
        /// <returns></returns>
        public IHidden<TModel, object> Hidden(string name, object value)
        {
            return new Hidden<TModel, object>(this.htmlHelper, name, value);
        }
    }
}
