using System.Text.Json.Serialization;

namespace InvestingOak.Models.Finnhub
{
    public class Buzz
    {
        [JsonPropertyName("articlesInLastWeek")]
        public uint ArticlesInLastWeek { get; set; }

        [JsonPropertyName("buzz")]
        public double Value { get; set; }

        [JsonPropertyName("weeklyAverage")]
        public double WeeklyAverage { get; set; }
    }
}
