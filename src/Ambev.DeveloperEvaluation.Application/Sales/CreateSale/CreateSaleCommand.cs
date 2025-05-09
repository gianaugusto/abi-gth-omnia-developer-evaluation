using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Command for creating a new sale.
    /// </summary>
    /// <remarks>
    /// This command is used to capture the required data for creating a sale,
    /// including sale details and items. It implements <see cref="IRequest{TResponse}"/>
    /// to initiate the request that returns a <see cref="CreateSaleResult"/>.
    ///
    /// The data provided in this command is validated using the
    /// <see cref="GetSaleCommandValidator"/> which extends
    /// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly
    /// populated and follow the required rules.
    /// </remarks>
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        /// <summary>
        /// Gets or sets the sale number.
        /// </summary>
        public int SaleNumber { get; set; }

        /// <summary>
        /// Gets or sets the customer.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the total sale amount.
        /// </summary>
        public decimal TotalSaleAmount { get; set; }

        /// <summary>
        /// Gets or sets the branch.
        /// </summary>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the sale items.
        /// </summary>
        public ICollection<CreateSaleItem> Items { get; set; } = [];
    }

    /// <summary>
    /// item within a sale.
    /// </summary>
    public class CreateSaleItem
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
    }

}
