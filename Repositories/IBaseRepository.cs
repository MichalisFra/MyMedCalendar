using Microsoft.EntityFrameworkCore;
using MyMedCalendar.Data;
using System.Linq.Expressions;

/// <summary>
/// Defines a generic repository interface outlining standard CRUD operations for entities of type T.
/// </summary>
/// <typeparam name="T">The type of entity this repository will manage. Must be a class.</typeparam>
namespace MyMedCalendar.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Asynchronously retrieves an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>The entity found, or null if no entity is found.</returns>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Asynchronously retrieves all entities of type T.
        /// </summary>
        /// <returns>A collection of all entities of type T.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Asynchronously finds entities based on a specified predicate.
        /// </summary>
        /// <param name="predicate">An expression to filter entities.</param>
        /// <returns>A collection of entities that match the predicate.</returns>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Adds a single entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(T entity);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(T entity);
    }
}
