using Database;
using Database.Interfaces;
using EducationService.Dto;
using EducationService.Models;

namespace EducationService.Repositories;

public class UserRepository 
{
    private IConnection connection;

    public UserRepository(IConnection connection)
    {
        this.connection = connection;
    }

    public async Task<List<RankingUserDTO?>> GetTop10()
    {
        var queryObject = new QueryObject(
            $"SELECT \"Id\", \"Username\", \"RatingScore\" FROM USERS ORDER BY \"RatingScore\" DESC");
        return await connection.ListOrEmpty<RankingUserDTO?>(queryObject);
    }

    public async Task<List<RankingUserDTO?>> GetRating()
    {
        var queryObject = new QueryObject(
            @"SELECT  ""Id"", ""Username"", ""RatingScore"" FROM USERS ORDER BY ""RatingScore"" DESC");
        return await connection.ListOrEmpty<RankingUserDTO?>(queryObject);
    }
    public async Task<User?> AddUser(RegisterDto user)
    {
        var queryObject = new QueryObject(
            $"INSERT INTO USERS(\"Username\", \"Password\", \"Email\") VALUES(@username, @password, @email) RETURNING *",
            new { username = user.Username, password = user.Password, email = user.Email, });
        return await connection.CommandWithResponse<User>(queryObject);
    }

    public async Task<User?> GetByUsername(string username)
    {
        var queryObject = new QueryObject(
            "SELECT * FROM USERS WHERE \"Username\" = @username",
            new { username });
        return await connection.FirstOrDefault<User>(queryObject);
    }

    public async Task<User?> GetById(int id)
    {
        var queryObject = new QueryObject(
            "SELECT * FROM USERS WHERE \"Id\" = @id",
            new { id });
        return await connection.FirstOrDefault<User>(queryObject);
    }


    public async Task<User?> GetByEmail(string email)
    {
        var queryObject = new QueryObject(
            "SELECT * FROM USERS WHERE \"Email\" = @email",
            new {email});
        return await connection.FirstOrDefault<User>(queryObject);
    }

    public async Task<AuthUpdateDTO?> Update(int id,AuthUpdateDTO authUpdateDto)
    {
        var queryObject = new QueryObject(
            $"UPDATE users SET \"Username\"=@username, \"Email\"=@email WHERE \"Id\" = @id RETURNING \"Username\", \"Email\"",
            new { id, username = authUpdateDto.Username, email = authUpdateDto.Email });
        return await connection.CommandWithResponse<AuthUpdateDTO>(queryObject);
    }

    public async Task<string?> PutPath(string path, int id)
    {
        var queryObject = new QueryObject(
            $"UPDATE USERS SET \"Photo\" = @path WHERE \"Id\" = @id RETURNING \"Photo\" ",
            new { path, id });
        return await connection.CommandWithResponse<string>(queryObject);
    }
    public async Task<int?> PutRatingScore(int rating, int id)
        {
            var queryObject = new QueryObject(
                $"UPDATE USERS SET \"RatingScore\" = @rating WHERE \"Id\" = @id RETURNING \"RatingScore\" ",
                new { rating, id });
            return await connection.CommandWithResponse<int>(queryObject);
        }

    public async Task<int> PutRang(int ratingScore, Rang rang, int id)
    {
        var rangChange = rang.Id;
        if (ratingScore > rang.MaxScore && rangChange < 5)
        {
            rangChange += 1;
        }

        if (ratingScore < rang.MinScore && rangChange > 1)
        {
            rangChange -= 1;
        }
        var queryObject = new QueryObject(
            $"UPDATE USERS SET \"RangId\"= @rangChange WHERE \"Id\" = @id RETURNING \"RangId\" ",
            new { rangChange, id });
        return await connection.CommandWithResponse<int>(queryObject);
    }

    public async Task<string?> GetPath(int id)
    {
        var queryObject = new QueryObject(
            $"SELECT \"Photo\" FROM users WHERE \"Id\" = @id",
            new { id });
        return await connection.FirstOrDefault<string>(queryObject);
    }
    
}
