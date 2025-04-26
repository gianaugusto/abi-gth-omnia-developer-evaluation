using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Validator for CreateUserRequest that defines validation rules for user creation.
    /// </summary>
    public class CreateSalesRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSalesRequestValidator()
        {
            RuleFor(sale => sale.SaleNumber).NotEmpty();
            RuleFor(sale => sale.Branch).NotEmpty();
            RuleFor(sale => sale.Items).NotEmpty().Must(items => items.Count > 0);
        }
    }
}
