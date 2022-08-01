namespace BuildingBlocks.Application.Bus;

public interface IBus
{
    Task SendAsync(ICommand command);

    Task<TResult> SendAsync<TResult>(ICommand<TResult> command);

    Task<TResult> SendAsync<TResult>(IQuery<TResult> query);
}