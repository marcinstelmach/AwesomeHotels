using BuildingBlocks.Application.Bus;
using MediatR;

namespace BuildingBlocks.Infrastructure.Bus
{
    public class MediatRBus : IBus
    {
        private readonly IMediator _mediator;

        public MediatRBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendAsync(ICommand command)
        {
            await _mediator.Send(command);
        }

        public async Task<TResult> SendAsync<TResult>(ICommand<TResult> command)
        {
            return await _mediator.Send(command);
        }

        public async Task<TResult> SendAsync<TResult>(IQuery<TResult> query)
        {
            return await _mediator.Send(query);
        }
    }
}