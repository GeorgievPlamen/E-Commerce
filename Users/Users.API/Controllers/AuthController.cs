using Microsoft.AspNetCore.Mvc;
using Users.Core.DTO;
using Users.Core.ServiceContracts;

namespace Users.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IUsersService usersService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        if (registerRequest is null)
            return BadRequest("Invalid");

        var response = await usersService.Register(registerRequest);

        if (response is null || !response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        if (loginRequest is null)
            return BadRequest("Invalid");

        var response = await usersService.Login(loginRequest);

        if (response is null || !response.Success)
            return Unauthorized(response);

        return Ok(response);
    }
}