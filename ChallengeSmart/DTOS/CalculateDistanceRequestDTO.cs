using ChallengeSmart.Model;

namespace ChallengeSmart.DTOS
{
    public class CalculateDistanceRequestDTO
    {
        public string originIata { get; set; }
        public string destinationIata { get; set; }
    }
}
