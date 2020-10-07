using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace InvestingOak.Data
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public Repository(ApplicationDbContext context, ILogger<Repository> logger, IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
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
            return context.Add(model);
        }

        public async Task<EntityEntry> AddEntityAsync(object model)
        {
            return await context.AddAsync(model);
        }
        
        public EntityEntry RemoveEntity(object model)
        {
            return context.Remove(model);
        }
    }
}
