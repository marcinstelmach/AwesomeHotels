using ErrorOr;

namespace BuildingBlocks.UnitTests.Common;

public static class ErrorExtensions
{
    public static ErrorAssertions ErrorShould(this IErrorOr error)
    {
        return new ErrorAssertions(error);
    }
}