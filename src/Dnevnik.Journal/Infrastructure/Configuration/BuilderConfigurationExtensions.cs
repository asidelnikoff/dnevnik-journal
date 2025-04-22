using System.Text.Json;
using System.Text.Json.Serialization;

using Dnevnik.Journal.Dal;

using Microsoft.AspNetCore.HttpLogging;

namespace Dnevnik.Journal.Infrastructure.Configuration;

public static class BuilderConfigurationExtensions
{
    public static void Configure(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions();
        builder.Services.RegisterOptions(builder.Configuration);
        builder.Services.AddTransient<IJournalRepository, JournalRepository>();
        builder.Services.AddVersioningApi(builder.Configuration);
        builder.Services.AddJournalDbContext();
        
        builder.Services.AddHttpLogging(o =>
        {
            o.CombineLogs = false;
            o.LoggingFields = HttpLoggingFields.All;
        });
        
        // Add services to the container.
        builder.Services.AddResponseCompression();
        builder.Services.AddRouting();
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
                options.JsonSerializerOptions.Converters.Add(
                    new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower));
            });;
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }
}