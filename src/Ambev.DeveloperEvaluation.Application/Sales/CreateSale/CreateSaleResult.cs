namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Result returned after creating a new sale.
    /// </summary>
    public class CreateSaleResult
    {
        /// <summary>
        /// Unique identifier of the newly created sale.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Total amount for the item (considering quantity and discount).
        /// </summary>
        public decimal TotalAmount { get; set; }
    }
}
