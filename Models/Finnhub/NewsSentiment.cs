using System.Text.Json.Serialization;

namespace InvestingOak.Models.Finnhub
{
    public class NewsSentiment
    {
        [JsonPropertyName("buzz")]
        public Buzz Buzz { get; set; }

        [JsonPropertyName("companyNewsScore")]
        public double CompanyNewsScore { get; set; }

        [JsonPropertyName("sectorAverageBullishPercent")]
        public double SectorAverageBullishPercent { get; set; }

        [JsonPropertyName("sectorAverageNewsScore")]
        public double SectorAverageNewsScore { get; set; }

        [JsonPropertyName("sentiment")]
        public Sentiment Sentiment { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
    }
}
