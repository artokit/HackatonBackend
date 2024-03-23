using EducationService.Dto;
using EducationService.Models;
using EducationService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EducationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AchievementController: BaseController
{
    private readonly IWebHostEnvironment appEnviroment;
    private readonly AchievementService achievementService;

    public AchievementController(IWebHostEnvironment appEnviroment, AchievementService achievementService)
    {
        this.appEnviroment = appEnviroment;
        this.achievementService = achievementService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var achievements = await achievementService.GetAll();
        if (achievements.IsNullOrEmpty())
        {
            return NotFound();
        }
        return Ok(achievements);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var achievement = await achievementService.GetById(id);
        if (achievement is null)
        {
            return NotFound();
        }

        return Ok(achievement);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AchievementDTO achievementDto)
    {
        var achievementExist = await achievementService.GetByName(achievementDto.Name);
        if (achievementExist != null)
        {
            return BadRequest();
        }

        return Ok(achievementService.Add(achievementDto));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateAchievementDTO updateAchievementDto)
    {
        var achievement = await achievementService.Update(updateAchievementDto);
        if (achievement is null)
        {
            return NotFound();
        }

        return Ok(achievement);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var achievement = await achievementService.Delete(id);
        if (achievement is null)
        {
            return NotFound();
        }

        return Ok(achievement);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetImage(int id)
    {
        var path = await achievementService.GetPath(id);
        path = appEnviroment.WebRootPath + path;
        var imageFileStream = System.IO.File.OpenRead(path);
        return File(imageFileStream, "image/jpg");
    }
    [HttpPut]
    public async Task<IActionResult> AddImage(IFormFile uploadedFile, int id)
    {
        if (uploadedFile != null)
        {
            var path = "/Achievement/" + uploadedFile.FileName;
            using (var fileStream = new FileStream(appEnviroment.WebRootPath + path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }

            return (await achievementService.GetById(id) == null)
                ? NotFound()
                : Ok(await achievementService.Update(new UpdateAchievementDTO
                {
                    Id = id,
                    Photo = path
                }));
        }

        return BadRequest();
    }
}
