using Dnevnik.Journal.Controllers.Dto;
using Dnevnik.Journal.Controllers.Dto.Requests;

namespace Dnevnik.Journal.Dal;

public interface IJournalRepository
{
    Task<IEnumerable<MarkDto>> AddMarks(CreateMarkRequest[] marks);
    Task<IEnumerable<MarkDto>> UpdateMarks(MarkDto[] marks);
    Task DeleteMarks(Guid[] markIds);
    List<MarkDto> GetMarks(FiltersRequest filtersRequest);
}