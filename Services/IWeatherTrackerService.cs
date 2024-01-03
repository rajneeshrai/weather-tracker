using weather_tracker.Models;

namespace weather_tracker.Services
{
    public interface IWeatherTrackerService
    {
        Task<WeatherForecast?> GetCurrentWeatherAsync(double lat, double lon);
    }
}
