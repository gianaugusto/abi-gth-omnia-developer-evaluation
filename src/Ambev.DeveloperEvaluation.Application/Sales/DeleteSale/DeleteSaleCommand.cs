namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    using System;
    using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
    using MediatR;

    /// <summary>
    /// Command to delete a sale by its ID.
    /// </summary>
    public class DeleteSaleCommand : IRequest<DeleteSaleResponse>
    {
        
        /// <summary>
        /// Unique identifier of the sale.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Unique identifier of the Requester.
        /// </summary>
        public Guid CustomerId { get; set; }
    }
}
