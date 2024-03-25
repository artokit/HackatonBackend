namespace EducationService.Models;

public class Progress
{
    public int UserId { get; set; }
    public int TaskId { get; set; }
    public bool IsSolve { get; set; }
}
