using System.ComponentModel.DataAnnotations;

namespace InvestingOak.Data.Entities
{
    public class StockSymbol
    {
        [Key]
        public string Symbol { get; set; }

        public string DisplaySymbol { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public string Currency { get; set; }
    }
}
