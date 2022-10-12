using AwesomeHotels.Services.Users.Domain.Repositories;
using BuildingBlocks.Application.Bus;

namespace AwesomeHotels.Services.Users.Application.GetUsers;

public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, IEnumerable<GetUsersResponse>>
{
    private readonly IUsersRepository _usersRepository;

    public GetUsersQueryHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<IEnumerable<GetUsersResponse>> Handle(GetUsersQuery query, CancellationToken cancellationToken = default)
    {
        var users = await _usersRepository.GetUsersAsync();
        return users.Select(x => new GetUsersResponse(x.Id.Value, $"{x.FirstName} {x.LastName}"));
    }
}