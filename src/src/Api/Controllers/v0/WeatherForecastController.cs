namespace Globo.TServiceNameT.Api.Controllers.v0
{
	using System;
	using System.Linq;

	using FluentResults;

	using Globo.TServiceNameT.Contracts;

	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;

	[ApiController]
	[Route("api/v0/[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching",
		};

		private readonly ILogger<WeatherForecastController> logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger)
		{
			this.logger = logger;
		}

		[HttpGet]
		[ProducesResponseType(typeof(WeatherForecast), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
		public IActionResult Get()
		{
			this.logger.LogInformation("Getting weather forecast");
			if (Random.Shared.Next(0, 2) == 0)
			{
				return BadRequest(Result.Fail("error"));
			}

			var response = Enumerable.Range(1, 5).Select(
					index => new WeatherForecast
					{
						Date = DateTime.Now.AddDays(index),
						TemperatureC = Random.Shared.Next(-20, 55),
						Summary = Summaries[Random.Shared.Next(Summaries.Length)],
					})
				.ToArray();

			return Ok(response);
		}
	}
}