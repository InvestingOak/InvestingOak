using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using AutoMapper;
using InvestingOak.Converters;
using InvestingOak.Data;
using InvestingOak.Data.Entities;
using InvestingOak.Models;
using InvestingOak.Models.Finnhub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using FinnhubStockSymbol = InvestingOak.Models.Finnhub.StockSymbol;
using FinnhubArticle = InvestingOak.Models.Finnhub.NewsArticle;
using NewsArticle = InvestingOak.Data.Entities.NewsArticle;
using Quote = InvestingOak.Data.Entities.Quote;
using Sentiment = InvestingOak.Data.Entities.Sentiment;
using StockSymbol = InvestingOak.Data.Entities.StockSymbol;

namespace InvestingOak.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    [Produces("application/json")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
    public class StockController : ControllerBase
    {
        private readonly HttpClient alphaVantageClient;
        private readonly string alphaVantageKey;
        private readonly HttpClient finnhubClient;
        private readonly IMapper mapper;
        private readonly IRepository repository;
        private readonly JsonSerializerOptions serializerOptions;

        public StockController(IRepository repository, IMapper mapper, IConfiguration configuration)
        {
            this.repository = repository;
            this.mapper = mapper;

            // Configure HttpClients
            alphaVantageKey = configuration["Keys:AlphaVantage"];
            alphaVantageClient = new HttpClient();
            string finnhubKey = configuration["Keys:Finnhub"];
            finnhubClient = new HttpClient();
            finnhubClient.DefaultRequestHeaders.Add("X-Finnhub-Token", finnhubKey);

            // Configure JsonSerializer
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
                {
                    new Int32Converter(),
                    new LongConverter(),
                    new DecimalConverter(),
                    new DateTimeOffsetConverter()
                }
            };
        }

        [HttpGet]
        public ActionResult Symbols(string exchange)
        {
            exchange = exchange.ToUpperInvariant();

            SymbolList symbolList = repository.GetSymbolList(exchange);

            if (symbolList is not null && !symbolList.IsStale(1440))
            {
                return Ok(symbolList.Symbols);
            }

            var parameters = $"https://finnhub.io/api/v1/stock/symbol?exchange={exchange}";

            HttpResponseMessage response = finnhubClient.GetAsync(parameters).Result;
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest($"Status code: {response.StatusCode}");
            }

            List<FinnhubStockSymbol> stockSymbols =
                response.Content.ReadFromJsonAsync<List<FinnhubStockSymbol>>(serializerOptions).Result;

            bool shouldAdd = symbolList is null;

            symbolList = new SymbolList
            {
                Exchange = exchange,
                Symbols = mapper.Map<List<StockSymbol>>(stockSymbols),
                LastUpdated = DateTimeOffset.UtcNow
            };

            if (shouldAdd)
            {
                repository.AddEntity(symbolList);
            }

            repository.SaveAll();

            return Ok(symbolList.Symbols);
        }

        [HttpGet("{symbol}/BalanceSheet")]
        public ActionResult BalanceSheet(string symbol)
        {
            symbol = symbol.ToUpperInvariant();
            BalanceSheet balanceSheet = repository.GetBalanceSheet(symbol);

            if (balanceSheet is not null && !balanceSheet.IsStale(1440))
            {
                return Ok(balanceSheet);
            }
            var parameters =
                $"https://www.alphavantage.co/query?function=BALANCE_SHEET&symbol={symbol}&apikey={alphaVantageKey}";
            HttpResponseMessage response = alphaVantageClient.GetAsync(parameters).Result;
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest($"Status code: {response.StatusCode}");
            }
            BalanceSheet2 balanceSheet2 = response.Content.ReadFromJsonAsync<BalanceSheet2>(serializerOptions).Result;
            if (balanceSheet2?.Symbol is null)
            {
                return BadRequest("Symbol is invalid or balance sheet does not exist");
            }

            bool shouldAdd = balanceSheet is null;
            balanceSheet = mapper.Map<BalanceSheet>(balanceSheet2);
            balanceSheet.LastUpdated = DateTimeOffset.UtcNow;
            if (shouldAdd)
            {
                repository.AddEntity(balanceSheet);
            }

            repository.SaveAll();
            return Ok(balanceSheet);
        }
        [HttpGet("{symbol}/CashFlow")]
        public ActionResult BalanceSheet(string symbol)
        {
            symbol = symbol.ToUpperInvariant();
            CashFlow cashFlow= repository.GetCashFlow(symbol);

            if (cashFlow is not null && !cashFlow.IsStale(1440))
            {
                return Ok(cashFlow);
            }
            var parameters =
                $"https://www.alphavantage.co/query?function=CASH_FLOW&symbol={symbol}&apikey={alphaVantageKey}";
            HttpResponseMessage response = alphaVantageClient.GetAsync(parameters).Result;
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest($"Status code: {response.StatusCode}");
            }
            CashFlow2 cashFlow2 = response.Content.ReadFromJsonAsync<CashFlow2>(serializerOptions).Result;
            if (cashFlow2?.Symbol is null)
            {
                return BadRequest("Symbol is invalid or failed to get cash flow");
            }

            bool shouldAdd = cashFlow is null;
            cashFlow = mapper.Map<CashFlow>(cashFlow2);
            balanceSheet.LastUpdated = DateTimeOffset.UtcNow;
            if (shouldAdd)
            {
                repository.AddEntity(CashFlow);
            }

            repository.SaveAll();
            return Ok(cashFlow);
        }


        [HttpGet("{symbol}/profile")]
        public ActionResult Profile(string symbol)
        {
            symbol = symbol.ToUpperInvariant();
            CompanyProfile profile = repository.GetCompanyProfile(symbol);

            if (profile is not null && !profile.IsStale(1440))
            {
                return Ok(profile);
            }

            var parameters =
                $"https://www.alphavantage.co/query?function=OVERVIEW&symbol={symbol}&apikey={alphaVantageKey}";
            HttpResponseMessage response = alphaVantageClient.GetAsync(parameters).Result;
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest($"Status code: {response.StatusCode}");
            }

            StockOverviewInput aProfile =
                response.Content.ReadFromJsonAsync<StockOverviewInput>(serializerOptions).Result;
            if (aProfile is null)
            {
                return BadRequest("Failed to get company profile.");
            }

            parameters = $"https://finnhub.io/api/v1/stock/profile2?symbol={symbol}";
            response = finnhubClient.GetAsync(parameters).Result;
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest($"Status code: {response.StatusCode}");
            }

            CompanyProfile2 fProfile = response.Content.ReadFromJsonAsync<CompanyProfile2>(serializerOptions).Result;

            if (fProfile?.Ticker is null)
            {
                return BadRequest("Failed to get company profile.");
            }

            bool shouldAdd = profile is null;

            profile = mapper.Map<CompanyProfile>(aProfile);
            profile.LogoUrl = fProfile.LogoUrl;
            profile.WebUrl = fProfile.WebUrl;
            profile.LastUpdated = DateTimeOffset.UtcNow;

            if (shouldAdd)
            {
                repository.AddEntity(profile);
            }

            repository.SaveAll();

            return Ok(profile);
        }

        [HttpGet("{symbol}/sentiment")]
        public ActionResult Sentiment(string symbol)
        {
            symbol = symbol.ToUpperInvariant();
            Sentiment sentiment = repository.GetSentiment(symbol);

            if (sentiment is not null && !sentiment.IsStale(1440))
            {
                return Ok(sentiment);
            }

            var parameters = $"https://finnhub.io/api/v1/news-sentiment?symbol={symbol}";

            HttpResponseMessage response = finnhubClient.GetAsync(parameters).Result;
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest($"Status code: {response.StatusCode}");
            }

            NewsSentiment newsSentiment = response.Content.ReadFromJsonAsync<NewsSentiment>(serializerOptions).Result;

            if (newsSentiment?.Symbol is null)
            {
                return BadRequest("Symbol is invalid or news sentiment does not exist.");
            }

            bool shouldAdd = sentiment is null;

            sentiment = mapper.Map<Sentiment>(newsSentiment);
            sentiment.LastUpdated = DateTimeOffset.UtcNow;

            if (shouldAdd)
            {
                repository.AddEntity(sentiment);
            }

            repository.SaveAll();

            return Ok(sentiment);
        }

        [HttpGet("{symbol}/news")]
        public ActionResult News(string symbol, DateTimeOffset from, DateTimeOffset to)
        {
            symbol = symbol.ToUpperInvariant();
            ArticleList articleList = repository.GetNewsArticles("company", symbol);

            if (articleList is not null && !articleList.IsStale(15))
            {
                IEnumerable<NewsArticle> ret = articleList.Articles.Where(a =>
                {
                    DateTimeOffset dto = DateTimeOffset.FromUnixTimeSeconds(a.DateTime);
                    return dto >= from && dto <= to;
                });
                return Ok(ret);
            }

            var fromStr = from.ToString("yyyy-MM-dd");
            var toStr = to.ToString("yyyy-MM-dd");
            var parameters =
                $"https://finnhub.io/api/v1/company-news?symbol={symbol}&from={fromStr}&to={toStr}";

            HttpResponseMessage response = finnhubClient.GetAsync(parameters).Result;
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest($"Status code: {response.StatusCode}");
            }

            List<FinnhubArticle> articles =
                response.Content.ReadFromJsonAsync<List<FinnhubArticle>>(serializerOptions).Result;

            bool shouldAdd = articleList is null;

            articleList = new ArticleList
            {
                Category = "company",
                Symbol = symbol,
                Articles = mapper.Map<List<NewsArticle>>(articles),
                LastUpdated = DateTimeOffset.UtcNow
            };

            if (shouldAdd)
            {
                repository.AddEntity(articleList);
            }

            repository.SaveAll();

            IEnumerable<NewsArticle> articlesRet = articleList.Articles.Where(a =>
            {
                DateTimeOffset dto = DateTimeOffset.FromUnixTimeSeconds(a.DateTime);
                return dto >= from && dto <= to;
            });

            return Ok(articlesRet);
        }

        [HttpGet("{symbol}/recommendation")]
        public ActionResult Recommendation(string symbol)
        {
            symbol = symbol.ToUpperInvariant();
            Recommendations recommendations = repository.GetRecommendations(symbol);

            // Fetch data if it does not exist in the database or is older than 1 day.
            if (recommendations is not null && !recommendations.IsStale(1440))
            {
                return Ok(recommendations);
            }

            var parameters = $"https://finnhub.io/api/v1/stock/recommendation?symbol={symbol}";

            HttpResponseMessage response = finnhubClient.GetAsync(parameters).Result;
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest($"Failed to get recommendations. Status code: {response.StatusCode}");
            }

            List<Recommendation> recommendationList =
                response.Content.ReadFromJsonAsync<List<Recommendation>>(serializerOptions).Result;

            if (recommendationList is null)
            {
                return BadRequest("Recommendation was empty.");
            }

            bool shouldAdd = recommendations is null;

            recommendations = mapper.Map<Recommendations>(recommendationList[0]);
            recommendations.Symbol = symbol;
            recommendations.LastUpdated = DateTimeOffset.UtcNow;

            if (shouldAdd)
            {
                repository.AddEntity(recommendations);
            }

            repository.SaveAll();

            return Ok(recommendations);
        }

        [HttpGet("{symbol}/pricetarget")]
        public ActionResult PriceTarget(string symbol)
        {
            symbol = symbol.ToUpperInvariant();
            PriceTargets priceTarget = repository.GetPriceTargets(symbol);

            if (priceTarget is not null && !priceTarget.IsStale(1440))
            {
                return Ok(priceTarget);
            }

            var parameters = $"https://finnhub.io/api/v1/stock/price-target?symbol={symbol}";

            HttpResponseMessage response = finnhubClient.GetAsync(parameters).Result;
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest($"Status code: {response.StatusCode}");
            }

            PriceTarget target = response.Content.ReadFromJsonAsync<PriceTarget>(serializerOptions).Result;

            if (target is null)
            {
                return BadRequest("Price target was empty.");
            }

            bool shouldAdd = priceTarget is null;

            priceTarget = mapper.Map<PriceTargets>(target);
            priceTarget.LastUpdated = DateTimeOffset.UtcNow;

            if (shouldAdd)
            {
                repository.AddEntity(priceTarget);
            }

            repository.SaveAll();

            return Ok(priceTarget);
        }


        [HttpGet("{symbol}/quote")]
        public ActionResult Quote(string symbol)
        {
            symbol = symbol.ToUpperInvariant();
            Quote quote = repository.GetQuote(symbol);

            if (quote is not null && !quote.IsStale(15))
            {
                return Ok(quote);
            }

            // Fetch data if it does not exist in the database or is older than 15 minutes.
            var today = DateTimeOffset.Now.ToUnixTimeSeconds();
            var fromDate = new DateTimeOffset(DateTime.Now.AddDays(-3)).ToUnixTimeSeconds();
            string parameters = $"https://finnhub.io/api/v1/stock/candle?symbol={symbol}" +
                                $"&resolution=D&from={fromDate}&to={today}";

            HttpResponseMessage response = finnhubClient.GetAsync(parameters).Result;
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest($"Failed to get quote. Status code: {response.StatusCode}");
            }

            Candles candles = response.Content.ReadFromJsonAsync<Candles>().Result;
            if (candles is null)
            {
                return BadRequest("Failed to get quote.");
            }

            bool shouldAdd = quote is null;

            quote = mapper.Map<Quote>(candles);
            quote.Symbol = symbol;
            quote.LastUpdated = DateTimeOffset.UtcNow;

            if (shouldAdd)
            {
                repository.AddEntity(quote);
            }

            repository.SaveAll();

            return Ok(quote);
        }
    }
}
