using EducationService.Dto;
using EducationService.Models;
using EducationService.Repositories;

namespace EducationService.Services;

public class PortfolioService
{
    private readonly PortfolioRepository portfolioRepository;

    public PortfolioService(PortfolioRepository portfolioRepository)
    {
        this.portfolioRepository = this.portfolioRepository;
    }
    public async Task<List<Portfolio?>> GetAll()
    {
        return await portfolioRepository.GetAll();
    }

    public async Task<Portfolio?> GetById(int id)
    {
        return await portfolioRepository.GetById(id);
    }

    public async Task<Portfolio?> Add(PortfolioDTO portfolioDto)
    {
        return await portfolioRepository.Add(portfolioDto);
    }

    public async Task<Portfolio?> Update(UpdatePortfolioDTO portfolioDto)
    {
        var currentPortfolio = await portfolioRepository.GetById(portfolioDto.Id);
        if (currentPortfolio is null)
        {
            return null;
        }

        var p = new Portfolio
        {
            Id = portfolioDto.Id,
            AchievementId = portfolioDto.AchievementId ?? currentPortfolio.AchievementId,
            UserId = portfolioDto.UserId ?? currentPortfolio.UserId
        };
        return await portfolioRepository.Update(p);
    }

    public async Task<Portfolio?> Delete(int id)
    {
        return await portfolioRepository.Delete(id);
    }
}
