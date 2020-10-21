using System;
using System.Text.Json.Serialization;

namespace InvestingOak.Models.Finnhub
{
    public class CompanyProfile2
    {
        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("exchange")]
        public string Exchange { get; set; }

        [JsonPropertyName("ipo")]
        public DateTimeOffset Ipo { get; set; }

        [JsonPropertyName("marketCapitalization")]
        public long MarketCapitalization { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("shareOutstanding")]
        public decimal SharesOutstanding { get; set; }

        [JsonPropertyName("ticker")]
        public string Ticker { get; set; }

        [JsonPropertyName("weburl")]
        public string WebUrl { get; set; }

        [JsonPropertyName("logo")]
        public string LogoUrl { get; set; }

        [JsonPropertyName("finnhubIndustry")]
        public string Industry { get; set; }
    }
}
