using weather_tracker.Models;

namespace weather_tracker.Services
{
    public interface IGeoCodingService
    {
        Task GetGeoCodesAsync();
        Task<GeoLocation[]?> GetCoordinatesByLocationNameAsync(string locationName);
    }
}
