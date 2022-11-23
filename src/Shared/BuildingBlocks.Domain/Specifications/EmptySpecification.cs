using System.Linq.Expressions;

namespace BuildingBlocks.Domain.Specifications;

public class EmptySpecification<T> : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        return x => true;
    }
}