using System.Linq;
using AutoFixture.Xunit2;
using AwesomeHotels.Services.Users.Api.Utilities;
using ErrorOr;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NSubstitute;
using Xunit;

namespace AwesomeHotels.Services.Users.Api.Tests
{
    public class ProblemFactoryShould
    {
        private readonly ProblemDetailsFactory _problemDetailsFactory;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ProblemFactory _sut;

        public ProblemFactoryShould()
        {
            _problemDetailsFactory = Substitute.For<ProblemDetailsFactory>();
            _contextAccessor = Substitute.For<IHttpContextAccessor>();
            _sut = new ProblemFactory(_problemDetailsFactory, _contextAccessor);
        }

        [Theory]
        [AutoData]
        public void CreateValidationProblemDetailsInProblemDetailsFactory(Error error)
        {
            // Arrange
            var errors = new[] { error };
            var httpContext = Substitute.For<HttpContext>();
            _contextAccessor.HttpContext.Returns(httpContext);

            // Act
            _sut.CreateProblemResult(errors);

            // Assert
            _problemDetailsFactory.Received(1).CreateValidationProblemDetails(
                httpContext,
                Arg.Is<ModelStateDictionary>(x =>
                    x.Count == 1 &&
                    x.First().Key == error.Code &&
                    x.First().Value!.Errors.First().ErrorMessage == error.Description),
                StatusCodes.Status400BadRequest);
        }

        [Theory]
        [AutoData]
        public void ReturnBadRequestObjectResultWithProblemDetail(Error[] errors, ValidationProblemDetails validationProblemDetails)
        {
            // Arrange
            var httpContext = Substitute.For<HttpContext>();
            _contextAccessor.HttpContext.Returns(httpContext);
            _problemDetailsFactory.CreateValidationProblemDetails(Arg.Any<HttpContext>(), Arg.Any<ModelStateDictionary>(), Arg.Any<int?>()).Returns(validationProblemDetails);

            // Act
            var result = _sut.CreateProblemResult(errors);

            // Assert
            result
                .Should().BeOfType<BadRequestObjectResult>()
                .Which.As<BadRequestObjectResult>()
                .Value
                .Should().BeOfType<ValidationProblemDetails>()
                .Which.As<ValidationProblemDetails>();
        }
    }
}