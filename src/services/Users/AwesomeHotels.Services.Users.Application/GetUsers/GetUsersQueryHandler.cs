using BuildingBlocks.Application;

namespace AwesomeHotels.Services.Users.Application.GetUsers;

public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, GetUsersResponse>
{
    public async Task<GetUsersResponse> Handle(GetUsersQuery query, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        return new GetUsersResponse(1, "Roman");
    }
}