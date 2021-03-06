﻿using System.Collections.Generic;

namespace InvestingOak.Models
{
    public class IncomeStatementCollection : AlphaVantage
    {
        public string Symbol { get; set; }

        public IEnumerable<IncomeStatement> AnnualReports { get; set; }

        public IEnumerable<IncomeStatement> QuarterlyReports { get; set; }
    }
}
