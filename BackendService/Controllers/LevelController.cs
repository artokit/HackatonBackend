using EducationService.Dto;
using EducationService.Repositories;
using EducationService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationService.Controllers;

[Route("api/[controller]")]
public class LevelController : BaseController
{
    private readonly LevelService levelService;

    public LevelController(LevelService levelService)
    {
        this.levelService = levelService;
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetLevel(int id)
    {
        var level = await levelService.GetLevel(id);

        return (level is null) ? NotFound() : Ok(level);
    }

    [Authorize]
    [HttpGet("")]
    public async Task<ActionResult> GetAllCategories()
    {
        return Ok(await levelService.GetListLevel());
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLevel(int id)
    {
        var c = await levelService.DeleteLevel(id);
        return (c is null) ? NotFound() : Ok(c);
    }

    [Authorize(Roles = "admin")]
    [HttpPost("")]
    public async Task<ActionResult> AddLevel(AddLevelDto levelDto)
    {
        var c = await levelService.AddLevel(levelDto.Name, levelDto.Award);
        return Ok(c);
    }

}
