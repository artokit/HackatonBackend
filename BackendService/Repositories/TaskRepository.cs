﻿using Database;
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

    public async Task<List<TaskJoinDTO?>> GetAll()
    {
        var queryObject = new QueryObject(
            $"SELECT * FROM \"Tasks\" JOIN \"Levels\" ON \"Tasks\".\"LevelId\" = \"Levels\".\"Id\"");
        return await connection.ListOrEmpty<TaskJoinDTO?>(queryObject);
    }

    public async Task<TaskCase?> GetById(int id)
    {
        var queryObject = new QueryObject(
            $"SELECT * FROM \"Tasks\" WHERE \"Id\" = @id",
            new { id });
        return await connection.FirstOrDefault<TaskCase?>(queryObject);
    }

    public async Task<TaskCase?> GetByRandom()
    {
        var queryObject = new QueryObject(
            $"SELECT * FROM \"Tasks\" ORDER BY random()");
        return await connection.FirstOrDefault<TaskCase>(queryObject);
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

    public async Task<TaskCase> UpdateTask(UpdateTaskDTO task)
    {
        var queryObject = new QueryObject(
            "UPDATE \"Tasks\" SET \"LevelId\" = @LevelId, \"CategoryId\" = @CategoryId, \"RightAnswer\" = @RightAnswer, " +
            "\"Content\" = @Content WHERE \"Id\" = @id RETURNING \"LevelId\", \"CategoryId\", \"RightAnswer\", \"Content\"", 
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

    public async Task<TaskCase?> UpdateImage(int id, string path)
    {
        var queryObject = new QueryObject(
            "UPDATE \"Tasks\" SET \"PathFile\" = @zipPath where \"Id\" = @id RETURNING *", 
            new {zipPath = path, id});
        return await connection.FirstOrDefault<TaskCase>(queryObject);
    
    }

    public async Task<string?> GetZip(int id)
    {
        var queryObject = new QueryObject(
            $"SELECT \"PathFile\" FROM \"Tasks\" WHERE \"Id\" = @id",
            new { id });
        return await connection.FirstOrDefault<string>(queryObject);
    }
}
