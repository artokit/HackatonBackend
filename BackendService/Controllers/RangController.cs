using EducationService.Dto;
using EducationService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EducationService.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class RangController : BaseController
{
    private RangService rangService;
    private readonly IWebHostEnvironment appEnvironment;
    
    public RangController(RangService rangService, IWebHostEnvironment appEnvironment)
    {
        this.rangService = rangService;
        this.appEnvironment = appEnvironment;
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
    
    [HttpGet("image/{id}")]
    public async Task<IActionResult> GetImage(int id)
    {
        var path = await rangService.GetImage(id);
        path = appEnvironment.WebRootPath + path;
        var imageFileStream = System.IO.File.OpenRead(path);
        return File(imageFileStream, "image/jpg");
    }
}
