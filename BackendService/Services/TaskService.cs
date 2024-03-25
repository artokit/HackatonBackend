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
    private readonly CategoryRepository categoryRepository;
    private readonly ProgressService progressService;
    private readonly IWebHostEnvironment appEnvironment;
    public TaskService(LevelService levelService, UserService userService, 
        RangService rangService, TaskRepository taskRepository, CategoryRepository categoryRepository,
        ProgressService progressService, IWebHostEnvironment appEnvironment)
    {
        this.levelService = levelService;
        this.userService = userService;
        this.rangService = rangService;
        this.taskRepository = taskRepository;
        this.categoryRepository = categoryRepository;
        this.appEnvironment = appEnvironment;
        this.progressService = progressService;
    }
    public async Task<List<TaskCase?>> GetAll()
    {
        return await taskRepository.GetAll();
    }

    public async Task<TaskCase?> GetById(int id)
    {
        return await taskRepository.GetById(id);
    }

    public async Task<AdvancedTaskDTO?> GetAdvancedTask(int id)
    {
        var task = await GetById(id);
        if (task == null)
        {
            return null;
        }

        var level = await levelService.GetLevel(task.LevelId);
        var category = await categoryRepository.GetCategory(task.CategoryId);
        return new AdvancedTaskDTO
        {
            Level = level, Category = category, RightAnswer = task.RightAnswer, Content = task.Content
        };
    }
    
    public async Task<TaskCase?> Random()
    {
        return await taskRepository.GetByRandom();
    }

    public async Task<TaskCase?> RandomOnLevel(int levelId)
    {
        return await taskRepository.GetByRandomLevel(levelId);
    }

    public async Task<TaskCase?> AddTask(TaskDTO task)
    {
        return await taskRepository.AddTask(task);
    }

    public async Task<string?> GetZip(int id)
    {
        return await taskRepository.GetZip(id);
    }
    public async Task<TaskCase?> AddZip(int id, IFormFile uploadedFile)
    {
        var path = "/Tasks/" + uploadedFile.FileName; 
        using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
        {
            await uploadedFile.CopyToAsync(fileStream);
        }

        return await taskRepository.UpdateImage(id, path);
    }

    public async Task<TaskCase?> UpdateTask(UpdateTaskDTO task)
    {
        var currentTask = await GetById(task.Id);
        if (currentTask is null)
        {
            return null;
        }

        var t = new UpdateTaskDTO
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

    public async Task<RangResponseDTO?> Solve(int id, string answer, int userId)
    {
        var task = await GetAdvancedTask(id);
        if (task == null || answer != task.RightAnswer)
        {
            return null;
        }

        await progressService.SolveTask(userId, id);
        return await SolveAward(task, userId);

    }

    private async Task<RangResponseDTO?> SolveAward(AdvancedTaskDTO task, int userId)
    {
        var user = await userService.GetById(userId);
        var level = task.Level;
        if (user == null)
        {
            return null;
        }
        var rang = await rangService.GetRang(user.RangId);
        
        if (rang == null)
        {
            return null;
        }
        var ratingScore = await userService.PutRatingScore(user.RatingScore + level.Award, user.Id);
        var userRang = await userService.PutRang(ratingScore ?? 0, rang, user.Id);
        return new RangResponseDTO
        {
            RatingScore = ratingScore ?? 0,
            UserRang = userRang
        };
    }

    public async Task<RangResponseDTO?> SolvePunish(TaskCase task, int userId)
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
    var rating= user.RatingScore - level.Award;
    var ratingScore = await userService.PutRatingScore(rating, user.Id);
    var userRang = await userService.PutRang(ratingScore ?? 0, rang, user.Id);
    return new RangResponseDTO
    {
        RatingScore = ratingScore ?? 0,
        UserRang = userRang
    };
    }

    public async Task<List<TaskCase>> GetAllByLevelId(int levelId, int categoryId)
    {
        return await taskRepository.GetAllByLevelId(levelId, categoryId);
    }
}
