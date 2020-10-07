using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InvestingOak.Data
{
    public interface IRepository
    {
        /// <summary>
        /// Automatically detects changes to this context and saves to the database.
        /// </summary>
        /// <returns>The number of changes that were saved.</returns>
        bool SaveAll();

        /// <summary>
        /// Automatically detects changes to this context and saves to the database.
        /// This is the async version of SaveAll().
        /// </summary>
        /// <returns>The number of changes that were saved.</returns>
        Task<bool> SaveAllAsync();

        /// <summary>
        /// Adds an entity to be tracked so that it can be saved to the database when SaveAll() is called.
        /// </summary>
        /// <param name="model">The entity to be tracked.</param>
        /// <returns>The entity to be tracked.</returns>
        EntityEntry AddEntity(object model);

        /// <summary>
        /// Adds an entity to be tracked so that it can be saved to the database when SaveAll() is called.
        /// This is the async version of AddEntity().
        /// </summary>
        /// <param name="model">The entity to be tracked.</param>
        /// <returns>The entity to be tracked.</returns>
        Task<EntityEntry> AddEntityAsync(object model);

        /// <summary>
        /// Marks an entity as Deleted so that it may be deleted from the database when SaveAll() is called.
        /// If the entity is already tracked then this will untrack it.
        /// </summary>
        /// <param name="model">The entity to delete.</param>
        /// <returns>The entity that was deleted.</returns>
        EntityEntry RemoveEntity(object model);
    }
}
