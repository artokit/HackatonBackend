using Database;
using Database.Interfaces;
using EducationService.Dto;
using EducationService.Models;

namespace EducationService.Repositories;


public class RangRepository
{
    private IConnection connection;

    public RangRepository(IConnection connection)
    {
        this.connection = connection;
    }

    public async Task<Rang?> GetRang(int id)
    {
        var queryObject = new QueryObject("SELECT * FROM \"Rangs\" WHERE \"Id\" = @id", new { id });
        return await connection.FirstOrDefault<Rang>(queryObject);
    }
    
    public async Task<Rang> AddRang(AddRangRequestDto rang)
    {
        var queryObject = new QueryObject(
            "INSERT INTO \"Rangs\"(\"Name\") VALUES(@name) RETURNING *",
            new {rang.Name});
        return await connection.CommandWithResponse<Rang>(queryObject);
    }

    public async Task<Rang?> UpdateImage(int id, string imagePath)
    {
        var queryObject = new QueryObject(
            "UPDATE \"Rangs\" SET \"ImagePath\" = @imagePath where \"Id\" = @id RETURNING *", new {imagePath, id});
        return await connection.FirstOrDefault<Rang>(queryObject);
    }

    public async Task<string?> GetImage(int id)
    {
        var queryObject = new QueryObject(
            $"SELECT \"ImagePath\" FROM \"Rangs\" WHERE \"Id\" = @id",
            new { id });
        return await connection.FirstOrDefault<string>(queryObject);
    }
}
