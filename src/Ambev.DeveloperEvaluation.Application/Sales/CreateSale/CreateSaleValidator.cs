using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleCommand that defines validation rules for sale creation command.
/// </summary>
public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleCommand with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - SaleDetails: Required, must be between 3 and 200 characters
    /// - Items: Must contain at least one item
    /// </remarks>
    public CreateSaleValidator()
    {
        RuleFor(sale => sale.CustomerId)
            .NotEmpty()
            .WithMessage("Customer ID is required");

        RuleFor(sale => sale.SaleNumber)
            .NotEmpty()
            .WithMessage("SaleNumber is required");

        RuleFor(sale => sale.Branch)
            .NotEmpty()
            .WithMessage("Branch is required");

        RuleFor(sale => sale.Items)
            .NotEmpty()
            .Must(items => items.Count > 0)
            .WithMessage("Sale should have almos one item");
    }
}
