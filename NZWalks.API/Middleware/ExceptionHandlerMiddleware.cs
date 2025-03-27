using System.Text.Json;

namespace NZWalks.API.Middleware
{
	public class ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
	{
		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);
				context.Response.StatusCode = StatusCodes.Status500InternalServerError;
				context.Response.ContentType = "application/json";
				var response = new { message = "An error occurred. Please try again later." };
				var json = JsonSerializer.Serialize(response);
				await context.Response.WriteAsync(json);
			}
		}
	}
}
