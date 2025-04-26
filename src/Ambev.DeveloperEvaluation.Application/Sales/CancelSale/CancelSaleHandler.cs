using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    /// <summary>
    /// Handler for canceling an existing sale.
    /// </summary>
    /// <remarks>
    /// This handler processes the <see cref="CancelSaleCommand"/> to cancel an existing sale.
    /// It retrieves the sale by its ID, marks it as canceled, and updates it in the repository.
    /// The result is a <see cref="Unit"/> indicating the operation's success.
    /// </remarks>
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, Unit>
    {
        private readonly ISaleRepository _saleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CancelSaleHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The sale repository.</param>
        public CancelSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        /// <summary>
        /// Handles the cancellation of an existing sale.
        /// </summary>
        /// <param name="request">The cancel sale command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the unit value.</returns>
        public async Task<Unit> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId);

            if (sale == null)
            {
                throw new Exception("Sale not found");
            }

            sale.CancelSale();

            await _saleRepository.UpdateAsync(sale);

            return Unit.Value;
        }
    }
}
