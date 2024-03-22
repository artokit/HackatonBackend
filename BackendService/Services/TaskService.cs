using EducationService.Dto;
using EducationService.Models;
using EducationService.Repositories;

namespace EducationService.Services;

public class TaskService
{
    private readonly TaskRepository taskRepository;

    public TaskService(TaskRepository taskRepository)
    {
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
}
