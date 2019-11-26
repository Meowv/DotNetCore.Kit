using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Plus.Kit.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IWeatherServices _weatherServices;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherServices weatherServices)
        {
            _logger = logger;
            _weatherServices = weatherServices;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _weatherServices.GetWeatherForecasts();
        }
    }
}