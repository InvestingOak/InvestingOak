using System;
using System.Text.Json.Serialization;

namespace InvestingOak.Models
{
    public class StockOverviewInput : AlphaVantage
    {
        [JsonPropertyName("Symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("AssetType")]
        public string AssetType { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Exchange")]
        public string Exchange { get; set; }

        [JsonPropertyName("Currency")]
        public string Currency { get; set; }

        [JsonPropertyName("Country")]
        public string Country { get; set; }

        [JsonPropertyName("Sector")]
        public string Sector { get; set; }

        [JsonPropertyName("Industry")]
        public string Industry { get; set; }

        [JsonPropertyName("Address")]
        public string Address { get; set; }

        [JsonPropertyName("FullTimeEmployees")]
        public long FullTimeEmployees { get; set; }

        [JsonPropertyName("FiscalYearEnd")]
        public string FiscalYearEnd { get; set; }

        [JsonPropertyName("LatestQuarter")]
        public DateTimeOffset LatestQuarter { get; set; }

        [JsonPropertyName("MarketCapitalization")]
        public decimal MarketCapitalization { get; set; }

        [JsonPropertyName("EBITDA")]
        public decimal EBITDA { get; set; }

        [JsonPropertyName("PERatio")]
        public decimal PERatio { get; set; }

        [JsonPropertyName("PEGRatio")]
        public decimal PEGRatio { get; set; }

        [JsonPropertyName("BookValue")]
        public decimal BookValue { get; set; }

        [JsonPropertyName("DividendPerShare")]
        public decimal DividendPerShare { get; set; }

        [JsonPropertyName("DividendYear")]
        public decimal DividendYear { get; set; }

        [JsonPropertyName("EPS")]
        public decimal EarningsPerShare { get; set; }

        [JsonPropertyName("RevenuePerShareTTM")]
        public decimal RevenuePerShareTTM { get; set; }

        [JsonPropertyName("ProfitMargin")]
        public decimal ProfitMargin { get; set; }

        [JsonPropertyName("OperatingMarginTTM")]
        public decimal OperatingMarginTTM { get; set; }

        [JsonPropertyName("ReturnOnAssetsTTM")]
        public decimal ReturnOnAssetsTTM { get; set; }

        [JsonPropertyName("ReturnOnEquityTTM")]
        public decimal ReturnOnEquityTTM { get; set; }

        [JsonPropertyName("RevenueTTM")]
        public decimal RevenueTTM { get; set; }

        [JsonPropertyName("GrossProfitTTM")]
        public decimal GrossProfitTTM { get; set; }

        [JsonPropertyName("DilutedEPSTTM")]
        public decimal DilutedEPSTTM { get; set; }

        [JsonPropertyName("QuarterlyEarningsGrowthYOY")]
        public decimal QuarterlyEarningsGrowthYOY { get; set; }

        [JsonPropertyName("QuarterlyRevenueGrowthYOY")]
        public decimal QuarterlyRevenueGrowthYOY { get; set; }

        [JsonPropertyName("AnalystTargetPrice")]
        public decimal AnalystTargetPrice { get; set; }

        [JsonPropertyName("TrailingPE")]
        public decimal TrailingPE { get; set; }

        [JsonPropertyName("ForwardPE")]
        public decimal ForwardPE { get; set; }

        [JsonPropertyName("PriceToSalesRatioTTM")]
        public decimal PriceToSalesRatioTTM { get; set; }

        [JsonPropertyName("PriceToBookRatio")]
        public decimal PriceToBookRatio { get; set; }

        [JsonPropertyName("EVToRevenue")]
        public decimal EVToRevenue { get; set; }

        [JsonPropertyName("EVToEBITDA")]
        public decimal EVToEBITDA { get; set; }

        [JsonPropertyName("Beta")]
        public decimal Beta { get; set; }

        [JsonPropertyName("52WeekHigh")]
        public decimal WeekHigh52 { get; set; }

        [JsonPropertyName("52WeekLow")]
        public decimal WeekLow52 { get; set; }

        [JsonPropertyName("50DayMovingAverage")]
        public decimal MovingAverageDaily50 { get; set; }

        [JsonPropertyName("200DayMovingAverage")]
        public decimal MovingAverageDaily200 { get; set; }

        [JsonPropertyName("SharesOutstanding")]
        public long SharesOutstanding { get; set; }

        [JsonPropertyName("SharesFloat")]
        public long SharesFloat { get; set; }

        [JsonPropertyName("SharesShort")]
        public long SharesShort { get; set; }

        [JsonPropertyName("SharesShortPriorMonth")]
        public long SharesShortPriorMonth { get; set; }

        [JsonPropertyName("ShortRatio")]
        public decimal ShortRatio { get; set; }

        [JsonPropertyName("ShortPercentOutstanding")]
        public decimal ShortPercentOutstanding { get; set; }

        [JsonPropertyName("ShortPercentFloat")]
        public decimal ShortPercentFloat { get; set; }

        [JsonPropertyName("PercentInsiders")]
        public decimal PercentInsiders { get; set; }

        [JsonPropertyName("PercentInstitutions")]
        public decimal PercentInstitutions { get; set; }

        [JsonPropertyName("ForwardAnnualDividendRate")]
        public decimal ForwardAnnualDividendRate { get; set; }

        [JsonPropertyName("ForwardAnnualDividendYield")]
        public decimal ForwardAnnualDividendYield { get; set; }

        [JsonPropertyName("PayoutRatio")]
        public decimal PayoutRatio { get; set; }

        [JsonPropertyName("DividendDate")]
        public DateTimeOffset DividendDate { get; set; }

        [JsonPropertyName("ExDividendDate")]
        public DateTimeOffset ExDividendDate { get; set; }

        [JsonPropertyName("LastSplitFactor")]
        public string LastSplitFactor { get; set; }

        [JsonPropertyName("LastSplitDate")]
        public DateTimeOffset LastSplitDate { get; set; }
    }
}
