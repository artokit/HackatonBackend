using EducationService.Models;
using EducationService.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace EducationService.Services;

public class ProgressService
{
    private readonly ProgressRepository progressRepository;
    private readonly TaskService taskService;

    public ProgressService(ProgressRepository progressRepository, TaskService taskService)
    {
        this.progressRepository = progressRepository;
        this.taskService = taskService;
    }

    public async Task<List<TaskCase?>> GetAllSolve(int UserId)
    {
        var tasks = await progressRepository.GetAllSolved(UserId);
        if (tasks.IsNullOrEmpty())
        {
            return null;
        }

        List<TaskCase> taskCases = null;
        foreach (var taskId in tasks)
        {
            var task = await taskService.GetById(taskId);
            taskCases.Add(task);
        }
        
        return taskCases;
    }

    public async Task<int?> SolveTask(int UserId, int TaskId)
    {
        return await progressRepository.SolveTask(UserId, TaskId);
    }
}
