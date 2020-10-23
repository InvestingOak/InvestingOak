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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using FinnhubArticle = InvestingOak.Models.Finnhub.NewsArticle;

namespace InvestingOak.Controllers
{
    [Route("api/news")]
    [ApiController]
    [Produces("application/json")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
    public class NewsController : ControllerBase
    {
        private readonly HttpClient finnhubClient;
        private readonly IMapper mapper;
        private readonly IRepository repository;
        private readonly JsonSerializerOptions serializerOptions;

        public NewsController(IRepository repository, IMapper mapper, IConfiguration configuration)
        {
            this.repository = repository;
            this.mapper = mapper;

            // Configure HttpClients
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
                    new DateTimeOffsetConverter()
                }
            };
        }

        [HttpGet("{category}")]
        public ActionResult MarketNews(string category, int minId = 0)
        {
            category = category.ToLowerInvariant();
            ArticleList articleList = repository.GetNewsArticles(category);

            if (articleList is not null && !articleList.IsStale(15))
            {
                IEnumerable<NewsArticle> ret = articleList.Articles.Where(a => a.Id >= minId);
                return Ok(ret);
            }

            var parameters = $"https://finnhub.io/api/v1/news?category={category}&minId={minId}";
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
                Category = category,
                Symbol = "",
                Articles = mapper.Map<List<NewsArticle>>(articles),
                LastUpdated = DateTimeOffset.UtcNow
            };

            if (shouldAdd)
            {
                repository.AddEntity(articleList);
            }

            repository.SaveAll();

            return Ok(articleList.Articles);
        }
    }
}
