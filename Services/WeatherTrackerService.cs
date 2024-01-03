using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using weather_tracker.Models;

namespace weather_tracker.Services
{
    public class WeatherTrackerService : IWeatherTrackerService
    {
        private readonly ILogger<WeatherTrackerService> _logger;
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;

        public WeatherTrackerService(ILogger<WeatherTrackerService> logger, IOptions<AppSettings> options, HttpClient httpClient)
        {
            this._logger = logger;
            this._httpClient = httpClient;
            this._appSettings = options.Value;
        }

        public async Task<WeatherForecast?> GetCurrentWeatherAsync(double lat, double lon)
        {
            this._logger.LogInformation("Start method GetCurrentWeatherAsync");
            var url = this._appSettings?.WeatherTrackerApiUrl?
                            .Replace("{lat}", lat.ToString())
                            .Replace("{lon}", lon.ToString())
                            .Replace("{api-key}", this._appSettings.ApiKey);
            var response = await this._httpClient.GetAsync(url);
            var weather = JsonConvert.DeserializeObject<WeatherForecast?>(await response.Content.ReadAsStringAsync());
            this._logger.LogInformation("End method GetCurrentWeatherAsync");
            return weather;
        }
    }
}
