using System.Net;

namespace Dnevnik.Journal.Infrastructure.Exceptions;

public class BusinessException(string message, string? details = null) : Exception(message)
{
    public virtual HttpStatusCode StatusCode { get; init; } = HttpStatusCode.InternalServerError;
    public string? ErrorText { get; init; }
    public string? Details => details;
}