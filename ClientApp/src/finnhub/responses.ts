export interface CompanyProfile2 {
  country: string;
  currency: string;
  exchange: string;
  ipo: Date;
  marketCapitalization: number;
  name: string;
  phone: string;
  shareOutstanding: number;
  ticker: string;
  weburl: string;
  logo: string;
  finnhubIndustry: string;
}

export interface StockSymbol {
  description: string;
  displaySymbol: string;
  symbol: string;
  type: string;
  currency: string;
}

export interface News {
  category: string;
  datetime: number;
  headline: string;
  id: number;
  image: string;
  related: string;
  source: string;
  summary: string;
  url: string
}

export interface NewsSentiment {
  buzz: {
    articlesInLastWeek: number;
    buzz: number;
    weeklyAverage: number
  };
  companyNewsScore: number;
  sectorAverageBullishPercent: number;
  sectorAverageNewsScore: number;
  sentiment: {
    bearishPercent: number;
    bullishPercent: number
  };
  symbol: string
}

export interface RecommendationTrend {
  buy: number;
  hold: number;
  period: Date;
  sell: number;
  strongBuy: number;
  strongSell: number;
  symbol: string;
}

export interface PriceTarget {
  lastUpdated: Date;
  symbol: string;
  targetHigh: number;
  targetLow: number;
  targetMean: number;
  targetMedian: number;
}

export interface Quote {
  o: number;
  h: number;
  l: number;
  c: number;
  pc: number;
}

export interface StockCandles {
  c: number[],
  h: number[],
  l: number[],
  o: number[],
  s: string,
  t: number[],
  v: number[]
}
