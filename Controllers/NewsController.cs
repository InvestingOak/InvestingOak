using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using InvestingOak.Converters;
using InvestingOak.Models.Finnhub;
using Microsoft.AspNetCore.Mvc;

namespace InvestingOak.Controllers
{
    [Route("api/news")]
    [ApiController]
    [Produces("application/json")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
    public class NewsController : ControllerBase
    {
        private const string FinnhubKey = "btt5puv48v6q0kg1m610";
        private const string FinnhubBaseUrl = "https://finnhub.io/api/v1";
        private readonly HttpClient finnhubClient;
        private readonly JsonSerializerOptions serializerOptions;

        public NewsController()
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
                    new DateTimeOffsetConverter()
                }
            };
        }

        [HttpGet("{category}")]
        public ActionResult MarketNews(string category, int minId = 0)
        {
            var parameters = $"https://finnhub.io/api/v1/news?category={category}&minId={minId}";

            HttpResponseMessage response = finnhubClient.GetAsync(parameters).Result;
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest($"Status code: {response.StatusCode}");
            }

            List<NewsArticle> articles =
                response.Content.ReadFromJsonAsync<List<NewsArticle>>(serializerOptions).Result;

            return Ok(articles);
        }
    }
}
