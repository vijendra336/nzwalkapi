using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NZWalk.API.CustomActionFilter
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        // Call before the actual action method execute 
        public override void OnActionExecuting(ActionExecutingContext context)
        {
           if(context.ModelState.IsValid == false)
            {
                context.Result = new BadRequestResult();
            }
        }
    }
}
