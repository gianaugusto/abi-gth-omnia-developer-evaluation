using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCreatedEvent : INotification
    {
        public Guid SaleId { get; }
        public int SaleNumber { get; }
        public DateTime SaleDate { get; }
        public string Customer { get; }
        public string Branch { get; }

        public SaleCreatedEvent(Guid saleId, int saleNumber, DateTime saleDate, string customer, string branch)
        {
            SaleId = saleId;
            SaleNumber = saleNumber;
            SaleDate = saleDate;
            Customer = customer;
            Branch = branch;
        }
    }
}
