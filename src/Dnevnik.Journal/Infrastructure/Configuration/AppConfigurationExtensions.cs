using Dnevnik.Journal.Dal;
using Dnevnik.Journal.Infrastructure.Configuration.Config;
using Dnevnik.Journal.Infrastructure.Middlewares;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Dnevnik.Journal.Infrastructure.Configuration;

public static class AppConfigurationExtensions
{
    public static void Configure(this WebApplication app, IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateOnBuild = true
        });
        MigrateJournal(serviceProvider);

        app.UseExceptionMiddleware();
        app.UseHttpLogging();
        
        app.UseSwagger();
        app.UseSwaggerUI();
        
        app.MapControllers();
    }
    
    private static void MigrateJournal(ServiceProvider serviceProvider)
    {
        var connectionOptions = serviceProvider
            .GetRequiredService<IOptions<ConnectionStringsOptions>>()
            .Value;

        using var context = new JournalDbContext(
            new DbContextOptionsBuilder<JournalDbContext>()
                .UseNpgsql(
                    connectionOptions.JournalConnection,
                    b => b.MigrationsHistoryTable("__EFMigrationsHistory")
                )
                .UseSnakeCaseNamingConvention()
                .Options
        );
        context.Database.Migrate();
    }
}