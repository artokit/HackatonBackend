﻿namespace EducationService.Dto;

public class UpdateTaskDTO
{
    public int Id { get; set; }
    public int? LevelId { get; set; }
    public int? CategoryId { get; set; }
    public string? RightAnswer { get; set; }
    public string? Content { get; set; }
}
