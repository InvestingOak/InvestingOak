using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InvestingOak.Data.Entities.PaperTrading;

namespace InvestingOak.Models.PaperTrading
{
    public class OptionTradeModel : PositionModel
    {
        [Required]
        public List<Leg> Legs { get; set; }
    }
}
