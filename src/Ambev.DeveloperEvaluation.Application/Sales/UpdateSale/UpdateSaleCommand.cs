using Ambev.DeveloperEvaluation.Application.Sales.Shared;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Command to update an existing sale.
    /// </summary>
    public class UpdateSaleCommand : IRequest<Unit>
    {
        /// <summary>
        /// Unique identifier of the sale to be updated.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// New total amount of the sale.
        /// </summary>
        public decimal TotalSaleAmount { get; set; }

        /// <summary>
        /// Updated branch associated with the sale.
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// Indicates whether the sale is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Updated list of items for the sale.
        /// </summary>
        public ICollection<SaleItemDto> SaleItems { get; set; }
    }
}
