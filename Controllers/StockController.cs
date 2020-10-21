using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using InvestingOak.Converters;
using InvestingOak.Models.Finnhub;
using Microsoft.AspNetCore.Mvc;

namespace InvestingOak.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    [Produces("application/json")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
    public class StockController : ControllerBase
    {
        private const string FinnhubKey = "btt5puv48v6q0kg1m610";
        private readonly HttpClient finnhubClient;
        private readonly JsonSerializerOptions serializerOptions;

        public StockController()
        {
            // Configure HttpClients
            finnhubClient = new HttpClient();
            finnhubClient.DefaultRequestHeaders.Add("X-Finnhub-Token", FinnhubKey);

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
            var parameters = $"https://finnhub.io/api/v1/stock/symbol?exchange={exchange.ToUpperInvariant()}";

            HttpResponseMessage response = finnhubClient.GetAsync(parameters).Result;
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest($"Status code: {response.StatusCode}");
            }

            List<StockSymbol> stockSymbol =
                response.Content.ReadFromJsonAsync<List<StockSymbol>>(serializerOptions).Result;

            return Ok(stockSymbol);
        }

        [HttpGet("{symbol}/profile")]
        public ActionResult Profile(string symbol)
        {
            var parameters = $"https://finnhub.io/api/v1/stock/profile2?symbol={symbol.ToUpperInvariant()}";

            HttpResponseMessage response = finnhubClient.GetAsync(parameters).Result;
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest($"Status code: {response.StatusCode}");
            }

            CompanyProfile2 profile = response.Content.ReadFromJsonAsync<CompanyProfile2>(serializerOptions).Result;

            if (profile?.Ticker is null)
            {
                return BadRequest("Symbol is invalid or profile does not exist.");
            }

            profile.Exchange = GetExchangeAcronym(profile.Exchange);

            return Ok(profile);
        }

        [HttpGet("{symbol}/sentiment")]
        public ActionResult Sentiment(string symbol)
        {
            var parameters = $"https://finnhub.io/api/v1/news-sentiment?symbol={symbol.ToUpperInvariant()}";

            HttpResponseMessage response = finnhubClient.GetAsync(parameters).Result;
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest($"Status code: {response.StatusCode}");
            }

            NewsSentiment sentiment = response.Content.ReadFromJsonAsync<NewsSentiment>(serializerOptions).Result;

            if (sentiment?.Symbol is null)
            {
                return BadRequest("Symbol is invalid or news sentiment does not exist.");
            }

            return Ok(sentiment);
        }

        [HttpGet("{symbol}/news")]
        public ActionResult News(string symbol, DateTimeOffset from, DateTimeOffset to)
        {
            var fromStr = from.ToString("yyyy-MM-dd");
            var toStr = to.ToString("yyyy-MM-dd");
            var parameters =
                $"https://finnhub.io/api/v1/company-news?symbol={symbol.ToUpperInvariant()}&from={fromStr}&to={toStr}";

            HttpResponseMessage response = finnhubClient.GetAsync(parameters).Result;
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest($"Status code: {response.StatusCode}");
            }

            List<NewsArticle> articles =
                response.Content.ReadFromJsonAsync<List<NewsArticle>>(serializerOptions).Result;

            return Ok(articles);
        }

        [HttpGet("{symbol}/recommendation")]
        public ActionResult Recommendation(string symbol)
        {
            var parameters = $"https://finnhub.io/api/v1/stock/recommendation?symbol={symbol.ToUpperInvariant()}";

            HttpResponseMessage response = finnhubClient.GetAsync(parameters).Result;
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest($"Status code: {response.StatusCode}");
            }

            List<Recommendation> recommendations =
                response.Content.ReadFromJsonAsync<List<Recommendation>>(serializerOptions).Result;

            if (recommendations is null)
            {
                return BadRequest("Recommendation was empty.");
            }

            return Ok(recommendations[0]);
        }

        [HttpGet("{symbol}/pricetarget")]
        public ActionResult PriceTarget(string symbol)
        {
            var parameters = $"https://finnhub.io/api/v1/stock/price-target?symbol={symbol.ToUpperInvariant()}";

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

            return Ok(target);
        }

        [HttpGet("{symbol}/quote")]
        public ActionResult Quote(string symbol)
        {
            var parameters = $"https://finnhub.io/api/v1/quote?symbol={symbol.ToUpperInvariant()}";

            HttpResponseMessage response = finnhubClient.GetAsync(parameters).Result;
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest($"Status code: {response.StatusCode}");
            }

            Quote quote = response.Content.ReadFromJsonAsync<Quote>(serializerOptions).Result;

            if (quote is null)
            {
                return BadRequest("Quote was empty.");
            }

            return Ok(quote);
        }

        [HttpGet("{symbol}/candles")]
        public ActionResult Candles(string symbol, string resolution, DateTimeOffset from, DateTimeOffset to)
        {
            string parameters = $"https://finnhub.io/api/v1/stock/candle?symbol={symbol.ToUpperInvariant()}" +
                                $"&resolution={resolution}&from={from.ToUnixTimeSeconds()}&to={to.ToUnixTimeSeconds()}";

            HttpResponseMessage response = finnhubClient.GetAsync(parameters).Result;
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest($"Status code: {response.StatusCode}");
            }

            Candles candles = response.Content.ReadFromJsonAsync<Candles>(serializerOptions).Result;

            if (candles is null)
            {
                return BadRequest("Candles are empty.");
            }

            return Ok(candles);
        }

        private static string GetExchangeAcronym(string exchange)
        {
            return exchange switch
            {
                "NASDAQ NMS - GLOBAL MARKET" => "NASDAQ",
                "NEW YORK STOCK EXCHANGE, INC." => "NYSE",
                "NYSE MKT LLC" => "AME",
                "OTC MARKETS" => "OTC",
                _ => exchange
            };
        }
    }
}
