using Database;
using Database.Interfaces;
using EducationService.Models;
namespace EducationService.Repositories;

public class CategoryRepository
{
    private IConnection connection;

    public CategoryRepository(IConnection connection)
    {
        this.connection = connection;
    }
    
    public async Task<Category?> GetCategory(int id)
    {
        var queryObject = new QueryObject("SELECT * FROM \"Categories\" where \"Id\" = @id", new {id});
        return await connection.FirstOrDefault<Category>(queryObject);
    }

    public async Task<List<Category>> GetListCategory()
    {
        var queryObject = new QueryObject("SELECT * FROM \"Categories\"", new {});
        return await connection.ListOrEmpty<Category>(queryObject);
    }
}
