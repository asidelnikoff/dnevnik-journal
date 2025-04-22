using Dnevnik.Journal.Infrastructure.Configuration.Config;

namespace Dnevnik.Journal.Infrastructure.Configuration;

public static class OptionsExtensions
{
    public static IServiceCollection RegisterOptions(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .Configure<ConnectionStringsOptions>(configuration.GetRequiredSection(ConnectionStringsOptions.ConnectionStrings));
    }
}