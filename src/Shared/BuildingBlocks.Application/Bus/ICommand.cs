using MediatR;

namespace BuildingBlocks.Application.Bus;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResult> : IRequest<TResult>
{
}