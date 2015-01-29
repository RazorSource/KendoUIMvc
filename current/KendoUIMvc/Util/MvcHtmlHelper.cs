using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Html;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace KendoUIMvc.Util
{
    public class MvcHtmlHelper
    {
        /// <summary>
        /// Gets the ID for the HTML control that is created for the passed in expression.  For nested expressions
        /// the outer most parent class is ignored in the name.  As an example:
        /// MyClass.MyProperty => MyProperty
        /// MyClass.MyInnerClass.MyProperty => MyInnerClass_MyProperty
        /// </summary>
        /// <typeparam name="TModel">Type of model being bound.</typeparam>
        /// <typeparam name="TProperty">The type of property being bound.</typeparam>
        /// <param name="htmlHelper">Instance of HtmlHelper being used to generate the current view.</param>
        /// <param name="expression">The bound expression to evaluate for a property.</param>
        /// <returns>The ID of the corresponding control that will be generated as a string.</returns>
        public static string GetExpressionPropertyId<TModel, TProperty>(HtmlHelper<TModel> htmlHelper,
                Expression<Func<TModel, TProperty>> expression)
        {
            string property = ExpressionHelper.GetExpressionText(expression);
            return htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(property);
        }

        /// <summary>
        /// Gets the name for the HTML control that is created for the passed in expression.  For nested expressions
        /// the outer most parent class is ignored in the name.  As an example:
        /// MyClass.MyProperty => MyProperty
        /// MyClass.MyInnerClass.MyProperty => MyInnerClass_MyProperty
        /// </summary>
        /// <typeparam name="TModel">Type of model being bound.</typeparam>
        /// <typeparam name="TProperty">The type of property being bound.</typeparam>
        /// <param name="htmlHelper">Instance of HtmlHelper being used to generate the current view.</param>
        /// <param name="expression">The bound expression to evaluate for a property.</param>
        /// <returns>The name of the corresponding control that will be generated as a string.</returns>
        public static string GetExpressionPropertyName<TModel, TProperty>(HtmlHelper<TModel> htmlHelper,
                Expression<Func<TModel, TProperty>> expression)
        {
            string property = ExpressionHelper.GetExpressionText(expression);
            return htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(property);
        }

        /// <summary>
        /// Writes the provided HTML directly to the view without encoding.
        /// </summary>
        /// <typeparam name="TModel">Type of model being bound.</typeparam>
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

        /// <summary>
        /// Returns the corresponding boolean value for the expression.
        /// </summary>
        /// <typeparam name="TModel">Type of model being bound.</typeparam>
        /// <param name="htmlHelper">Instance of HtmlHelper being used to generate the current view.</param>
        /// <param name="expression">The bound expression to evaluate for a property.</param>
        /// <returns>The boolean value for the provided property.</returns>
        public static bool GetBooleanValue<TModel, TProperty>(System.Web.Mvc.HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            bool boolValue;
            string stringBool = htmlHelper.ValueFor(expression).ToString();
            if (stringBool == null)
            {
                return false;
            }

            bool.TryParse(stringBool, out boolValue);

            return boolValue;
        }

        /// <summary>
        /// Retrieves the text only portion of text to display for the model property associated with the given expression.  The text
        /// is not wrapped in any additional tags.
        /// </summary>
        /// <typeparam name="TModel">Type of model being bound.</typeparam>
        /// <typeparam name="TProperty">Type of property being bound.</typeparam>
        /// <param name="htmlHelper">Instance of HtmlHelper being used to generate the current view.</param>
        /// <param name="expression">The bound expression to evaluate for the display text.</param>
        /// <returns>The text to display for the property.</returns>
        public static string GetDisplayText<TModel, TProperty>(System.Web.Mvc.HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            MemberExpression memberAccessExpression = (MemberExpression)expression.Body;
            object[] displayAttribs = memberAccessExpression.Member.GetCustomAttributes(
                typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), true);

            if (displayAttribs.Length > 0)
            {
                string resourceName = ((System.ComponentModel.DataAnnotations.DisplayAttribute)displayAttribs[0]).Name;

                //Use the resource file that corresponds to that type instead of ApplicationText type.
                Type resourceType = ((System.ComponentModel.DataAnnotations.DisplayAttribute)displayAttribs[0]).ResourceType;
                if (resourceType != null)
                {
                    PropertyInfo resourceTypeProperty = resourceType.GetProperty("ResourceManager");
                    System.Resources.ResourceManager resourceManager = (System.Resources.ResourceManager)resourceTypeProperty.GetMethod.Invoke(null, null);
                    return resourceManager.GetString(resourceName);
                }
            }
            
            //If a display attribute is not tagged on the property use the display name.
            return GetExpressionPropertyId(htmlHelper, expression);
        }
    }
}
