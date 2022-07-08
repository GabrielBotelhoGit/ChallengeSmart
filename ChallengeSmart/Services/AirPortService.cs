using ChallengeSmart.Configs;
using ChallengeSmart.DTOS;
using ChallengeSmart.Exceptions;
using ChallengeSmart.Interfaces;
using ChallengeSmart.Model;
using Geolocation;
using System.Text.Json;

namespace ChallengeSmart.Services
{
    public class AirPortService: IAirPortService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AirPortConfig _airPortConfig;
        public AirPortService(IHttpClientFactory httpClientFactory,
            AirPortConfig airPortConfig)
        {
            _httpClientFactory = httpClientFactory;
            _airPortConfig = airPortConfig;
        }

        public async Task<AirPort> GetAirPortAsync(string iata)
        {
            AirPort airport = new();

            HttpClient client = _httpClientFactory.CreateClient(nameof(AirPortService));
            string url = _airPortConfig.AirportGetUrl + "/" + iata;
            var responseGet = await client.GetAsync(url);
            string responseContent = await responseGet.Content.ReadAsStringAsync();
            if (responseGet.IsSuccessStatusCode && responseContent.IndexOf("errors") == -1)
            {
                airport = JsonSerializer.Deserialize<AirPort>(responseContent);                
            }
            else
            {
                throw new AirPortException(responseContent);
            }
            return airport;
        }

        public async Task<CalculateDistanceResponseDTO> CalculateDistanceAsync(CalculateDistanceRequestDTO calculateDistanceRequest)
        {
            CalculateDistanceResponseDTO calculateDistanceResponse = new ();

            AirPort originAirPort = await GetAirPortAsync(calculateDistanceRequest.originIata);
            AirPort destinationAirPort = await GetAirPortAsync(calculateDistanceRequest.destinationIata);

            Coordinate originCoordinate = new(Convert.ToDouble(originAirPort?.Location.Lat), Convert.ToDouble(originAirPort?.Location.Lon));
            Coordinate destinationCoordinate = new(Convert.ToDouble(destinationAirPort?.Location.Lat), Convert.ToDouble(destinationAirPort?.Location.Lon));
            calculateDistanceResponse.Distance = GeoCalculator.GetDistance(originCoordinate, destinationCoordinate);

            return calculateDistanceResponse;
        }
    }
}
