using MediatR;

namespace BuildingBlocks.Application;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResult> : IRequest<TResult>
{
}