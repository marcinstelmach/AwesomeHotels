using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AwesomeHotels.Services.Users.Api.Utilities;

public interface IProblemFactory
{
    IActionResult CreateProblemResult(IEnumerable<Error> errors);
}

public class ProblemFactory : IProblemFactory
{
    private readonly ProblemDetailsFactory _problemDetailsFactory;
    private readonly IHttpContextAccessor _contextAccessor;

    public ProblemFactory(ProblemDetailsFactory problemDetailsFactory, IHttpContextAccessor contextAccessor)
    {
        _problemDetailsFactory = problemDetailsFactory;
        _contextAccessor = contextAccessor;
    }

    public IActionResult CreateProblemResult(IEnumerable<Error> errors)
    {
        var modelState = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelState.AddModelError(error.Code, error.Description);
        }

        var detail = _problemDetailsFactory.CreateValidationProblemDetails(_contextAccessor.HttpContext!, modelState, 400);
        return new BadRequestObjectResult(detail);
    }
}