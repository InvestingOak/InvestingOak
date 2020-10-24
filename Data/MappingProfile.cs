﻿using System.Linq;
using AutoMapper;
using InvestingOak.Data.Entities.PaperTrading;
using InvestingOak.Data.Entities.StockData;
using InvestingOak.Models.AlphaVantage;
using InvestingOak.Models.Finnhub;
using InvestingOak.Models.PaperTrading;
using FinnhubStockSymbol = InvestingOak.Models.Finnhub.StockSymbol;
using FinnhubPriceTarget = InvestingOak.Models.Finnhub.PriceTarget;
using FinnhubNewsArticle = InvestingOak.Models.Finnhub.NewsArticle;
using NewsArticle = InvestingOak.Data.Entities.StockData.NewsArticle;
using Quote = InvestingOak.Data.Entities.StockData.Quote;
using Sentiment = InvestingOak.Data.Entities.StockData.Sentiment;
using StockSymbol = InvestingOak.Data.Entities.StockData.StockSymbol;

namespace InvestingOak.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Stock Data mappings
            CreateMap<StockOverviewInput, StockOverview>();
            CreateMap<StockQuoteInput, StockQuote>();
            CreateMap<GlobalQuoteInput, GlobalQuote>();

            CreateMap<FinnhubStockSymbol, StockSymbol>();
            CreateMap<StockOverviewInput, CompanyProfile>();
            CreateMap<Recommendation, Recommendations>();
            CreateMap<FinnhubNewsArticle, NewsArticle>();

            CreateMap<Candles, Quote>()
                .ForMember(q => q.Close, ex => ex.MapFrom(c => c.Close.LastOrDefault()))
                .ForMember(q => q.High, ex => ex.MapFrom(c => c.Close.LastOrDefault()))
                .ForMember(q => q.Low, ex => ex.MapFrom(c => c.Low.LastOrDefault()))
                .ForMember(q => q.Open, ex => ex.MapFrom(c => c.Open.LastOrDefault()))
                .ForMember(q => q.Volume, ex => ex.MapFrom(c => c.Volume.LastOrDefault()))
                .ForMember(q => q.PreviousClose, ex => ex.MapFrom(c => c.Close.GetValue(c.Close.Length - 2)));

            CreateMap<FinnhubPriceTarget, PriceTargets>()
                .ForMember(p => p.Period, ex => ex.MapFrom(t => t.LastUpdated));

            CreateMap<NewsSentiment, Sentiment>()
                .ForMember(s => s.Buzz, ex => ex.MapFrom(n => n.Buzz.Value))
                .ForMember(s => s.ArticlesInLastWeek, ex => ex.MapFrom(n => n.Buzz.ArticlesInLastWeek))
                .ForMember(s => s.WeeklyAverage, ex => ex.MapFrom(n => n.Buzz.WeeklyAverage))
                .ForMember(s => s.BearishPercent, ex => ex.MapFrom(n => n.Sentiment.BearishPercent))
                .ForMember(s => s.BullishPercent, ex => ex.MapFrom(n => n.Sentiment.BullishPercent));

            // Paper Trading mappings
            CreateMap<ProjectCreateModel, Project>();
            CreateMap<ProjectCreateModel, ProjectDescModel>();
            CreateMap<Project, ProjectModel>();
            CreateMap<Project, ProjectDescModel>();
            CreateMap<Project, ProjectUpdateModel>().ReverseMap();
        }
    }
}
