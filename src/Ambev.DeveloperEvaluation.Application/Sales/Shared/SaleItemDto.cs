namespace Ambev.DeveloperEvaluation.Application.Sales.Shared
{
    /// <summary>
    /// Data transfer object representing an item within a sale.
    /// </summary>
    public class SaleItemDto
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
