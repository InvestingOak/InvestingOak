using System;
using System.ComponentModel.DataAnnotations;

namespace InvestingOak.Models.PaperTrading
{
    public abstract class PositionModel
    {
        [Required]
        public string Symbol { get; set; }

        [Required]
        public DateTimeOffset Open { get; set; }

        public DateTimeOffset Close { get; set; }

        public string Note { get; set; }
    }
}
