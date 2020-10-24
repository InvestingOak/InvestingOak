using System.Text.Json.Serialization;

namespace InvestingOak.Models.AlphaVantage
{
    public class AlphaVantage
    {
        [JsonPropertyName("Information")]
        public string Information { get; set; }
    }
}
