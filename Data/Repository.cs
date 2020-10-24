using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestingOak.Data.Entities.PaperTrading;
using InvestingOak.Data.Entities.StockData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InvestingOak.Data
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext context;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool SaveAll()
        {
            return context.SaveChanges() > 0;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public EntityEntry AddEntity(object model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return context.Add(model);
        }

        public async Task<EntityEntry> AddEntityAsync(object model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return await context.AddAsync(model);
        }

        public EntityEntry RemoveEntity(object model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return context.Remove(model);
        }

        public SymbolList GetSymbolList(string exchange)
        {
            return context.SymbolLists.Include(s => s.Symbols).FirstOrDefault(s => s.Exchange == exchange);
        }

        public Quote GetQuote(string symbol)
        {
            return context.Quotes.Find(symbol);
        }

        public CompanyProfile GetCompanyProfile(string symbol)
        {
            return context.CompanyProfiles.Find(symbol);
        }

        public Recommendations GetRecommendations(string symbol)
        {
            return context.Recommendations.Find(symbol);
        }

        public PriceTargets GetPriceTargets(string symbol)
        {
            return context.PriceTargets.Find(symbol);
        }

        public Sentiment GetSentiment(string symbol)
        {
            return context.Sentiments.Find(symbol);
        }

        public ArticleList GetNewsArticles(string category, string symbols = "")
        {
            return context.NewsArticles.Include(s => s.Articles)
                .FirstOrDefault(a => a.Category == category && a.Symbol == symbols);
        }

        public Project GetProjectByName(string name, ApplicationUser user)
        {
            return context.Projects.FirstOrDefault(p => p.User == user && p.Name == name);
        }

        public IEnumerable<Project> GetProjects(ApplicationUser user)
        {
            return context.Projects.Where(p => p.User == user);
        }
    }
}
