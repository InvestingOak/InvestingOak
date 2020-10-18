using System.Text.Json.Serialization;

namespace InvestingOak.Models
{
    public class StockQuoteInput : AlphaVantage
    {
        [JsonPropertyName("Global Quote")]
        public GlobalQuoteInput GlobalQuote { get; set; }
    }
}
