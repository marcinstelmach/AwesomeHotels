using AwesomeHotels.Services.Users.Application.GetUsers;
using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeHotels.Services.Users.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{ver:apiVersion}/users")]
public class UsersController : ControllerBase
{
    private readonly IBus _bus;

    public UsersController(IBus bus)
    {
        _bus = bus;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsersAsync()
    {
        var query = new GetUsersQuery();
        var response = await _bus.SendAsync(query);

        return Ok(response);
    }
}