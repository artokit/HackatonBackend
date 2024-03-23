using Database.Interfaces;
using EducationService.Dto;
using EducationService.Models;
using EducationService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EducationService.Controllers;

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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var portfolio = await portfolioService.GetById(id);
        if (portfolio is null)
        {
            return NotFound();
        }

        return Ok(portfolio);
    }

    [HttpPost]
    public async Task<IActionResult> Add(PortfolioDTO portfolioDto)
    {
        return Ok(portfolioService.Add(portfolioDto));
    }

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

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var portfolio = await portfolioService.Delete(id);
        if (portfolio is null)
        {
            return NotFound();
        }

        return Ok(portfolio);
    }
    
}   
