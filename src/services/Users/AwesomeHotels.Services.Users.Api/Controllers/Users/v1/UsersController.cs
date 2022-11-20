using AwesomeHotels.Services.Users.Api.Utilities;
using AwesomeHotels.Services.Users.Application.Commands.AddUser;
using AwesomeHotels.Services.Users.Application.Queries.GetUsers;
using BuildingBlocks.Application.Bus;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeHotels.Services.Users.Api.Controllers.Users.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{ver:apiVersion}/users")]
public class UsersController : ControllerBase
{
    private readonly IBus _bus;
    private readonly IMapper _mapper;
    private readonly IProblemFactory _problemFactory;

    public UsersController(IBus bus, IMapper mapper, IProblemFactory problemFactory)
    {
        _bus = bus;
        _mapper = mapper;
        _problemFactory = problemFactory;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsersAsync()
    {
        var query = new GetUsersQuery();
        var response = await _bus.SendAsync(query);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddUserAsync(AddUserRequest request)
    {
        var command = _mapper.Map<AddUserCommand>(request);

        var result = await _bus.SendAsync(command);
        return result.Match(
            value => Ok(new {Id = value}), 
            errors => _problemFactory.CreateProblemResult(errors));
    }
}