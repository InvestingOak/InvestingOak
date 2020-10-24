using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestingOak.Data.Entities.PaperTrading
{
    public class StockTrade : Position
    {
        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(38, 3)")]
        public decimal OpenPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(38, 3)")]
        public decimal ClosePrice { get; set; }
    }
}
