using ErrorOr;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace BuildingBlocks.UnitTests.Common;

public class ErrorAssertions : ReferenceTypeAssertions<IErrorOr, ErrorAssertions>
{
    public ErrorAssertions(IErrorOr subject)
        : base(subject)
    {
    }

    protected override string Identifier => "error";

    public AndConstraint<ErrorAssertions> FailedWithError(Error error, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject.Errors)
            .ForCondition(x => x.Contains(error))
            .FailWith($"Expected error to be failed, with error {error.Description}");

        return new AndConstraint<ErrorAssertions>(this);
    }
}