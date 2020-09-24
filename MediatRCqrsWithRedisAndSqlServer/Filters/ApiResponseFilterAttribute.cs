using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MediatRCqrs.Filters
{
    public class ApiResponseFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Result is BadRequestResult)
            {
                var problemDetails=new ValidationProblemDetails(context.ModelState);
                var validationErrors = problemDetails.Errors;
                context.Result = new JsonResult(validationErrors) { StatusCode = StatusCodes.Status400BadRequest};
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }
    }
}

