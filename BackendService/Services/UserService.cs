using Common.Enums;
using EducationService.Dto;
using EducationService.Models;
using EducationService.Repositories;

namespace EducationService.Services;

public class UserService
{
    private readonly UserRepository userRepository;

    public UserService(UserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    
    public async Task<User?> AddUser(RegisterDto user)
    {
        user.Password = Common.Common.PasswordHash(user.Password);
        return await userRepository.AddUser(user);
    }

    public async Task<User?> GetById(int id)
    {
        return await userRepository.GetById(id);
    }
    public async Task<User?> GetByUsername(string username)
    {
        return await userRepository.GetByUsername(username);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await userRepository.GetByEmail(email);
    }
    
    public async Task<ValidationUserStatus> CheckUserExist(RegisterDto user)
    {
        if (await GetByUsername(user.Username) != null)
        {
            return ValidationUserStatus.UsernameIsTaken;
        }

        if (await GetByEmail(user.Email) != null)
        {
            return ValidationUserStatus.EmailIsTaken;
        }

        return ValidationUserStatus.Success;
    }

    public async Task<string?> PutPath(string path, int id)
    {
        return await userRepository.PutPath(path, id);
    }
}
