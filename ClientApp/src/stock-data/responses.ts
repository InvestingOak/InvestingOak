export interface StockSymbol {
  description: string;
  displaySymbol: string;
  symbol: string;
  type: string;
  currency: string;
}

export interface News {
  category: string;
  dateTime: number;
  headline: string;
  id: number;
  imageUrl: string;
  related: string;
  source: string;
  summary: string;
  url: string
}

export interface NewsSentiment {
  articlesInLastWeek: number;
  buzz: number;
  weeklyAverage: number
  companyNewsScore: number;
  sectorAverageBullishPercent: number;
  sectorAverageNewsScore: number;
  bearishPercent: number;
  bullishPercent: number
  symbol: string
}

export interface Recommendations {
  buy: number;
  hold: number;
  period: Date;
  sell: number;
  strongBuy: number;
  strongSell: number;
  symbol: string;
}

export interface PriceTarget {
  period: Date;
  symbol: string;
  targetHigh: number;
  targetLow: number;
  targetMean: number;
  targetMedian: number;
}

export interface Quote {
  open: number;
  high: number;
  low: number;
  close: number;
  previousClose: number;
  volume: number;
}

export interface CompanyProfile {
  symbol: string;
  assetType: string;
  name: string;
  description: string;
  exchange: string;
  currency: string;
  country: string;
  sector: string;
  industry: string;
  address: string;
  fullTimeEmployees: number;
  fiscalYearEnd: string;
  latestQuarter: Date,
  marketCapitalization: number;
  ebitda: number;
  peRatio: number;
  pegRatio: number;
  bookValue: number;
  dividendPerShare: number;
  dividendYear: number;
  earningsPerShare: number;
  revenuePerShareTTM: number;
  profitMargin: number;
  operatingMarginTTM: number;
  returnOnAssetsTTM: number;
  returnOnEquityTTM: number;
  revenueTTM: number;
  grossProfitTTM: number;
  dilutedEPSTTM: number;
  quarterlyEarningsGrowthYOY: number;
  quarterlyRevenueGrowthYOY: number;
  analystTargetPrice: number;
  trailingPE: number;
  forwardPE: number;
  priceToSalesRatioTTM: number;
  priceToBookRatio: number;
  evToRevenue: number;
  evToEbitda: number;
  beta: number;
  weekHigh52: number;
  weekLow52: number;
  movingAverageDaily50: number;
  movingAverageDaily200: number;
  sharesOutstanding: number;
  sharesFloat: number;
  sharesShort: number;
  sharesShortPriorMonth: number;
  shortRatio: number;
  shortPercentOutstanding: number;
  shortPercentFloat: number;
  percentInsiders: number;
  percentInstitutions: number;
  forwardAnnualDividendRate: number;
  forwardAnnualDividendYield: number;
  payoutRatio: number;
  dividendDate: Date,
  exDividendDate: Date,
  lastSplitFactor: string,
  lastSplitDate: Date,
  logoUrl: string,
  webUrl: string
}
