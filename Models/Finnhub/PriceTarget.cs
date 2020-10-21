using System;
using System.Text.Json.Serialization;

namespace InvestingOak.Models.Finnhub
{
    public class PriceTarget
    {
        [JsonPropertyName("lastUpdated")]
        public DateTimeOffset LastUpdated { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("targetHigh")]
        public decimal TargetHigh { get; set; }

        [JsonPropertyName("targetLow")]
        public decimal TargetLow { get; set; }

        [JsonPropertyName("targetMean")]
        public decimal TargetMean { get; set; }

        [JsonPropertyName("targetMedian")]
        public decimal TargetMedian { get; set; }
    }
}
