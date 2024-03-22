using EducationService.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationService.Controllers;

[Route("[controller]")]
public class CategoryController : BaseController
{
    private readonly CategoryRepository categoryRepository;
    
    public CategoryController(CategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository;
    }

    // [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetCategory(int id)
    {
        var category = await categoryRepository.GetCategory(id);
        
        return (category is null) ? NotFound() : Ok(category);
    }

    // [Authorize]
    [HttpGet("")]
    public async Task<ActionResult> GetAllCategories()
    {
        return Ok(await categoryRepository.GetListCategory());
    }

    // [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        var c = await categoryRepository.DeleteCategory(id);
        return (c is null) ? NotFound() : Ok(c);
    }
    
    // [Authorize]
    [HttpPost("")]
    public async Task<ActionResult> AddCategory(string name)
    {
        var c = await categoryRepository.AddCategory(name);
        return Ok(c);
    }
}
