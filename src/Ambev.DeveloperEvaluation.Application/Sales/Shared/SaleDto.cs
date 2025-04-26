namespace Ambev.DeveloperEvaluation.Application.Sales.Shared
{
    /// <summary>
    /// Data transfer object representing a sale.
    /// </summary>
    public class SaleDto
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
        /// Name of the customer associated with the sale.
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// Total amount of the sale.
        /// </summary>
        public decimal TotalSaleAmount { get; set; }

        /// <summary>
        /// Branch where the sale was made.
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// Indicates whether the sale has been cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Collection of items included in the sale.
        /// </summary>
        public ICollection<SaleItemDto> SaleItems { get; set; }
    }
}
