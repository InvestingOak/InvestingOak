using System.Threading.Tasks;
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
