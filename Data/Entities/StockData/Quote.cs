using System.ComponentModel.DataAnnotations;

namespace InvestingOak.Data.Entities.StockData
{
    public class Quote : Staleable
    {
        [Key]
        public string Symbol { get; set; }

        public decimal Close { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Open { get; set; }

        public ulong Volume { get; set; }

        public decimal PreviousClose { get; set; }
    }
}
