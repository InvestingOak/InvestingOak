using System;

namespace InvestingOak.Data.Entities
{
    public abstract class Staleable
    {
        public DateTimeOffset LastUpdated { get; set; }

        public bool IsStale(double minutes)
        {
            TimeSpan interval = DateTimeOffset.UtcNow - LastUpdated;
            return interval.TotalMinutes > minutes;
        }
    }
}
