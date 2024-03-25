using Database.Interfaces;
using System.Web;
using EducationService.Dto;
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
    private readonly RangService rangService;
    private readonly IWebHostEnvironment appEnviroment;
    public UserController(UserService userService, RangService rangService, IWebHostEnvironment appEnviroment)
    {
        this.userService = userService;
        this.rangService = rangService;
        this.appEnviroment = appEnviroment;
    }
    [HttpGet("my")]
    public async Task<IActionResult> GetInfo()
    {
        var userExist = await rangService.GetUserWithRang(UserId);
        if (userExist == null)
        {
            return BadRequest();
        }

        return Ok(userExist);
    }
    
    [AllowAnonymous]
    [HttpGet("image")]
    public async Task<IActionResult> GetImage(int id)
    {
        var path = await userService.GetPath(id);
        if (path is null)
        {
            path = "/Avatars/" + "default.jpg";
        }
        path = appEnviroment.WebRootPath + path;
        var imageFileStream = System.IO.File.OpenRead(path);
        return File(imageFileStream, "image/jpg");
    }
    
    [HttpPut("image")]
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

    [HttpPut]
    public async Task<IActionResult> Update(AuthUpdateDTO authUpdateDto)
    {
        var user = await userService.Update(UserId, authUpdateDto);
        return (user is null) ? NotFound() : Ok(user);
    }
}
