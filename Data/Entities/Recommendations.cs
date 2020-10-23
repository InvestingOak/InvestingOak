using System;
using System.ComponentModel.DataAnnotations;

namespace InvestingOak.Data.Entities
{
    public class Recommendations : Staleable
    {
        [Key]
        public string Symbol { get; set; }

        public DateTimeOffset Period { get; set; }

        public uint Buy { get; set; }

        public uint Hold { get; set; }

        public uint Sell { get; set; }

        public uint StrongBuy { get; set; }

        public uint StrongSell { get; set; }
    }
}
