namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    using System;
    using System.Collections.Generic;
    using MediatR;

    /// <summary>
    /// Command to update an existing sale.
    /// </summary>
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        /// <summary>
        /// Unique identifier of the sale.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Number identifying the sale.
        /// </summary>
        public int SaleNumber { get; set; }

        /// <summary>
        /// Date when the sale occurred.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Customer ID associated with the sale.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Branch where the sale was made.
        /// </summary>
        public string Branch { get; set; }  = string.Empty;

        /// <summary>
        /// Collection of items included in the sale.
        /// </summary>
        public ICollection<UpdateSaleItem> Items { get; set; } = [];
    }


    /// <summary>
    /// item within a sale.
    /// </summary>
    public class UpdateSaleItem
    {
        /// <summary>
        /// Unique identifier of the product.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Quantity of the product sold.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Unit price of the product at the time of sale.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Discount applied to the product.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Total amount for the item (considering quantity and discount).
        /// </summary>
        public decimal TotalAmount { get; set; }
    }

}
