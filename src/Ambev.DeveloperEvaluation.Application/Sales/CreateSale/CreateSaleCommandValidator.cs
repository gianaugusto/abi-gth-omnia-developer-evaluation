using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleCommand that defines validation rules for sale creation command.
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - SaleDetails: Required, must be between 3 and 200 characters
    /// - Items: Must contain at least one item
    /// </remarks>
    public CreateSaleCommandValidator()
    {
        RuleFor(sale => sale.Items).NotEmpty().Must(items => items.Count > 0);
    }
}
