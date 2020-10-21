using System.Text.Json.Serialization;

namespace InvestingOak.Models.Finnhub
{
    public class StockSymbol
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("displaySymbol")]
        public string DisplaySymbol { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }
}
