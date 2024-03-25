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
    private readonly ProgressService progressService;
    private readonly IWebHostEnvironment appEnviroment;

    public TaskController(TaskService taskService, IWebHostEnvironment appEnviroment,
        ProgressService progressService)
    {
        this.taskService = taskService;
        this.appEnviroment = appEnviroment;
        this.progressService = progressService;
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

    [HttpGet("solved")]
    [Authorize]
    public async Task<IActionResult> GetAllSolved()
    {
        var tasks = await progressService.GetAllSolve(UserId);
        if (tasks.IsNullOrEmpty())
        {
            return NotFound();
        }

        return Ok(tasks);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetInfo(int id)
    {
        var task = await taskService.GetAdvancedTask(id);
        if (task == null)
        {
            return BadRequest();
        }

        return Ok(task);
    }

    [HttpGet("random")]
    public async Task<IActionResult> GetRandom()
    {
        var task = await taskService.Random();
        if (task is null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    [HttpGet("random/{leveId}")]
    public async Task<IActionResult> GetRandomOnLevel(int leveId)
    {
        var task = await taskService.RandomOnLevel(leveId);
        if (task is null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    [HttpGet("files/{id}")]
    public async Task<IActionResult> GetZip(int id)
    {
        var path = await taskService.GetZip(id);
        path = appEnviroment.WebRootPath + path;
        var imageFileSystem = System.IO.File.OpenRead(path);
        return File(imageFileSystem, "application/zip");
    }
    
    [HttpPut]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateTask(UpdateTaskDTO task)
    {
        var updatedTask = await taskService.UpdateTask(task);
        if (updatedTask is null)
        {
            return NotFound();
        }
        return Ok(updatedTask);
    }

    [HttpPut("files/")]
    public async Task<IActionResult> AddZip(int id, IFormFile file)
    {
        var res = await taskService.AddZip(id, file);
        return (res is null) ? NotFound() : Ok(res);
    }
    
    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> AddTask(TaskDTO task)
    {
        return Ok(await taskService.AddTask(task));
    }
    
    [HttpDelete]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await taskService.DeleteTask(id);
        return (task is null) ? NotFound() : Ok(task);
    }
    //[Authorize]
    [HttpPut("solve/{id}")]
    public async Task<IActionResult> Solve(int id, string answer, int userId)
    {
        var rangResponse = await taskService.Solve(id, answer, userId);
        if (rangResponse is null)
        {
            return NotFound();
        }

        return Ok(rangResponse);
    }
}
