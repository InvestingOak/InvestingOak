using System.ComponentModel.DataAnnotations;

namespace InvestingOak.Data.Entities
{
    public class Sentiment : Staleable
    {
        [Key]
        public string Symbol { get; set; }

        public uint ArticlesInLastWeek { get; set; }

        public double Buzz { get; set; }

        public double WeeklyAverage { get; set; }

        public double CompanyNewsScore { get; set; }

        public double SectorAverageBullishPercent { get; set; }

        public double SectorAverageNewsScore { get; set; }

        public double BearishPercent { get; set; }

        public double BullishPercent { get; set; }
    }
}
