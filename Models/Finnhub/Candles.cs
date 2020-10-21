using System.Text.Json.Serialization;

namespace InvestingOak.Models.Finnhub
{
    public class Candles
    {
        [JsonPropertyName("c")]
        public decimal[] Close { get; set; }

        [JsonPropertyName("h")]
        public decimal[] High { get; set; }

        [JsonPropertyName("l")]
        public decimal[] Low { get; set; }

        [JsonPropertyName("o")]
        public decimal[] Open { get; set; }

        [JsonPropertyName("s")]
        public string Status { get; set; }

        [JsonPropertyName("t")]
        public ulong[] TimeStamp { get; set; }

        [JsonPropertyName("v")]
        public ulong[] Volume { get; set; }
    }
}
