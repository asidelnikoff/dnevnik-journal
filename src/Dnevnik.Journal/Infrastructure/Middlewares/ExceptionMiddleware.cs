using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

using Dnevnik.Journal.Infrastructure.Exceptions;

namespace Dnevnik.Journal.Infrastructure.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    private static readonly JsonSerializerOptions s_jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower) }
    };

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured {ExceptionMessage}", ex.Message);

            var businessException = ConvertToBusinessException(ex);
            await HandleBusinessExceptionAsync(httpContext, businessException);
        }
    }
    
    private BusinessException ConvertToBusinessException(Exception ex) => ex switch
    {
        BusinessException exception => exception,
        _ => new BusinessException(ex.Message, ex.ToString())
    };

    private static Task HandleBusinessExceptionAsync(HttpContext context, BusinessException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)exception.StatusCode;

        return context.Response.WriteAsync(ResponseFromException(exception));
    }

    private static string ResponseFromException(BusinessException exception)
    {
        return JsonSerializer.Serialize(
            new
            {
                ErrorDescription = exception.Message,
                exception.Details,
                exception.ErrorText
            },
            s_jsonSerializerOptions
        );
    }
}

public static class ExceptionMiddlewareExtensions
{
    /// <summary>
    /// Использовать глобальный обработчик ошибок API
    /// </summary>
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    }
}