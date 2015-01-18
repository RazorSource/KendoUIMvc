using KendoUIMvc.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

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

        public DatePicker<TModel, DateTime?> DatePickerFor(Expression<Func<TModel, DateTime?>> expression)
        {
            return new DatePicker<TModel, DateTime?>(this.htmlHelper, expression);
        }

        public DatePicker<TModel, DateTime> DatePickerFor(Expression<Func<TModel, DateTime>> expression)
        {
            return new DatePicker<TModel, DateTime>(this.htmlHelper, expression);
        }

        public DatePicker<TModel, DateTime> DatePicker(string name, DateTime? value = null)
        {
            return new DatePicker<TModel, DateTime>(this.htmlHelper, name, value);
        }

        public TextBox<TModel, TProperty> TextBoxFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return new TextBox<TModel, TProperty>(this.htmlHelper, expression);
        }

        public HtmlForm<TModel> BeginForm(string formId, string actionName, string controllerName)
        {
            HtmlForm<TModel> htmlForm = new HtmlForm<TModel>(this.htmlHelper, formId, actionName, controllerName);

            return htmlForm.RenderBegin();
        }

        public HtmlForm<TModel> Form(string formId, string actionName, string controllerName)
        {
            HtmlForm<TModel> htmlForm = new HtmlForm<TModel>(this.htmlHelper, formId, actionName, controllerName);

            return htmlForm;
        }

        public DropDownList<TModel, TProperty> DropDownListFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return new DropDownList<TModel, TProperty>(this.htmlHelper, expression);
        }

        public DropDownList<TModel, TProperty> DropDownList<TProperty>(string name, object value = null)
        {
            return new DropDownList<TModel, TProperty>(this.htmlHelper, name, value);
        }

        public TabStrip<TModel> TabStrip(string name)
        {
            return new TabStrip<TModel>(this.htmlHelper, name);
        }
    }
}
