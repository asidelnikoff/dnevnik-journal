using Dnevnik.Journal.Controllers.Dto;
using Dnevnik.Journal.Controllers.Dto.Requests;
using Dnevnik.Journal.Dal.Exceptions;
using Dnevnik.Journal.Dal.Extensions;
using Dnevnik.Journal.Dal.Models;

namespace Dnevnik.Journal.Dal;

public class JournalRepository(JournalDbContext dbContext) : IJournalRepository
{
    public async Task<IEnumerable<MarkDto>> AddMarks(CreateMarkRequest[] marks)
    {
        var range = marks.Select(ToUserMarks).ToList();
        await dbContext.UserMarks.AddRangeAsync(range);
        
        await dbContext.SaveChangesAsync();
        
        return range.Select(ToMarkResponse);
    }

    public async Task<IEnumerable<MarkDto>> UpdateMarks(MarkDto[] marks)
    {
        var result = new List<UserMarks>();
        foreach (var mark in marks)
        {
            var entity = await dbContext.UserMarks.FindAsync(mark.Id) ?? throw new MarkNotFoundException();
            entity.LessonId = mark.LessonId;
            entity.UserId = mark.UserId;
            entity.Mark = mark.Mark;
            entity.Comment = mark.Comment;
            entity.UpdatedAt = DateTime.UtcNow;
            
            result.Add(entity);
        }

        await dbContext.SaveChangesAsync();

        return result.Select(ToMarkResponse);
    }

    public async Task DeleteMarks(Guid[] markId)
    {
        foreach (var id in markId)
        {
            var entity = await dbContext.UserMarks.FindAsync(id) ?? throw new MarkNotFoundException();
            dbContext.UserMarks.Remove(entity);
        }

        await dbContext.SaveChangesAsync();
    }

    public List<MarkDto> GetMarks(FiltersRequest filtersRequest)
    {
        var marks = dbContext.UserMarks.AsQueryable()
            .FilterByUserId(filtersRequest.UserId)
            .FilterByLessonId(filtersRequest.LessonId)
            .FilterBySubject(filtersRequest.Subject)
            .FilterByDate(filtersRequest.From, filtersRequest.To);

        if (marks is null)
        {
            return [];
        }
        
        return marks
            .OrderByDescending(a => a.CreatedAt)
            .Select(ToMarkResponse)
            .ToList();
    }
    
    private UserMarks ToUserMarks(CreateMarkRequest mark) => new()
    {
        Id = Guid.NewGuid(),
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
        Comment = mark.Comment,
        LessonId = mark.LessonId,
        UserId = mark.UserId,
        Mark = mark.Mark
    };
    
    private MarkDto ToMarkResponse(UserMarks mark) => new()
    {
        Id = mark.Id,
        Comment = mark.Comment,
        UserId = mark.UserId,
        LessonId = mark.LessonId,
        Mark = mark.Mark,
        UpdatedAt = mark.UpdatedAt
    };
}