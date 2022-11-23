using AwesomeHotels.Services.Users.Domain.Entities;
using AwesomeHotels.Services.Users.Domain.Repositories;
using BuildingBlocks.Application.Bus;
using BuildingBlocks.Domain.Specifications;
using MapsterMapper;

namespace AwesomeHotels.Services.Users.Application.Queries.GetUsers;

public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, IEnumerable<GetUsersResponse>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetUsersResponse>> Handle(GetUsersQuery query, CancellationToken cancellationToken = default)
    {
        var users = await _usersRepository.GetUsersAsync(new EmptySpecification<User>());
        var response = _mapper.Map<IEnumerable<GetUsersResponse>>(users);
        return response;
    }
}