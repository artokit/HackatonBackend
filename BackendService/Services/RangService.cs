using EducationService.Dto;
using EducationService.Models;
using EducationService.Repositories;

namespace EducationService.Services;

public class RangService
{
    private RangRepository rangRepository;
    private IWebHostEnvironment appEnvironment;
    
    public RangService(RangRepository rangRepository, IWebHostEnvironment appEnvironment)
    {
        this.appEnvironment = appEnvironment;
        this.rangRepository = rangRepository;
    }

    public async Task<Rang?> GetRang(int id)
    {
        return await rangRepository.GetRang(id);
    }
    
    public async Task<Rang> AddRang(AddRangRequestDto rang)
    {
        return await rangRepository.AddRang(rang);
    }

    public async Task<Rang?> AddImage(int id, IFormFile uploadedFile)
    {
        var path = "/Rang/" + uploadedFile.FileName; 
        using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
        {
            await uploadedFile.CopyToAsync(fileStream);
        }

        return await rangRepository.UpdateImage(id, path);
    }
}
