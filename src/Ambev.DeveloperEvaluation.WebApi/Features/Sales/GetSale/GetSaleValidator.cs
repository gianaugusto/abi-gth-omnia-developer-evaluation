using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    /// <summary>
    /// Validator for UpdateSaleRequest that defines validation rules for sale update.
    /// </summary>
    public class GetSaleValidator : AbstractValidator<UpdateSaleRequest>
    {
        public GetSaleValidator()
        {
            RuleFor(sale => sale.Id).NotEmpty();
        }
    }
}
