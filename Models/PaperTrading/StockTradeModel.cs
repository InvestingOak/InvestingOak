using System.ComponentModel.DataAnnotations;

namespace InvestingOak.Models.PaperTrading
{
    public class StockTradeModel : PositionModel
    {
        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal OpenPrice { get; set; }

        public decimal ClosePrice { get; set; }
    }
}
