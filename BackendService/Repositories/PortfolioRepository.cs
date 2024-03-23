using Database;
using Database.Interfaces;
using EducationService.Dto;
using EducationService.Models;

namespace EducationService.Repositories;

public class PortfolioRepository
{
    private readonly IConnection connection;

    public PortfolioRepository(IConnection connection)
    {
        this.connection = connection;
    }
    
    public async Task<List<Portfolio?>> GetAll()
    {
        var queryObject = new QueryObject(
            $"SELECT * FROM \"Portfolio\"");
        return await connection.ListOrEmpty<Portfolio>(queryObject);
    }

    public async Task<Portfolio?> GetById(int id)
    {
        var queryObject = new QueryObject(
            $"SELECT * FROM \"Portfolio\" WHERE \"Id\" = @id",
            new { id });
        return await connection.FirstOrDefault<Portfolio>(queryObject);
    }

    public async Task<Portfolio?> Add(PortfolioDTO portfolioDto)
    {
        var queryObject = new QueryObject(
            $"INSERT INTO \"Portfolio\" (\"AchievementId\", \"UserId\") VALUES(@achieve, @user) RETURNING *",
            new { achieve = portfolioDto.AchievementId, user = portfolioDto.UserId });
        return await connection.CommandWithResponse<Portfolio>(queryObject);
    }

    public async Task<Portfolio?> Update(Portfolio portfolio)
    {
        var queryObject = new QueryObject(
            $"UPDATE \"Portfolio\" SET \"AchievementId\" = @achieve, \"UserId\" = @user WHERE \"Id\" = @id RETURNING *",
            new { achieve = portfolio.AchievementId, user = portfolio.UserId, id = portfolio.Id });
        return await connection.CommandWithResponse<Portfolio>(queryObject);
    }

    public async Task<Portfolio?> Delete(int id)
    {
        var queryObject = new QueryObject(
            $"DELETE FROM \"Portfolio\" WHERE \"Id\" = @id RETURNING *",
            new { id });
        return await connection.CommandWithResponse<Portfolio>(queryObject);
    }
}
