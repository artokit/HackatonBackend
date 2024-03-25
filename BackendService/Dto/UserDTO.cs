using EducationService.Models;

namespace EducationService.Dto;

public class UserDTO
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Photo { get; set; }
    public int RatingScore { get; set; }
    public string Role { get; set; }
    public Rang Rang { get; set; }
}
