using System;
using System.ComponentModel.DataAnnotations;

namespace InvestingOak.Data.Entities
{
    public class PriceTargets : Staleable
    {
        [Key]
        public string Symbol { get; set; }

        public DateTimeOffset Period { get; set; }

        public decimal TargetHigh { get; set; }

        public decimal TargetLow { get; set; }

        public decimal TargetMean { get; set; }

        public decimal TargetMedian { get; set; }
    }
}
