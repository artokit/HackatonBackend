using EducationService.Models;
using EducationService.Repositories;

namespace EducationService.Services;

public class LevelService
{
    private readonly LevelRepository levelRepository;

    public LevelService(LevelRepository levelRepository)
    {
        this.levelRepository = levelRepository;
    }

    public async Task<Level?> GetLevel(int id)
    {
        return await levelRepository.GetLevel(id);
    }

    public async Task<List<Level>> GetListLevel()
    {
        return await levelRepository.GetListLevel();
    }

    public async Task<Level?> DeleteLevel(int id)
    {
        return await levelRepository.DeleteLevel(id);
    }

    public async Task<Level> AddLevel(string name, int award)
    {
        return await levelRepository.AddLevel(name, award);
    }
    
}
