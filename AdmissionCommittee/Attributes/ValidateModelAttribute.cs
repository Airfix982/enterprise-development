using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdmissionCommittee.Domain.Attributes
{
    /// <summary>
    /// Attribute for validating the model state before executing an action.
    /// If the model is not valid, returns a BadRequest result.
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Checks if the model state is valid before the action executes.
        /// If the model is invalid, sets the result to a BadRequestObjectResult.
        /// </summary>
        /// <param name="context">The context for the action execution.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
