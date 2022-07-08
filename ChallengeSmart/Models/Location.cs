using System.Text.Json.Serialization;

namespace ChallengeSmart.Model
{
    public class Location
    {
        [JsonPropertyName("lon")]
        public double? Lon { get; set; }
        [JsonPropertyName("lat")]
        public double? Lat { get; set; }
    }
}
