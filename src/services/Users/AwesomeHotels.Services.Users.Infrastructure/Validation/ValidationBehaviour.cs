using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace AwesomeHotels.Services.Users.Infrastructure.Validation;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var errors = _validators
            .Select(validator => validator.Validate(request))
            .Where(validationResult => !validationResult.IsValid)
            .ToArray();

        if (!errors.Any())
        {
            return await next();
        }

        return PrepareErrors(errors);
    }

    private static TResponse PrepareErrors(IEnumerable<ValidationResult> validationResults)
    {
        var errors = validationResults
            .SelectMany(x => x.Errors)
            .ToArray();

        var validationErrors = errors
            .Select(x => Error.Validation(x.PropertyName, x.ErrorMessage))
            .ToList();

        try
        {
            return (TResponse)(dynamic)validationErrors;
        }
        catch
        {
            throw new ValidationException(errors);
        }
    }
}