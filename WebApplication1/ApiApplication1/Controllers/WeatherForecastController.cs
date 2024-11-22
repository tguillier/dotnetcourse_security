using ApiApplication1.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] _summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return GetNextFiveDaysWeather();
        }

        [HttpGet("{Date}", Name = "GetWeatherForecastByDate")]
        public IActionResult GetByDate([FromRoute]GetWeatherByDateQuery query)
        {
            var weathers = GetNextFiveDaysWeather();

            var dateWeather = weathers.FirstOrDefault(weather => weather.Date == query.Date);
            if (dateWeather == null)
            {
                return NotFound();
            }

            return Ok(dateWeather);
        }

        private static IEnumerable<WeatherForecast> GetNextFiveDaysWeather()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = _summaries[Random.Shared.Next(_summaries.Length)]
            });
        }
    }
}
