using EducationService.Dto;
using EducationService.Models;
using EducationService.Repositories;

namespace EducationService.Services;

public class PortfolioService
{
    private readonly PortfolioRepository portfolioRepository;

    public PortfolioService(PortfolioRepository portfolioRepository)
    {
        this.portfolioRepository = portfolioRepository;
    }
    public async Task<List<Portfolio?>> GetAll()
    {
        return await portfolioRepository.GetAll();
    }

    public async Task<List<Portfolio?>> GetByUserId(int id)
    {
        return await portfolioRepository.GetByUserId(id);
    }

    public async Task<Portfolio?> GetByAchieveUser(int achieve, int user)
    {
        return await portfolioRepository.GetByAchieveUser(achieve, user);
    }

    public async Task<Portfolio?> Add(Portfolio portfolioDto)
    {
        return await portfolioRepository.Add(portfolioDto);
    }

    public async Task<Portfolio?> Update(UpdatePortfolioDTO portfolioDto)
    {
        var currentPortfolio = await portfolioRepository.GetByAchieveUser(portfolioDto.AchievementIdFind, portfolioDto.UserIdFind);
        if (currentPortfolio is null)
        {
            return null;
        }

        var p = new UpdatePortfolioDTO
        {
            AchievementIdFind = currentPortfolio.AchievementId,
            UserIdFind = currentPortfolio.UserId,
            AchievementId = portfolioDto.AchievementId ?? currentPortfolio.AchievementId,
            UserId = portfolioDto.UserId ?? currentPortfolio.UserId
        };
        return await portfolioRepository.Update(p);
    }

    public async Task<Portfolio?> Delete(int achieve, int user)
    {
        return await portfolioRepository.Delete(achieve, user);
    }
}
