namespace Dnevnik.Journal.Dal.Models;

public class UserMarks
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid LessonId { get; set; }
    public string? Subject { get; set; }
    public required string Mark { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}