using EducationService.Dto;
using EducationService.Models;
using EducationService.Repositories;

namespace EducationService.Services;

public class TaskService
{
    private readonly TaskRepository taskRepository;
    private readonly LevelService levelService;
    private readonly UserService userService;
    private readonly RangService rangService;

    public TaskService(LevelService levelService, UserService userService, RangService rangService, TaskRepository taskRepository)
    {
        this.levelService = levelService;
        this.userService = userService;
        this.rangService = rangService;
        this.taskRepository = taskRepository;
    }
    public async Task<List<TaskCase?>> GetAll()
    {
        return await taskRepository.GetAll();
    }

    public async Task<TaskCase?> GetById(int id)
    {
        return await taskRepository.GetById(id);
    }

    public async Task<TaskCase?> Random()
    {
        return await taskRepository.GetByRandom();
    }

    public async Task<TaskCase?> AddTask(TaskDTO task)
    {
        return await taskRepository.AddTask(task);
    }

    public async Task<TaskCase?> UpdateTask(UpdateTaskDTO task)
    {
        var currentTask = await GetById(task.Id);
        if (currentTask is null)
        {
            return null;
        }

        var t = new TaskCase
        {
            Id=task.Id,
            LevelId = task.LevelId ?? currentTask.LevelId,
            CategoryId = task.CategoryId ?? currentTask.CategoryId,
            RightAnswer = task.RightAnswer ?? currentTask.RightAnswer,
            Content = task.Content ?? currentTask.Content
        };
        return await taskRepository.UpdateTask(t);
    }

    public async Task<TaskCase?> DeleteTask(int id)
    {
        return await taskRepository.DeleteTask(id);
    }

    public async Task<int?> Solve(int id, string answer, int userId)
    {
        var task = await taskRepository.GetById(id);
        if (task == null || answer != task.RightAnswer)
        {
            return null;
        }
        return await SolveAward(task, userId);

    }

    private async Task<int?> SolveAward(TaskCase task, int userId)
    {
        var user = await userService.GetById(userId);
        var level = await levelService.GetLevel(task.LevelId);
        if (user == null || level == null)
        {
            return null;
        }
        var rang = await rangService.GetRang(user.RangId);
        if (rang == null)
        {
            return null;
        }
        var rating= user.RatingScore + level.Award;
        var ratingScore = await userService.PutRatingScore(rating, user.Id);
        userService.PutRang(rang, user.RangId);
        return ratingScore;

    }
}
