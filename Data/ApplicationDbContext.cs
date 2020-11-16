using IdentityServer4.EntityFramework.Options;
using InvestingOak.Data.Entities;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace InvestingOak.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<SymbolList> SymbolLists { get; set; }

        public DbSet<Quote> Quotes { get; set; }

        public DbSet<CompanyProfile> CompanyProfiles { get; set; }

        public DbSet<Recommendations> Recommendations { get; set; }

        public DbSet<PriceTargets> PriceTargets { get; set; }

        public DbSet<Sentiment> Sentiments { get; set; }

        public DbSet<ArticleList> NewsArticles { get; set; }

        public DbSet<BalanceSheet> BalanceSheets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ArticleList>().HasKey(a => new {a.Category, a.Symbol});

            base.OnModelCreating(builder);
        }
    }
}
