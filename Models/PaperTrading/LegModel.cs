using System;
using System.ComponentModel.DataAnnotations;

namespace InvestingOak.Models.PaperTrading
{
    public class LegModel
    {
        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTimeOffset Expiration { get; set; }

        [Required]
        public decimal Strike { get; set; }

        [Required]
        public OptionType Type { get; set; }

        [Required]
        public decimal OpenPrice { get; set; }

        public decimal ClosePrice { get; set; }
    }
}
