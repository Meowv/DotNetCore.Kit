using System.Collections.Generic;

namespace Plus.Kit
{
    public interface IWeatherServices
    {
        IEnumerable<WeatherForecast> GetWeatherForecasts();
    }
}