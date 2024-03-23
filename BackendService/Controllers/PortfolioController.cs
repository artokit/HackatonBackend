using Database.Interfaces;
using EducationService.Dto;
using EducationService.Models;
using EducationService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EducationService.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PortfolioController: BaseController
{
    private readonly PortfolioService portfolioService;

    public PortfolioController(PortfolioService portfolioService)
    {
        this.portfolioService = portfolioService;
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var portfolios = await portfolioService.GetAll();
        if (portfolios.IsNullOrEmpty())
        {
            return NotFound();
        }

        return Ok(portfolios);
    }
    
    [HttpGet("user/{id}")]
    public async Task<IActionResult> GetByUserId(int id)
    {
        var portfolio = await portfolioService.GetByUserId(id);
        if (portfolio.IsNullOrEmpty())
        {
            return NotFound();
        }

        return Ok(portfolio);
    }
    
    [HttpGet("{achieve}")]
    public async Task<IActionResult> GetByAchieveUser(int achieve)
    {
        var portfolio = await portfolioService.GetByAchieveUser(achieve, UserId);
        if (portfolio is null)
        {
            return NotFound();
        }

        return Ok(portfolio);
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> Add(Portfolio portfolio)
    {
        return Ok(await portfolioService.Add(portfolio));
    }

    [Authorize(Roles = "admin")]
    [HttpPut]
    public async Task<IActionResult> Update(UpdatePortfolioDTO updatePortfolioDto)
    {
        var portfolio = await portfolioService.Update(updatePortfolioDto);
        if (portfolio is null)
        {
            return NotFound();
        }

        return Ok(portfolio);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{achieve}/{id}")]
    public async Task<IActionResult> Delete(int achieve, int id)
    {
        var portfolio = await portfolioService.Delete(achieve, id);
        if (portfolio is null)
        {
            return NotFound();
        }

        return Ok(portfolio);
    }
    
}   
