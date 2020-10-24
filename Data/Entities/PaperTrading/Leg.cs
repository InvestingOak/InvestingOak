using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestingOak.Data.Entities.PaperTrading
{
    public class Leg
    {
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTimeOffset Expiration { get; set; }

        [Required]
        [Column(TypeName = "decimal(38, 1)")]
        public decimal Strike { get; set; }

        [Required]
        public OptionType Type { get; set; }

        [Required]
        [Column(TypeName = "decimal(38, 3)")]
        public decimal OpenPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(38, 3)")]
        public decimal ClosePrice { get; set; }
    }
}
