using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace GA.BDC.Web.MGP.Helpers
{
    public static class HtmlHelpers
    {
        /// <summary>
        /// Creates a menu link using the wrapper received and according to the current action, it will add the active class to work with bootstrap
        /// </summary>
        /// <param name="htmlHelper">HTML Helper</param>
        /// <param name="linkText">Text to be displayed</param>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        /// <param name="htmlWrapper">HTML tag wrapper, default: li</param>
        /// <param name="activeClass">HTML active class, default: active</param>
        /// <returns></returns>
        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues = null, object htmlAttributes = null, string htmlWrapper = "li", string activeClass = "active")
        {
            var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

            var builder = new TagBuilder(htmlWrapper)
            {
                InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes).ToHtmlString()
            };

            if (controllerName == currentController && actionName == currentAction)
                builder.AddCssClass(activeClass);

            return new MvcHtmlString(builder.ToString());
        }
    }
}