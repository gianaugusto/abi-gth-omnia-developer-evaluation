using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// Validator for UpdateSaleRequest that defines validation rules for sale update.
    /// </summary>
    public class UpdateSaleValidator : AbstractValidator<UpdateSaleRequest>
    {
        public UpdateSaleValidator()
        {
            RuleFor(sale => sale.Id).NotEmpty();
        }
    }
}
