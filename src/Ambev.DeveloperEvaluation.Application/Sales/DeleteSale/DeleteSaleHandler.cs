namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Ambev.DeveloperEvaluation.Application.Events;
    using Ambev.DeveloperEvaluation.Domain.Repositories;
    using MediatR;

    /// <summary>
    /// Handler for deleting a sale by its ID.
    /// </summary>
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
    {
        private readonly ISaleRepository saleRepository;
        private readonly ISaleEventProducer producer;

        public DeleteSaleHandler(ISaleRepository saleRepository, ISaleEventProducer producer)
        {
            this.saleRepository = saleRepository;
            this.producer = producer;
        }

        public async Task<DeleteSaleResponse> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
        {
            var sale = await saleRepository.GetByIdAsync(command.Id);

            if (sale == null)
            {
                throw new KeyNotFoundException("Sale not found");
            }

            if (sale.CustomerId != command.CustomerId)
            {
                throw new UnauthorizedAccessException($"Customer {command.CustomerId} doesn't have proper rights to perform action.");
            }

            await saleRepository.DeleteAsync(command.Id);

            // produce event
            await this.producer.PublishSaleCancelledAsync(sale.Id);

            return new DeleteSaleResponse { Success = true };

        }
    }
}
