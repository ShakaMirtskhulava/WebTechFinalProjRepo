using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Filters;

public class PostPutActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        //Get the logger from the context
        var logger = (ILogger<PostPutActionFilter>)context.HttpContext.RequestServices.GetService(typeof(ILogger<PostPutActionFilter>))!;

        if (context.ActionArguments.Count == 0)
        {
            context.Result = new BadRequestObjectResult("No data found");
            logger.LogWarning("No data found");
        }
        else if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult("Please fill all the requirement fields. And stop trying to circumvent server side validation.");
            logger.LogWarning("Server side validation circumvention trieal.");
        }
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {

    }
}
