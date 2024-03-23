using EducationService.Dto;
using EducationService.Models;
using EducationService.Repositories;

namespace EducationService.Services;

public class AchievementService
{
    private readonly AchievementRepository achievementRepository;

    public AchievementService(AchievementRepository achievementRepository)
    {
        this.achievementRepository = achievementRepository;
    }

    public async Task<List<Achievement?>> GetAll()
    {
        return await achievementRepository.GetAll();
    }

    public async Task<Achievement?> GetById(int id)
    {
        return await achievementRepository.GetById(id);
    }

    public async Task<Achievement?> GetByName(string name)
    {
        return await achievementRepository.GetByName(name);
    }

    public async Task<Achievement?> Add(AchievementDTO achievementDto)
    {
        return await achievementRepository.Add(achievementDto);
    }

    public async Task<Achievement?> Update(UpdateAchievementDTO achievement)
    {
        var currentAchievement = await achievementRepository.GetById(achievement.Id);
        if (currentAchievement is null)
        {
            return null;
        }

        var a = new Achievement
        {
            Id = achievement.Id,
            Photo = achievement.Photo ?? currentAchievement.Photo,
            Name = achievement.Name ?? currentAchievement.Name,
            Description = achievement.Description ?? currentAchievement.Description
        };
        return await achievementRepository.Update(a);
    }

    public async Task<Achievement?> Delete(int id)
    {
        return await achievementRepository.Delete(id);
    }

    public async Task<string?> GetPath(int id)
    {
        return await achievementRepository.GetPath(id);
    }
}
