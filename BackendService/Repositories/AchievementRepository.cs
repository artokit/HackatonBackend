using Database;
using Database.Interfaces;
using EducationService.Dto;
using EducationService.Models;

namespace EducationService.Repositories;

public class AchievementRepository
{
    private readonly IConnection connection;

    public AchievementRepository(IConnection connection)
    {
        this.connection = connection;
    }

    public async Task<List<Achievement?>> GetAll()
    {
        var queryObject = new QueryObject(
            $"SELECT * FROM \"Achievements\"");
        return await connection.ListOrEmpty<Achievement>(queryObject);
    }

    public async Task<Achievement?> GetById(int id)
    {
        var queryObject = new QueryObject(
            $"SELECT * FROM \"Achievement\" WHERE \"Id\" = @id",
            new { id });
        return await connection.FirstOrDefault<Achievement>(queryObject);
    }

    public async Task<Achievement?> GetByName(string name)
    {
        var queryObject = new QueryObject(
            $"SELECT * FROM \"Achievement\" WHERE \"Name\" = @name RETURNING *",
            new { name });
        return await connection.CommandWithResponse<Achievement>(queryObject);
    }

    public async Task<Achievement?> Add(AchievementDTO achievement)
    {
        var queryObject = new QueryObject(
            $"INSERT INTO \"Achievement\" (\"Photo\", \"Name\", \"Description\") VALUES(@photo, @name, @description) RETURNING *",
            new {photo = achievement.Photo, name = achievement.Name, description = achievement.Description});
        return await connection.CommandWithResponse<Achievement?>(queryObject);

    }
    
    public async Task<Achievement?> Update(Achievement achievement)
    {
        var queryObject = new QueryObject(
            $"UPDATE \"Achievement\" SET \"Photo\" = @photo, \"Name\" = @name, \"Description\" = @description WHERE \"Id\" = @id RETURNING *",
            new { photo = achievement.Photo, name = achievement.Name, description = achievement.Description, id = achievement.Id });
        return await connection.CommandWithResponse<Achievement>(queryObject);
    }

    public async Task<Achievement?> Delete(int id)
    {
        var queryObject = new QueryObject(
            $"DELETE FROM \"Achievement\" WHERE \"Id\" = @id RETURNING *",
            new { id });
        return await connection.CommandWithResponse<Achievement>(queryObject);
    }

    public async Task<string?> GetPath(int id)
    {
        var queryObject = new QueryObject(
            $"SELECT \"Photo\" FROM \"Achievements\" WHERE \"Id\" = @id",
            new { id });
        return await connection.FirstOrDefault<string>(queryObject);
    }
}
