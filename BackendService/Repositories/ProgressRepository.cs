using Database;
using Database.Interfaces;
using EducationService.Models;

namespace EducationService.Repositories;

public class ProgressRepository
{
    private readonly IConnection connection;

    public ProgressRepository(IConnection connection)
    {
        this.connection = connection;
    }

    public async Task<List<int>> GetAllSolved(int UserId)
    {
        var queryObject = new QueryObject(
            $"SELECT \"TaskId\" FROM \"Progress\"",
            new { UserId, notSolve = true});
        return await connection.ListOrEmpty<int>(queryObject);
    }

    public async Task<int?> SolveTask(int UserId, int TaskId)
    {
        var queryObject = new QueryObject(
            $"INSERT INTO \"Progress\" SET(\"UserId\", \"TaskId\") VALUES(@UserId, @TaskId)",
            new { UserId, TaskId });
        return await connection.CommandWithResponse<int>(queryObject);
    }
    
}
