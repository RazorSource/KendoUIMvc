﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace KendoUIMvc.Util
{
    public class MvcHtmlHelper
    {
        /// <summary>
        /// Gets the ID for the HTML control that is created for the passed in expression.  For nested expressions
        /// the outer most parent class is ignored in the name.  As an example:
        /// MyClass.MyProperty => MyProperty
        /// MyClass.MyInnerClass.MyProperty =? MyInnerClass_MyProperty
        /// </summary>
        /// <typeparam name="TModel">Type of model being bound.</typeparam>
        /// <typeparam name="TProperty">The type of property being bound.</typeparam>
        /// <param name="htmlHelper">Instance of HtmlHelper being used to generate the current view.</param>
        /// <param name="expression">The bound expression to evaluate for a property.</param>
        /// <returns>The name of the corresponding control that will be generated as a string.</returns>
        public static string GetExpressionPropertyId<TModel, TProperty>(HtmlHelper<TModel> htmlHelper,
                Expression<Func<TModel, TProperty>> expression)
        {
            string property = ExpressionHelper.GetExpressionText(expression);
            return htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(property);
        }

        /// <summary>
        /// Writes the provided HTML directly to the view without encoding.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="htmlHelper">HtmlHelper instance used to retrieve the view.</param>
        /// <param name="html">HTML text to write out to the view.</param>
        public static void WriteUnencodedContent<TModel>(HtmlHelper<TModel> htmlHelper, string html)
        {
            htmlHelper.ViewContext.Writer.WriteLine(html);
        }

        public static string GetActionUrl<TModel>(HtmlHelper<TModel> htmlHelper, string action, string controller, string area)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            RouteValueDictionary routeValues = new RouteValueDictionary();

            if (area != null)
            {
                routeValues.Add("area", area);
            }

            return urlHelper.Action(action, controller, routeValues);
        }

        /// <summary>
        /// Returns a string to use in javascript for a boolean value.
        /// </summary>
        /// <param name="value">The boolean value.</param>
        /// <returns>If the passed in value is True, "true" is returned, else "false" is returned.</returns>
        public static string GetJavascriptBoolean(bool value)
        {
            return value ? "true" : "false";
        }
    }
}
