using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    /// <summary>
    /// Command to cancel an existing sale.
    /// </summary>
    public class CancelSaleCommand : IRequest<Unit>
    {
        /// <summary>
        /// Unique identifier of the sale to be canceled.
        /// </summary>
        public Guid SaleId { get; set; }
    }
}
