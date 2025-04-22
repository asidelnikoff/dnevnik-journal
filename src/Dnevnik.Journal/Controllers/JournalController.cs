using Dnevnik.Journal.Controllers.Dto;
using Dnevnik.Journal.Controllers.Dto.Requests;
using Dnevnik.Journal.Dal;

using Microsoft.AspNetCore.Mvc;

namespace Dnevnik.Journal.Controllers;

public class JournalController(IJournalRepository journalRepository) : BaseController
{
    [HttpGet("marks")]
    public List<MarkDto> GetMarks([FromQuery] FiltersRequest filtersRequest)
    {
        return journalRepository.GetMarks(filtersRequest);
    }

    [HttpPost("marks")]
    public async Task<List<MarkDto>> PostMarks(CreateMarkRequest[] marks)
    {
        return (await journalRepository.AddMarks(marks)).ToList();
    }
    
    [HttpPut("marks")]
    public async Task<List<MarkDto>> PutMarks(MarkDto[] marks)
    {
        return (await journalRepository.UpdateMarks(marks)).ToList();
    }
    
    [HttpDelete("marks")]
    public async Task DeleteMarks([FromBody] MarkDto[] marks)
    {
        await journalRepository.DeleteMarks(marks.Select(a => a.Id).ToArray());
    }
}