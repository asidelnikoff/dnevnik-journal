using Dnevnik.Journal.Dal.Models;

namespace Dnevnik.Journal.Dal.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<UserMarks>? FilterByUserId(this IQueryable<UserMarks>? marks, Guid? userId) =>
        userId is null ? marks : marks?.Where(a => a.UserId == userId);
    
    public static IQueryable<UserMarks>? FilterByLessonId(this IQueryable<UserMarks>? marks, Guid? lessonId) =>
        lessonId is null ? marks : marks?.Where(a => a.LessonId == lessonId);
    
    public static IQueryable<UserMarks>? FilterBySubject(this IQueryable<UserMarks>? marks, string? subject) =>
        subject is null ? marks : marks?.Where(a => a.Subject == subject);
    
    public static IQueryable<UserMarks>? FilterByDate(this IQueryable<UserMarks>? source, DateTime? dateFrom, DateTime? dateTo)
    {
        source = dateFrom is null ? source : source?.Where(a => a.CreatedAt >= dateFrom);
        source = dateTo is null ? source : source?.Where(l => l.CreatedAt <= dateTo);

        return source;
    }
}