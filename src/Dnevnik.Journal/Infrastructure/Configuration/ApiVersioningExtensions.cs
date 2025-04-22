using Asp.Versioning;

using Dnevnik.Journal.Infrastructure.Configuration.Config;

namespace Dnevnik.Journal.Infrastructure.Configuration;

public static class ApiVersioningExtensions
{
    public static IServiceCollection AddVersioningApi(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddApiVersioning(options =>
            {
                var defaultVersion = configuration.GetSection("ApiLastVersion").Get<ApiLastVersionOptions>()
                                     ?? new ApiLastVersionOptions();
                // Версия по умолчанию
                options.DefaultApiVersion = new ApiVersion(defaultVersion.Major, defaultVersion.Minor);
                // Используется, когда клиент не предоставил явную версию
                options.AssumeDefaultVersionWhenUnspecified = true;
                // Сообщает о поддерживаемых версиях API в заголовке ответа
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        return services;
    }
}