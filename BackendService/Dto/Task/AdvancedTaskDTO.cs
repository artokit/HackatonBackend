using EducationService.Models;

namespace EducationService.Dto;

public class AdvancedTaskDTO
{
    public Level Level { get; set; }
    public Category Category { get; set; }
    public string RightAnswer { get; set; }
    public string Content { get; set; }
}
