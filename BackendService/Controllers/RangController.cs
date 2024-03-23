using EducationService.Dto;
using EducationService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EducationService.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class RangController : BaseController
{
    private RangService rangService;
    
    public RangController(RangService rangService)
    {
        this.rangService = rangService;
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateImage(int id, IFormFile uploadedFile)
    {
        var res = await rangService.AddImage(id, uploadedFile);
        return (res is null) ? NotFound() : Ok(res);
    }

    [HttpPost("")]
    public async Task<IActionResult> AddNewRang(AddRangRequestDto rang)
    {
        return Ok(await rangService.AddRang(rang));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRang(int id)
    {
        var res = await rangService.GetRang(id);
        return (res is null) ? NotFound() : Ok(res);
    }
}
