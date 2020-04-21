using bookystufflocal.domain.DomainLayer.BaseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace bookystufflocal.api.Helpers.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!(context.Exception is { } exception)) return;
            var response = new ApiResponse<object>
            {
                Message = "There was a problem with the request",
                StatusErrorCode = StatusCodes.Status500InternalServerError,
                Exception = exception.Message,
                InnerException = exception.InnerException?.Message,
                Success = false
            };

            context.Result = new ObjectResult(response);
            context.ExceptionHandled = true;
        }
    }
}
