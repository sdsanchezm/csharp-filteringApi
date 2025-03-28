using Ganss.Xss;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EVDataApi.Helpers
{
    public class EVModelActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var t = filterContext.ActionArguments;
            foreach ( var v in t )
            {
                var value = v.Value;
                var typeOfValue = value.GetType();
            }
            var sanitizer = new HtmlSanitizer();
            var s = sanitizer.Sanitize("<span id=\"PING_IFRAME_FORM_DETECTION\" style=\"display: none;\"></span>");

            filterContext.Result = ;
        }
    }
}
