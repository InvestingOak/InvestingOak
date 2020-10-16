using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using InvestingOak.Converters;
using InvestingOak.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestingOak.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    [Produces("application/json")]
    public class StocksController : Controller
    {
        private const string AlphaVantageKey = "HNAFS0NKXJHEG5YI";
        private const string AlphaVantageBaseUrl = "https://www.alphavantage.co/query";
        private readonly HttpClient client;
        private readonly JsonSerializerOptions serializerOptions;

        public StocksController()
        {
            // Initialize HttpClient
            client = new HttpClient
            {
                BaseAddress = new Uri(AlphaVantageBaseUrl)
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Configure JsonSerializer
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            serializerOptions.Converters.Add(new Int32Converter());
            serializerOptions.Converters.Add(new LongConverter());
            serializerOptions.Converters.Add(new DecimalConverter());
            serializerOptions.Converters.Add(new DateTimeOffsetConverter());
        }

        // GET api/stocks/overview?{symbol}
        [HttpGet("{symbol}/overview")]
        public IActionResult Overview(string symbol)
        {
            var parameters = $"?function=OVERVIEW&symbol={symbol.ToUpper()}&apikey={AlphaVantageKey}";

            HttpResponseMessage response = client.GetAsync(parameters).Result;
            if (!response.IsSuccessStatusCode) return BadRequest($"Request failed. Status code: {response.StatusCode}");

            StockOverview dataObject = response.Content.ReadFromJsonAsync<StockOverview>(serializerOptions).Result;
            if (dataObject is null) return BadRequest("Response was empty.");

            if (dataObject.Symbol is null)
                return StatusCode(StatusCodes.Status429TooManyRequests,
                    "Rate limit exceeded. Limit is 5 calls per minute and 500 calls per day.");

            return Ok(dataObject);
        }

        [HttpGet("{symbol}/incomestatement")]
        public IActionResult IncomeStatement(string symbol)
        {
            var parameters = $"?function=INCOME_STATEMENT&symbol={symbol.ToUpper()}&apikey={AlphaVantageKey}";

            HttpResponseMessage response = client.GetAsync(parameters).Result;
            if (!response.IsSuccessStatusCode) return BadRequest($"Request failed. Status code: {response.StatusCode}");

            IncomeStatementCollection dataObject =
                response.Content.ReadFromJsonAsync<IncomeStatementCollection>(serializerOptions).Result;
            if (dataObject is null) return BadRequest("Response was empty.");

            if (dataObject.Symbol is null)
                return StatusCode(StatusCodes.Status429TooManyRequests,
                    "Rate limit exceeded. Limit is 5 calls per minute and 500 calls per day.");

            return Ok(dataObject);
        }
    }
}
