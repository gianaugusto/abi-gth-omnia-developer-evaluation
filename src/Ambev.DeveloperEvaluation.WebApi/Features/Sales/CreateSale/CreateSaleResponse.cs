namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Result returned after creating a new sale.
/// </summary>
public class CreateSaleResponse
{
    /// <summary>
    /// Unique identifier of the newly created sale.
    /// </summary>
    public Guid SaleId { get; set; }
}
