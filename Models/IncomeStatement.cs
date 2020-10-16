using System;

namespace InvestingOak.Models
{
    public class IncomeStatement
    {
        public DateTimeOffset FiscalDateEnding { get; set; }

        public string ReportedCurrency { get; set; }

        public long TotalRevenue { get; set; }

        public long TotalOperatingExpense { get; set; }

        public long CostOfRevenue { get; set; }

        public long GrossProfit { get; set; }

        public long Ebit { get; set; }

        public long NetIncome { get; set; }

        public long ResearchAndDevelopment { get; set; }

        public long EffectOfAccountingCharges { get; set; }

        public long IncomeBeforeTax { get; set; }

        public long MinorityInterest { get; set; }

        public long SellingGeneralAdministrative { get; set; }

        public long OtherNonOperatingIncome { get; set; }

        public long OperatingIncome { get; set; }

        public long InterestExpense { get; set; }

        public long TaxProvision { get; set; }

        public long InterestIncome { get; set; }

        public long NetInterestIncome { get; set; }

        public long ExtraordinaryItems { get; set; }

        public long NonRecurring { get; set; }

        public long OtherItems { get; set; }

        public long IncomeTaxExpense { get; set; }

        public long TotalOtherIncomeExpense { get; set; }

        public long DiscontinuedOperations { get; set; }

        public long NetIncomeFromContinuingOperations { get; set; }

        public long NetIncomeApplicableToCommonShares { get; set; }

        public long PreferredStockAndOtherAdjustments { get; set; }
    }
}
