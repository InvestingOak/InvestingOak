using System;

namespace InvestingOak.Models.AlphaVantage
{
    public class GlobalQuote
    {
        public string Symbol { get; set; }

        public decimal Open { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Price { get; set; }

        public long Volume { get; set; }

        public DateTimeOffset LatestTradingDay { get; set; }

        public decimal PreviousClose { get; set; }

        public decimal Change { get; set; }

        public string ChangePercent { get; set; }
    }
}
