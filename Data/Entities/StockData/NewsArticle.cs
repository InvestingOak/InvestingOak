using System.ComponentModel.DataAnnotations;

namespace InvestingOak.Data.Entities.StockData
{
    public class NewsArticle
    {
        [Key]
        public int ArticleId { get; set; }

        public uint Id { get; set; }

        public string Category { get; set; }

        public uint DateTime { get; set; }

        public string Headline { get; set; }

        public string ImageUrl { get; set; }

        public string Related { get; set; }

        public string Source { get; set; }

        public string Summary { get; set; }

        public string Url { get; set; }
    }
}
