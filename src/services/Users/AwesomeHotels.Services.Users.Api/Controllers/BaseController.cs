using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AwesomeHotels.Services.Users.Api.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected IActionResult Problem(IEnumerable<Error> errors)
    {
        foreach (var error in errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }

        var detail = ProblemDetailsFactory.CreateValidationProblemDetails(HttpContext, ModelState, 400);

        return BadRequest(detail);
    }
}