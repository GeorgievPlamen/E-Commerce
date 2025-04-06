using Microsoft.AspNetCore.Mvc;
using Users.Core.DTO;
using Users.Core.ServiceContracts;

namespace Users.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUsersService usersService) : ControllerBase
{
    [HttpGet("{userID}")]
    public async Task<IActionResult> GetUserById(Guid? userID)
    {
        if (!userID.HasValue)
            return BadRequest("Invalid");

        var response = await usersService.GetUserByUserID(userID.Value);

        if (response is null)
            return BadRequest(response);

        return Ok(response);
    }
}