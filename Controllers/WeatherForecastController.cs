using Microsoft.AspNetCore.Mvc;
using weather_tracker.Models;
using weather_tracker.Services;

namespace weather_tracker.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IGeoCodingService _geoCodingService;
    private readonly IWeatherTrackerService _weatherTrackerService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IGeoCodingService geoCodingService, IWeatherTrackerService weatherTrackerService)
    {
        _logger = logger;
        this._geoCodingService = geoCodingService;
        this._weatherTrackerService = weatherTrackerService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<WeatherForecast?> Get(string cityName)
    {
        this._logger.LogInformation("Start method Get");
        WeatherForecast? weather = null;
        var geoLocation = await this._geoCodingService.GetCoordinatesByLocationNameAsync(cityName);
        var coordinates = geoLocation?.FirstOrDefault();
        if (coordinates != null)
        {
            weather = await _weatherTrackerService.GetCurrentWeatherAsync(coordinates.Lat, coordinates.Lon);
        }

        this._logger.LogInformation("End method Get");
        return weather;
    }
}
