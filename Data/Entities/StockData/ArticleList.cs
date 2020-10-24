using System.Collections.Generic;

namespace InvestingOak.Data.Entities.StockData
{
    public class ArticleList : Staleable
    {
        public string Category { get; set; }

        public string Symbol { get; set; }

        public List<NewsArticle> Articles { get; set; }
    }
}
