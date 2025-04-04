using Ganss.Xss;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;
using System.Runtime.CompilerServices;

namespace EVDataApi.Helpers
{
    public class EVModelActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var t = filterContext.ActionArguments;
            foreach ( var v in t )
            {
                if (v.Value != null)
                {
                    var value = v.Value;
                    var res = ValidateRecursively(value);
                    if (!res) 
                    {
                        filterContext.Result = new StatusCodeResult((int)HttpStatusCode.BadRequest);
                    }
                }
            }
        }

        private bool ValidateRecursively(object obj)
        {
            var sanitizer = new HtmlSanitizer();

            var typeOfValue = obj.GetType();

            if (typeOfValue.IsPrimitive)
            {
                return true;
            }

            if (typeOfValue == typeof(string))
            {
                var s = sanitizer.Sanitize(obj.ToString());
                return (s == obj.ToString()) ? true: false;
            } 
            else 
            {
                var propertiesOfObject = typeOfValue.GetProperties();

                // is gonna be true, until probing it is not
                bool result = true;
                foreach (var item in propertiesOfObject)
                {
                    var valueOfObject = item.GetValue(obj);
                    result = ValidateRecursively(valueOfObject);
                    if (result == false) { break; }
                }
                return (result == false)? false : true;

            }

            return true;
        }

    }

}
