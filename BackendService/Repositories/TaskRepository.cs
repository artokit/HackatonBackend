using Database;
using Database.Interfaces;
using EducationService.Dto;
using EducationService.Models;

namespace EducationService.Repositories;

public class TaskRepository
{
    private readonly IConnection connection;

    public TaskRepository(IConnection connection)
    {
        this.connection = connection;
    }

    public async Task<List<TaskCase?>> GetAll()
    {
        var queryObject = new QueryObject(
            $"SELECT * FROM \"Tasks\"");
        return await connection.ListOrEmpty<TaskCase?>(queryObject);
    }

    public async Task<TaskCase?> GetById(int id)
    {
        var queryObject = new QueryObject(
            $"SELECT * FROM \"Tasks\" WHERE \"Id\" = @id",
            new { id });
        return await connection.FirstOrDefault<TaskCase?>(queryObject);
    }

    public async Task<TaskCase?> AddTask(TaskDTO task)
    {
        var queryObject = new QueryObject(
            $"INSERT INTO \"Tasks\" (\"LevelId\", \"CategoryId\", \"RightAnswer\", \"Content\") VALUES( @levelId, @categoryId, @rightAnswer, @content) RETURNING *",
            new
            {
                levelId = task.LevelId,
                categoryId = task.CategoryId,
                rightAnswer = task.RightAnswer,
                content = task.Content
            });
        return await connection.CommandWithResponse<TaskCase>(queryObject);
    }

    public async Task<TaskCase> UpdateTask(TaskCase task)
    {
        var queryObject = new QueryObject(
            "UPDATE \"Tasks\" SET \"LevelId\"=@LevelId, \"CategoryId\"=@CategoryId, \"RightAnswer\"=@RightAnswer, \"Content\"=@Content WHERE \"Id\" = @id RETURNING *", 
            new {task.LevelId, task.CategoryId, task.RightAnswer, task.Content, task.Id});
        return await connection.CommandWithResponse<TaskCase>(queryObject);
    }

    public async Task<TaskCase?> DeleteTask(int id)
    {
        var queryObject = new QueryObject(
            $"DELETE FROM \"Tasks\" WHERE \"Id\" = @id RETURNING *",
            new { id });
        return await connection.FirstOrDefault<TaskCase?>(queryObject);
    }
    
}
