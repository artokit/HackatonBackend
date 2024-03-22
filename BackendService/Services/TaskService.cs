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
    
    //TODO: Артем запили пж UpdateTask

    public async Task<TaskCase?> DeleteTask(int id)
    {
        return await taskRepository.DeleteTask(id);
    }
}
