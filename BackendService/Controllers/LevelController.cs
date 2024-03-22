using EducationService.Dto;
using EducationService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EducationService.Controllers;

[Route("api/[controller]")]
public class LevelController : BaseController
{
    private readonly LevelRepository LevelRepository;
    
    public LevelController(LevelRepository LevelRepository)
    {
        this.LevelRepository = LevelRepository;
    }

    // [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetLevel(int id)
    {
        var level = await LevelRepository.GetLevel(id);
        
        return (level is null) ? NotFound() : Ok(level);
    }

    // [Authorize]
    [HttpGet("")]
    public async Task<ActionResult> GetAllCategories()
    {
        return Ok(await LevelRepository.GetListLevel());
    }

    // [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLevel(int id)
    {
        var c = await LevelRepository.DeleteLevel(id);
        return (c is null) ? NotFound() : Ok(c);
    }
    
    // [Authorize]
    [HttpPost("")]
    public async Task<ActionResult> AddLevel(AddLevelDto levelDto)
    {
        var c = await LevelRepository.AddLevel(levelDto.Name, levelDto.Award);
        return Ok(c);
    }
}
