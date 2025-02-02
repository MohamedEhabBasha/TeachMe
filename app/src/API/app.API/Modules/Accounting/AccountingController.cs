using Accounting.Application.Accounting.Commands.RegisterNewUser;
using Accounting.Application.Accounting.Queries.Login;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace app.API.Modules.Accounting;

[Route("api/[controller]")]
[ApiController]

public class AccountingController(ISender sender) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<RegisterNewUserResponse>> RegisterNewUser(RegisterNewUserRequest request)
    {
        var command = request.Adapt<RegisterNewUserCommand>();

        var result = await sender.Send(command);

        var response = result.Adapt<RegisterNewUserResponse>();

        return Created($"/{response.UserDto.Id}", response);
    }
    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginRequest request)
    {
        var command = request.Adapt<LoginQuery>();

        var result = await sender.Send(command);

        var response = result.Adapt<LoginResponse>();

        return Created($"/{response.UserDto.Id}", response);
    }
}
