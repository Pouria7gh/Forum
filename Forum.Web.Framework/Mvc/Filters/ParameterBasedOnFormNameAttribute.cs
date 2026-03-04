using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Forum.Web.Framework.Mvc.Filters;

public class ParameterBasedOnFormNameAttribute : TypeFilterAttribute
{
    public ParameterBasedOnFormNameAttribute(string formKeyName, string actionParameterName) : base(typeof(ParameterBasedOnFormNameFilter))
    {
        Arguments = [formKeyName, actionParameterName];
    }

    private class ParameterBasedOnFormNameFilter : IAsyncActionFilter
    {
        private readonly string _formKeyName;
        private readonly string _actionParameterName;
        public ParameterBasedOnFormNameFilter(string formKeyName, string actionParameterName)
        {
            _formKeyName = formKeyName;
            _actionParameterName = actionParameterName;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool result = false;

            var form = await context.HttpContext.Request.ReadFormAsync();

            if (form != null)
            {
                result = form.Any(x => x.Key == _formKeyName);
            }

            context.ActionArguments[_actionParameterName] = result;

            await next();
        }
    }
}
