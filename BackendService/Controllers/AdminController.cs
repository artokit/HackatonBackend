using EducationService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EducationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController: BaseController
{
    private readonly UserService userService;

    public AdminController(UserService userService)
    {
        this.userService = userService;
    }

    [HttpPut]
    public async Task<IActionResult> ChangeRole(string username)
    {
        var id = await userService.ChangeRole(username);
        if (id is null)
        {
            return BadRequest();
        }

        return Ok(id);
    }
}
