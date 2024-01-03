using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using weather_tracker.Models;

namespace weather_tracker.Services
{
    public class GeoCodingService : IGeoCodingService
    {
        private readonly ILogger<GeoCodingService> _logger;
        private readonly AppSettings _appSettings;
        private readonly HttpClient _httpClient;

        public GeoCodingService(ILogger<GeoCodingService> logger, IOptions<AppSettings> options, HttpClient httpClient)
        {
            this._logger = logger;
            this._appSettings = options.Value;
            this._httpClient = httpClient;
        }

        public async Task GetGeoCodesAsync()
        {

        }

        public async Task<GeoLocation[]?> GetCoordinatesByLocationNameAsync(string locationName)
        {
            this._logger.LogInformation("Start method GetCoordinatesByLocationNameAsync");
            var url = this._appSettings?.GeoCodingApiUrl?
                            .Replace("{city-name}", locationName)
                            .Replace("{state-code}", this._appSettings.StateCode)
                            .Replace("{country-code}", this._appSettings.CountryCode)
                            .Replace("{limit}", "5")
                            .Replace("{api-key}", this._appSettings.ApiKey);

            var response = await _httpClient.GetAsync(url);
            var location = JsonConvert.DeserializeObject<GeoLocation[]>(await response.Content.ReadAsStringAsync());
            this._logger.LogInformation("End method GetCoordinatesByLocationNameAsync");
            return location;
        }
    }
}
