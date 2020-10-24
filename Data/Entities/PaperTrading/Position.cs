using System;
using System.ComponentModel.DataAnnotations;

namespace InvestingOak.Data.Entities.PaperTrading
{
    public abstract class Position
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Symbol { get; set; }

        [Required]
        public DateTimeOffset Open { get; set; }

        public DateTimeOffset Close { get; set; }

        public string Note { get; set; }
    }
}
