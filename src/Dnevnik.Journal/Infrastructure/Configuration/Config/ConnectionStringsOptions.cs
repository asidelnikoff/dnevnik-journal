namespace Dnevnik.Journal.Infrastructure.Configuration.Config;

public class ConnectionStringsOptions
{
    public const string ConnectionStrings = "ConnectionStrings";
    public required string JournalConnection { get; init; }
}

