using EducationService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EducationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RatingConroller: BaseController
{
    private readonly RatingService ratingService;

    public RatingConroller(RatingService ratingService)
    {
        this.ratingService = ratingService;
    }
    [HttpGet("top10")]
    public async Task<IActionResult> GetTop10()
    {
        var users = await ratingService.GetRating();
        if (users.IsNullOrEmpty())
        {
            return NotFound();
        }

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRank()
    {
        var index = await ratingService.GetRank(UserId);
        if (index != -1)
        {
            return Ok(index);
        }

        return BadRequest();
    }
}
