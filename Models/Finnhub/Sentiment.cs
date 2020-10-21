using System.Text.Json.Serialization;

namespace InvestingOak.Models.Finnhub
{
    public class Sentiment
    {
        [JsonPropertyName("bearishPercent")]
        public double BearishPercent { get; set; }

        [JsonPropertyName("bullishPercent")]
        public double BullishPercent { get; set; }
    }
}
