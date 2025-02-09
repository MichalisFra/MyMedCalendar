using Microsoft.EntityFrameworkCore;
using MyMedCalendar.Data;
using MyMedCalendar.Models;
using System.Linq.Expressions;

namespace MyMedCalendar.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        /// <summary>
        /// The database context.
        /// </summary>
        protected readonly AppDbContext _context;

        /// <summary>
        /// The DbSet corresponding to the entity type T.
        /// </summary>
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
        /// </summary>
        /// <param name="context">The database context to be used by this repository.</param>
        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Asynchronously retrieves an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>The entity found, or null if no entity is found.</returns>
        public virtual async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        /// <summary>
        /// Asynchronously retrieves all entities of type T.
        /// </summary>
        /// <returns>A collection of all entities of type T.</returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        /// <summary>
        /// Asynchronously finds entities based on a predicate.
        /// </summary>
        /// <param name="predicate">The expression to evaluate for each entity.</param>
        /// <returns>A collection of entities that match the predicate.</returns>
        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
            => await _dbSet.Where(predicate).ToListAsync();

        /// <summary>
        /// Adds a single entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        public virtual async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public virtual void Update(T entity)
        {
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.ModifiedAt = DateTime.UtcNow;
            }
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }

    
}
