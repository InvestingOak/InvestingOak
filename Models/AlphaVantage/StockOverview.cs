using System;

namespace InvestingOak.Models.AlphaVantage
{
    public class StockOverview
    {
        public string Symbol { get; set; }

        public string AssetType { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Exchange { get; set; }

        public string Currency { get; set; }

        public string Country { get; set; }

        public string Sector { get; set; }

        public string Industry { get; set; }

        public string Address { get; set; }

        public long FullTimeEmployees { get; set; }

        public string FiscalYearEnd { get; set; }

        public DateTimeOffset LatestQuarter { get; set; }

        public decimal MarketCapitalization { get; set; }

        public decimal EBITDA { get; set; }

        public decimal PERatio { get; set; }

        public decimal PEGRatio { get; set; }

        public decimal BookValue { get; set; }

        public decimal DividendPerShare { get; set; }

        public decimal DividendYear { get; set; }

        public decimal EarningsPerShare { get; set; }

        public decimal RevenuePerShareTTM { get; set; }

        public decimal ProfitMargin { get; set; }

        public decimal OperatingMarginTTM { get; set; }

        public decimal ReturnOnAssetsTTM { get; set; }

        public decimal ReturnOnEquityTTM { get; set; }

        public decimal RevenueTTM { get; set; }

        public decimal GrossProfitTTM { get; set; }

        public decimal DilutedEPSTTM { get; set; }

        public decimal QuarterlyEarningsGrowthYOY { get; set; }

        public decimal QuarterlyRevenueGrowthYOY { get; set; }

        public decimal AnalystTargetPrice { get; set; }

        public decimal TrailingPE { get; set; }

        public decimal ForwardPE { get; set; }

        public decimal PriceToSalesRatioTTM { get; set; }

        public decimal PriceToBookRatio { get; set; }

        public decimal EVToRevenue { get; set; }

        public decimal EVToEBITDA { get; set; }

        public decimal Beta { get; set; }

        public decimal WeekHigh52 { get; set; }

        public decimal WeekLow52 { get; set; }

        public decimal MovingAverageDaily50 { get; set; }

        public decimal MovingAverageDaily200 { get; set; }

        public long SharesOutstanding { get; set; }

        public long SharesFloat { get; set; }

        public long SharesShort { get; set; }

        public long SharesShortPriorMonth { get; set; }

        public decimal ShortRatio { get; set; }

        public decimal ShortPercentOutstanding { get; set; }

        public decimal ShortPercentFloat { get; set; }

        public decimal PercentInsiders { get; set; }

        public decimal PercentInstitutions { get; set; }

        public decimal ForwardAnnualDividendRate { get; set; }

        public decimal ForwardAnnualDividendYield { get; set; }

        public decimal PayoutRatio { get; set; }

        public DateTimeOffset DividendDate { get; set; }

        public DateTimeOffset ExDividendDate { get; set; }

        public string LastSplitFactor { get; set; }

        public DateTimeOffset LastSplitDate { get; set; }
    }
}
