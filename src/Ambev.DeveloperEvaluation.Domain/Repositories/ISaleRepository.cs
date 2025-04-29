using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Interface for managing Sales in the persistence layer.
    /// </summary>
    public interface ISaleRepository
    {
        /// <summary>
        /// Retrieves a Sale by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the Sale.</param>
        /// <returns>The Sale if found; otherwise, null or an exception depending on implementation.</returns>
        Task<Sale> GetByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all Sales.
        /// </summary>
        /// <returns>A collection of all Sales.</returns>
        Task<IEnumerable<Sale>> GetAllAsync();

        /// <summary>
        /// Adds a new Sale to the data store.
        /// </summary>
        /// <param name="sale">The Sale entity to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(Sale sale);

        /// <summary>
        /// Updates an existing Sale.
        /// </summary>
        /// <param name="sale">The Sale entity with updated values.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(Sale sale);

        /// <summary>
        /// Deletes a Sale by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the Sale to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(Guid id);
    }
}
