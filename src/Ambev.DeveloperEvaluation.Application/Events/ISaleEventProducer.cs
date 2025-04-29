namespace Ambev.DeveloperEvaluation.Application.Events
{
    /// <summary>
    /// Defines an event publisher interface for sale-related domain events.
    /// Implementations are responsible for emitting events to external systems or logging mechanisms.
    /// </summary>
    public interface ISaleEventProducer
    {
        /// <summary>
        /// Publishes an event indicating a sale has been created.
        /// </summary>
        /// <param name="saleId">The unique identifier of the created sale.</param>
        Task PublishSaleCreatedAsync(Guid saleId);

        /// <summary>
        /// Publishes an event indicating a sale has been modified.
        /// </summary>
        /// <param name="saleId">The unique identifier of the modified sale.</param>
        Task PublishSaleModifiedAsync(Guid saleId);

        /// <summary>
        /// Publishes an event indicating a sale has been cancelled.
        /// </summary>
        /// <param name="saleId">The unique identifier of the cancelled sale.</param>
        Task PublishSaleCancelledAsync(Guid saleId);

        /// <summary>
        /// Publishes an event indicating an item within a sale has been cancelled.
        /// </summary>
        /// <param name="saleId">The unique identifier of the sale containing the cancelled item.</param>
        /// <param name="itemId">The unique identifier of the cancelled item.</param>
        Task PublishItemCancelledAsync(Guid saleId, Guid itemId);
    }
}
