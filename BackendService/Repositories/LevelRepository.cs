using Database;
using Database.Interfaces;
using EducationService.Models;
namespace EducationService.Repositories;

public class LevelRepository
{
    private IConnection connection;

    public LevelRepository(IConnection connection)
    {
        this.connection = connection;
    }
    
    public async Task<Level?> GetLevel(int id)
    {
        var queryObject = new QueryObject("SELECT * FROM \"Levels\" where \"Id\" = @id", new {id});
        return await connection.FirstOrDefault<Level>(queryObject);
    }

    public async Task<List<Level>> GetListLevel()
    {
        var queryObject = new QueryObject("SELECT * FROM \"Levels\"", new {});
        return await connection.ListOrEmpty<Level>(queryObject);
    }

    public async Task<Level?> DeleteLevel(int id)
    {
        var queryObject = new QueryObject(
            "DELETE FROM \"Levels\" where \"Id\" = @id RETURNING *", 
            new { id });
        return await connection.FirstOrDefault<Level>(queryObject);
    }

    public async Task<Level> AddLevel(string name, int award) 
    {
        var queryObject = new QueryObject(
            "INSERT INTO \"Levels\"(\"Name\", \"Award\") VALUES(@name, @award) RETURNING *",
            new {name, award});
        return await connection.CommandWithResponse<Level>(queryObject);
    }
}

