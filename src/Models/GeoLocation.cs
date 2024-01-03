using Newtonsoft.Json;

namespace weather_tracker.Models
{
    public class GeoLocation
    {
        public string? Name { get; set; }
        [JsonProperty("local_names")]
        public LocalNames? LocalNames { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
    }
}
