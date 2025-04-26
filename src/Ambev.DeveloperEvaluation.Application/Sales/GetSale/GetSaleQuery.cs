using Ambev.DeveloperEvaluation.Application.Sales.Shared;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Query to retrieve a sale by its identifier.
    /// </summary>
    public class GetSaleQuery : IRequest<SaleDto>
    {
        /// <summary>
        /// Unique identifier of the sale to be retrieved.
        /// </summary>
        public Guid SaleId { get; set; }
    }
}
