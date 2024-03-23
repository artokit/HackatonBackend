using EducationService.Dto;
using EducationService.Repositories;
using EducationService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationService.Controllers;

[Route("api/[controller]")]
public class LevelController : BaseController
{
    private readonly LevelRepository LevelRepository;
    private readonly UserService userService;

    public LevelController(LevelRepository LevelRepository, UserService userService)
    {
        this.LevelRepository = LevelRepository;
        this.userService = userService;
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetLevel(int id)
    {
        var level = await LevelRepository.GetLevel(id);

        return (level is null) ? NotFound() : Ok(level);
    }

    [Authorize]
    [HttpGet("")]
    public async Task<ActionResult> GetAllCategories()
    {
        return Ok(await LevelRepository.GetListLevel());
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLevel(int id)
    {
        var c = await LevelRepository.DeleteLevel(id);
        return (c is null) ? NotFound() : Ok(c);
    }

    [Authorize(Roles = "admin")]
    [HttpPost("")]
    public async Task<ActionResult> AddLevel(AddLevelDto levelDto)
    {
        var c = await LevelRepository.AddLevel(levelDto.Name, levelDto.Award);
        return Ok(c);
    }

    [Authorize]
    [HttpPut("solve/{id}")]
    public async Task<IActionResult> SolveAward(int id)
    {
        var user = await userService.GetById(UserId);
        var level = await LevelRepository.GetLevel(id);
        if (user == null || level == null)
        {
            return NotFound();
        }

        var rating= user.RatingScore + level.Award;
        return Ok(await userService.PutRatingScore(rating, UserId));

    }

}
