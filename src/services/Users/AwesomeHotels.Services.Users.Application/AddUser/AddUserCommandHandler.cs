using BuildingBlocks.Application.Bus;
using ErrorOr;

namespace AwesomeHotels.Services.Users.Application.AddUser;

public class AddUserCommandHandler : ICommandHandler<AddUserCommand, ErrorOr<long>>
{
    public async Task<ErrorOr<long>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return 15;
    }
}