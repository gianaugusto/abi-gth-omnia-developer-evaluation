using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale transaction in the system.
    /// </summary>
    public class Sale : BaseEntity
    {
        /// <summary>
        /// Sale number for identification.
        /// </summary>
        public int SaleNumber { get; set; }

        /// <summary>
        /// Date when the sale occurred.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Name of the customer who made the purchase.
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
        public ICollection<SaleItem> Items { get; set; }

        /// <summary>
        /// Creates a new instance of a sale.
        /// </summary>
        public Sale(int saleNumber, DateTime saleDate, string customer, decimal totalSaleAmount, string branch, bool isCancelled, ICollection<SaleItem> saleItems)
        {
            SaleNumber = saleNumber;
            SaleDate = saleDate;
            Customer = customer;
            TotalSaleAmount = totalSaleAmount;
            Branch = branch;
            IsCancelled = isCancelled;
            Items = saleItems;
        }

        /// <summary>
        /// Adds an item to the sale.
        /// </summary>
        public void AddItem(SaleItem item)
        {
            Items.Add(item);
        }

        /// <summary>
        /// Cancels a specific item in the sale.
        /// </summary>
        public void CancelItem(SaleItem item)
        {
            item.IsCancelled = true;
        }

        /// <summary>
        /// Cancels the entire sale.
        /// </summary>
        public void CancelSale()
        {
            IsCancelled = true;
        }
    }
}
