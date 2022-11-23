using AwesomeHotels.Services.Users.Domain.Entities;
using AwesomeHotels.Services.Users.Domain.Repositories;
using BuildingBlocks.Application.Bus;
using BuildingBlocks.Domain.Specifications;

namespace AwesomeHotels.Services.Users.Application.Queries.GetUsers;

public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, IEnumerable<GetUsersResponse>>
{
    private readonly IUsersRepository _usersRepository;

    public GetUsersQueryHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<IEnumerable<GetUsersResponse>> Handle(GetUsersQuery query, CancellationToken cancellationToken = default)
    {
        var users = await _usersRepository.GetUsersAsync(new EmptySpecification<User>());
        return users.Select(x => new GetUsersResponse(x.Id.Value, $"{x.FirstName} {x.LastName}"));
    }
}