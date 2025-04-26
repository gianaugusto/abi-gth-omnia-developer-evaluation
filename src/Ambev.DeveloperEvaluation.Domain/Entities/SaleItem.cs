using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalAmount { get; private set; }
public bool IsCancelled { get; set; }

        public SaleItem(Guid productId, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = CalculateDiscount(quantity);
            TotalAmount = CalculateTotalAmount(quantity, unitPrice, Discount);
            IsCancelled = false;
        }

        public void Cancel()
        {
            IsCancelled = true;

            // Publish ItemCancelledEvent
            DomainEvents.Raise(new ItemCancelledEvent(Id, ProductId));
        }

        private decimal CalculateDiscount(int quantity)
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

        private decimal CalculateTotalAmount(int quantity, decimal unitPrice, decimal discount)
        {
            return quantity * unitPrice * (1 - discount);
        }
    }
}
