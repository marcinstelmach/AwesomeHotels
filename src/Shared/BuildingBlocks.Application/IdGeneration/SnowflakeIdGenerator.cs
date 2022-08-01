using IdGen;

namespace BuildingBlocks.Application.IdGeneration;

public class SnowflakeIdGenerator : IIdGenerator
{
    private readonly IIdGenerator<long> _idGenerator;

    public SnowflakeIdGenerator(IIdGenerator<long> idGenerator)
    {
        _idGenerator = idGenerator;
    }

    public long Generate()
    {
        return _idGenerator.CreateId();
    }
}