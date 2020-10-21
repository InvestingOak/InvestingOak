using System.Text.Json.Serialization;

namespace InvestingOak.Models.Finnhub
{
    public class NewsArticle
    {
        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("datetime")]
        public uint DateTime { get; set; }

        [JsonPropertyName("headline")]
        public string Headline { get; set; }

        [JsonPropertyName("id")]
        public uint Id { get; set; }

        [JsonPropertyName("image")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("related")]
        public string Related { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
