namespace EducationService.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Photo { get; set; }
    public int RatingScore { get; set; }
    public int RangId { get; set; }
}
