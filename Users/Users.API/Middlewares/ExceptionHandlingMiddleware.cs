namespace Users.API.Middlewares;

public class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            logger.LogError($"{ex.GetType()}: {ex.Message}");

            if (ex.InnerException is not null)
            {
                logger.LogError($"{ex.InnerException.GetType()}: {ex.InnerException.Message}");
            }
            httpContext.Response.StatusCode = 500;
            await httpContext.Response.WriteAsJsonAsync(
                new
                {
                    ex.Message,
                    Type = ex.GetType().ToString()
                }
            );
        }
    }
}

public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlingMiddleware(
        this IApplicationBuilder builder)
        => builder.UseMiddleware<ExceptionHandlingMiddleware>();
}