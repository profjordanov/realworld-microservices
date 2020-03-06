using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using YngStrs.Common;

namespace Articles.Api.Infrastructure.Filters
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var errors = context
                .ModelState
                .Values
                .SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

            context.Result = new BadRequestObjectResult(Error.Validation(errors));
        }
    }
}