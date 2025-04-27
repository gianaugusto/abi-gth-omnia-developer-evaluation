using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents an individual item within a sale transaction, including pricing, quantity, discounts, and cancellation state.
    /// </summary>
    public class SaleItem : BaseEntity
    {
        /// <summary>
        /// Gets the unique identifier of the associated Sale.
        /// </summary>
        public Guid SaleId { get; private set; }

        /// <summary>
        /// Gets the unique identifier of the associated product.
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Gets the quantity of the product sold.
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// Gets the unit price of the product at the time of sale.
        /// </summary>
        public decimal UnitPrice { get; private set; }

        /// <summary>
        /// Gets the discount rate applied based on quantity.
        /// </summary>
        public decimal Discount { get; private set; }

        /// <summary>
        /// Gets the total amount for the sale item after applying discounts.
        /// </summary>
        public decimal TotalAmount { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sale item has been cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaleItem"/> class with specified product details.
        /// </summary>
        /// <param name="productId">The unique identifier of the product.</param>
        /// <param name="quantity">The quantity of product purchased.</param>
        /// <param name="unitPrice">The unit price of the product.</param>
        public SaleItem(Guid productId, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = CalculateDiscount(quantity);
            TotalAmount = CalculateTotalAmount(quantity, unitPrice, Discount);
            IsCancelled = false;
        }

        /// <summary>
        /// Marks the sale item as cancelled and raises a domain event.
        /// </summary>
        public void Cancel()
        {
            IsCancelled = true;
        }

        /// <summary>
        /// Calculates the discount based on the quantity purchased.
        /// </summary>
        /// <param name="quantity">The quantity purchased.</param>
        /// <returns>The discount rate as a decimal.</returns>
        private static decimal CalculateDiscount(int quantity)
        {
            if (quantity >= 20)
            {
                return 0.20m;
            }
            else if (quantity >= 10)
            {
                return 0.10m;
            }
            else if (quantity >= 4)
            {
                return 0.10m;
            }
            return 0;
        }

        /// <summary>
        /// Calculates the total amount after applying the discount.
        /// </summary>
        /// <param name="quantity">The quantity purchased.</param>
        /// <param name="unitPrice">The price per unit.</param>
        /// <param name="discount">The discount rate.</param>
        /// <returns>The total calculated amount.</returns>
        private static decimal CalculateTotalAmount(int quantity, decimal unitPrice, decimal discount)
        {
            return quantity * unitPrice * (1 - discount);
        }
    }
}
