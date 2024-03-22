using EducationService.Dto;
using EducationService.Models;
using EducationService.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace EducationService.Services;

public class RatingService
{
    private readonly UserRepository userRepository;

    public RatingService(UserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<List<RankingUserDTO?>> GetTop10()
    {
        return await userRepository.GetTop10();
    }

    public async Task<List<RankingUserDTO?>> GetRating()
    {
        return await userRepository.GetRating();
    }

    public async Task<int?> GetRank(int id)
    {
        var users = await userRepository.GetRating();
        var index = -1;
        if (!users.IsNullOrEmpty())
        {
            for (var i = 0; i < users.Count; i++)
            {
                if (users[i].Id == id)
                {
                    index = i + 1;
                }
            }
        }
        return index;
    }
}
