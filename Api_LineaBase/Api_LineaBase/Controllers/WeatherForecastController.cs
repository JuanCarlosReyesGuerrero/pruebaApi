using Commun.Logger;
using Microsoft.AspNetCore.Mvc;

namespace Api_LineaBase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ICreateLogger _createLogger;


        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICreateLogger createLogger)
        {
            _logger = logger;
            _createLogger = createLogger;

        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _createLogger.LogWriteExcepcion("dgksphgdkfhkfp`skhpodjk`ph`pgdh");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}