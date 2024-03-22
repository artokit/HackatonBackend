using EducationService.Dto;
using EducationService.Models;
using EducationService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EducationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController: BaseController
{
    private readonly TaskService taskService;

    public TaskController(TaskService taskService)
    {
        this.taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await taskService.GetAll();
        if (tasks.IsNullOrEmpty())
        {
            return BadRequest();
        }

        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await taskService.GetById(id);
        if (task == null)
        {
            return BadRequest();
        }

        return Ok(task);
    }

    [HttpPut("update")]
    // [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateTask(UpdateTaskDTO task)
    {
        var updatedTask = await taskService.UpdateTask(task);
        if (updatedTask is null)
        {
            return NotFound();
        }
        return Ok(updatedTask);
    }
    
    [HttpPost("")]
    public async Task<IActionResult> AddTask(TaskDTO task)
    {
        return Ok(await taskService.AddTask(task));
    }
    
    [HttpDelete("")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await taskService.DeleteTask(id);
        return (task is null) ? NotFound() : Ok(task);
    }
}
