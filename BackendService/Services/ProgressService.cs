using EducationService.Models;
using EducationService.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace EducationService.Services;

public class ProgressService
{
    private readonly ProgressRepository progressRepository;

    public ProgressService(ProgressRepository progressRepository)
    {
        this.progressRepository = progressRepository;
    }


    public async Task<int?> SolveTask(int UserId, int TaskId)
    {
        return await progressRepository.SolveTask(UserId, TaskId);
    }
}
