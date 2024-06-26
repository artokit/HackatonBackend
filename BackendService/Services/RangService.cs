﻿using EducationService.Dto;
using EducationService.Models;
using EducationService.Repositories;

namespace EducationService.Services;

public class RangService
{
    private RangRepository rangRepository;
    private UserService userService;
    private IWebHostEnvironment appEnvironment;
    
    public RangService(RangRepository rangRepository, IWebHostEnvironment appEnvironment, UserService userService)
    {
        this.appEnvironment = appEnvironment;
        this.rangRepository = rangRepository;
        this.userService = userService;
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

    public async Task<string?> GetImage(int id)
    {
        return await rangRepository.GetImage(id);
    }
    
    public async Task<UserDTO?> GetUserWithRang(int id)
    {
        var user = await userService.GetById(id);
        if (user == null)
        {
            return null;
        }
        var rang = await GetRang(user.RangId);
        var u = new UserDTO
        {
            Username = user.Username,
            Email = user.Email,
            Photo = user.Photo,
            RatingScore = user.RatingScore,
            Rang = rang,
            Role = user.Role
        };
        return u;
    }
    
}
