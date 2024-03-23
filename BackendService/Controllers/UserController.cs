using Database.Interfaces;
using System.Web;
using EducationService.Models;
using EducationService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationService.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController: BaseController
{
    private readonly UserService userService;
    private readonly IWebHostEnvironment appEnviroment;
    public UserController(UserService userService, IWebHostEnvironment appEnviroment)
    {
        this.userService = userService;
        this.appEnviroment = appEnviroment;
    }
    [HttpGet("my")]
    public async Task<IActionResult> GetInfo()
    {
        var userExist = await userService.GetById(UserId);
        if (userExist == null)
        {
            return BadRequest();
        }

        return Ok(userExist);
    }
    
    [AllowAnonymous]
    [HttpGet("image/{id]")]
    public async Task<IActionResult> GetImage(int id)
    {
        var path = await userService.GetPath(id);
        path = appEnviroment.WebRootPath + path;
        var imageFileStream = System.IO.File.OpenRead(path);
        return File(imageFileStream, "image/jpg");
    }
    
    [HttpPut]
    public async Task<IActionResult> AddAvatar(IFormFile uploadedFile)
    {
        if (uploadedFile != null)
        {
            var path = "/Avatars/" + uploadedFile.FileName;
            using (var fileStream = new FileStream(appEnviroment.WebRootPath + path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }

            return (await userService.GetById(UserId) == null)
                ? NotFound()
                : Ok(await userService.PutPath(path, UserId));
        }

        return BadRequest();
    }
}
