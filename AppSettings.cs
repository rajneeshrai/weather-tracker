namespace weather_tracker
{
    public class AppSettings
    {
        public string? ApiKey { get; set; }
        public string? StateCode { get; set; }
        public string? CountryCode { get; set; }
        public string? GeoCodingApiUrl { get; set; }
        public string? WeatherTrackerApiUrl { get; set; }
        public string? AirPollutionApiUrl { get; set; }
    }
}
