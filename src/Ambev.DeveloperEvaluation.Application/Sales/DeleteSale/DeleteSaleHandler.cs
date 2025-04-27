namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Ambev.DeveloperEvaluation.Domain.Repositories;
    using MediatR;

    /// <summary>
    /// Handler for deleting a sale by its ID.
    /// </summary>
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
    {
        private readonly ISaleRepository _saleRepository;

        public DeleteSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<DeleteSaleResponse> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(command.Id);

            if (sale == null)
            {
                throw new KeyNotFoundException("Sale not found");
            }

            if (sale.CustomerId != command.CustomerId)
            {
                throw new UnauthorizedAccessException($"Customer {command.CustomerId} doesn't have proper rights to perform action.");
            }

            await _saleRepository.DeleteAsync(command.Id);

            return new DeleteSaleResponse { Success = true };

        }
    }
}
