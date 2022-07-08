using ChallengeSmart.DTOS;
using ChallengeSmart.Model;

namespace ChallengeSmart.Interfaces
{
    public interface IAirPortService
    {
        Task<AirPort> GetAirPortAsync(string iata);
        Task<CalculateDistanceResponseDTO> CalculateDistanceAsync(CalculateDistanceRequestDTO calculateDistanceRequest);
    }
}
