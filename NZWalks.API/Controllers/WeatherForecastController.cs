using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
	[ApiController]
	// GET e.g https://localhost:portnumber/api/v1/weatherforecast
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiVersion("1.0")]
	[ApiVersion("2.0")]
	public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
	{
		private static readonly string[] Summaries =
		[
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		];

		private readonly ILogger<WeatherForecastController> _logger = logger;

		[MapToApiVersion("1.0")]
		[HttpGet(Name = "GetWeatherForecast")]
		// NOTE: V1 reports temperature in Celsius
		public IEnumerable<WeatherForecastV1> GetV1()
		{
			return Enumerable.Range(1, 5).Select(index => new WeatherForecastV1
			{
				Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}

		[MapToApiVersion("2.0")]
		[HttpGet(Name = "GetWeatherForecast")]
		// NOTE: V2 reports temperature in both Celsius and Fahrenheit
		public IEnumerable<WeatherForecastV2> GetV2()
		{
			return Enumerable.Range(1, 5).Select(index => new WeatherForecastV2
			{
				Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}
	}
}
