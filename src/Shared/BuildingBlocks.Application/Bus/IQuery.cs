using MediatR;

namespace BuildingBlocks.Application.Bus;

public interface IQuery<out TResult> : IRequest<TResult>
{
}