using Dnevnik.Journal.Dal;
using Dnevnik.Journal.Infrastructure.Configuration.Config;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Dnevnik.Journal.Infrastructure.Configuration;

public static class DbContextExtensions
{
    public static IServiceCollection AddJournalDbContext(this IServiceCollection services)
    {
        return services
            .AddDbContext<JournalDbContext>(
                (provider, options) =>
                {
                    var connections = provider
                        .GetRequiredService<IOptions<ConnectionStringsOptions>>()
                        .Value;
                    options
                        .UseNpgsql(
                            connections.JournalConnection,
                            npgsqlOptionsAction => npgsqlOptionsAction.EnableRetryOnFailure()
                        )
                        .UseSnakeCaseNamingConvention()
                        .EnableServiceProviderCaching()
                        .EnableThreadSafetyChecks();
                }
            );
    }
}