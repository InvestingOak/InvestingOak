using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvestingOak.Data.Entities
{
    public class SymbolList : Staleable
    {
        [Key]
        public string Exchange { get; set; }

        public List<StockSymbol> Symbols { get; set; }
    }
}
