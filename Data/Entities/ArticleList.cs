using System.Collections.Generic;

namespace InvestingOak.Data.Entities
{
    public class ArticleList : Staleable
    {
        public string Category { get; set; }

        public string Symbol { get; set; }

        public List<NewsArticle> Articles { get; set; }
    }
}
