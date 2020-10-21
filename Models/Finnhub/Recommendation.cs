using System;
using System.Text.Json.Serialization;

namespace InvestingOak.Models.Finnhub
{
    public class Recommendation
    {
        [JsonPropertyName("buy")]
        public uint Buy { get; set; }

        [JsonPropertyName("hold")]
        public uint Hold { get; set; }

        [JsonPropertyName("period")]
        public DateTimeOffset Period { get; set; }

        [JsonPropertyName("sell")]
        public uint Sell { get; set; }

        [JsonPropertyName("strongBuy")]
        public uint StrongBuy { get; set; }

        [JsonPropertyName("strongSell")]
        public uint StrongSell { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
    }
}
