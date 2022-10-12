using AwesomeHotels.Services.Users.Application.AddUser;
using AwesomeHotels.Services.Users.Application.GetUsers;
using BuildingBlocks.Application.Bus;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeHotels.Services.Users.Api.Controllers.Users.v1;

[ApiVersion("1.0")]
[Route("api/v{ver:apiVersion}/users")]
public class UsersController : BaseController
{
    private readonly IBus _bus;
    private readonly IMapper _mapper;

    public UsersController(IBus bus, IMapper mapper)
    {
        _bus = bus;
        _mapper = mapper;
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
            errors => Problem(errors));
    }
}