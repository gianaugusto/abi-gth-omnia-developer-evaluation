namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Data transfer object representing a sale.
/// </summary>
public class UpdateSaleResult
{
    /// <summary>
    /// Unique identifier of the sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the customer.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Number identifying the sale.
    /// </summary>
    public int SaleNumber { get; set; }

    /// <summary>
    /// Date when the sale occurred.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Total amount of the sale.
    /// </summary>
    public decimal TotalSaleAmount { get; set; }

    /// <summary>
    /// Branch where the sale was made.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the sale has been cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Collection of items included in the sale.
    /// </summary>
    public ICollection<UpdateSaleItemResult> Items { get; set; } = [];
}

/// <summary>
/// Data transfer object representing an item within a sale.
/// </summary>
public class UpdateSaleItemResult
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
