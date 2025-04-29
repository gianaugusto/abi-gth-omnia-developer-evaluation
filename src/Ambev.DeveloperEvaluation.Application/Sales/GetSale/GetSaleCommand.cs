namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    using System;
    using MediatR;

    /// <summary>
    /// Command to get a sale by its ID.
    /// </summary>
    public class GetSaleCommand : IRequest<GetSaleResult>
    {
        /// <summary>
        /// Unique identifier of the sale.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Unique identifier of who are requesting.
        /// </summary>
        public Guid CustomerId { get; set; }
    }

}
