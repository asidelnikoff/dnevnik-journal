namespace Dnevnik.Journal.Controllers.Dto;

public class MarkDto
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required Guid LessonId { get; set; }
    public required string Mark { get; set; }
    public string? Comment { get; set; }

    public DateTime UpdatedAt { get; set; }
}