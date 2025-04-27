using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Validator for GetSaleCommand that defines validation rules for sale creation command.
/// </summary>
public class GetSaleValidator : AbstractValidator<GetSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the GetSaleCommand with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - SaleId: Required
    /// - RequesterId: Required
    /// </remarks>
    public GetSaleValidator()
    {
        RuleFor(sale => sale.CustomerId).NotEmpty()
            .WithMessage("Customer ID is required");

        RuleFor(sale => sale.Id).NotEmpty()
            .WithMessage("Sale ID is required");
    }
}
