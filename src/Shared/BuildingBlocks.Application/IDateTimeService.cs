namespace BuildingBlocks.Application;

public interface IDateTimeService
{
    DateTimeOffset GetDateTimeOffsetUtcNow();
}

public class DateTimeService : IDateTimeService
{
    public DateTimeOffset GetDateTimeOffsetUtcNow()
    {
        return DateTimeOffset.UtcNow;
    }
}