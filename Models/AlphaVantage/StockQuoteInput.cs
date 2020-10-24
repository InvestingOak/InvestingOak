using System.Text.Json.Serialization;

namespace InvestingOak.Models.AlphaVantage
{
    public class StockQuoteInput : AlphaVantage
    {
        [JsonPropertyName("Global Quote")]
        public GlobalQuoteInput GlobalQuote { get; set; }
    }
}
