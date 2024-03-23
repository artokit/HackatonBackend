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

    public async Task<List<Portfolio?>> GetByUserId(int Userid)
    {
        var queryObject = new QueryObject(
            $"SELECT * FROM \"Portfolio\" WHERE \"UserId\" = @Userid",
            new { Userid });
        return await connection.ListOrEmpty<Portfolio?>(queryObject);
    }

    public async Task<Portfolio?> GetByAchieveUser(int achieve, int user)
    {
        var queryObject = new QueryObject(
            $"SELECT * FROM \"Portfolio\" WHERE \"AchievementId\" = @achieve AND \"UserId\" = @user",
            new { achieve, user });
        return await connection.FirstOrDefault<Portfolio>(queryObject);
    }

    public async Task<Portfolio?> Add(Portfolio portfolioDto)
    {
        var queryObject = new QueryObject(
            $"INSERT INTO \"Portfolio\" (\"AchievementId\", \"UserId\") VALUES(@achieve, @user) RETURNING *",
            new { achieve = portfolioDto.AchievementId, user = portfolioDto.UserId });
        return await connection.CommandWithResponse<Portfolio>(queryObject);
    }

    //TODO: хз чет тут исправить надо подумать
    public async Task<Portfolio?> Update(UpdatePortfolioDTO portfolio)
    {
        var queryObject = new QueryObject(
            $"UPDATE \"Portfolio\" SET \"AchievementId\" = @achieve, \"UserId\" = @user WHERE \"AchievementId\" = @AchievementId AND \"UserId\" = @UserId RETURNING *",
            new { achieve = portfolio.AchievementIdFind, user = portfolio.UserIdFind, AchievementId = portfolio.AchievementId, UserId = portfolio.UserId});
        return await connection.CommandWithResponse<Portfolio>(queryObject);
    }

    public async Task<Portfolio?> Delete(int achievement, int user)
    {
        var queryObject = new QueryObject(
            $"DELETE FROM \"Portfolio\" WHERE \"AchievementId\" = @achievement AND \"UserId\" = @user RETURNING *",
            new {achievement, user});
        return await connection.CommandWithResponse<Portfolio>(queryObject);
    }
}
