using Serilog;

namespace Ambev.DeveloperEvaluation.Application.Events
{
    /// <summary>
    /// Publishes sale-related events using Serilog for logging purposes.
    /// </summary>
    public class SaleEventProducer : ISaleEventProducer
    {
        private readonly ILogger _logger;

        public SaleEventProducer()
        {
            _logger = Log.ForContext<SaleEventProducer>();
        }

        public Task PublishSaleCreatedAsync(Guid saleId)
        {
            _logger.Information("SaleCreated event published for Sale ID: {SaleId}", saleId);
            return Task.CompletedTask;
        }

        public Task PublishSaleModifiedAsync(Guid saleId)
        {
            _logger.Information("SaleModified event published for Sale ID: {SaleId}", saleId);
            return Task.CompletedTask;
        }

        public Task PublishSaleCancelledAsync(Guid saleId)
        {
            _logger.Information("SaleCancelled event published for Sale ID: {SaleId}", saleId);
            return Task.CompletedTask;
        }

        public Task PublishItemCancelledAsync(Guid saleId, Guid itemId)
        {
            _logger.Information("ItemCancelled event published for Sale ID: {SaleId}, Item ID: {ItemId}", saleId, itemId);
            return Task.CompletedTask;
        }
    }
}
