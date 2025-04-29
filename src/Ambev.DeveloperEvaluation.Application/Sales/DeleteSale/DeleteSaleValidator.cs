using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    internal class DeleteSaleValidator : AbstractValidator<DeleteSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the DeleteSaleCommand with defined validation rules.
        /// </summary>
        public DeleteSaleValidator()
        {
            RuleFor(sale => sale.CustomerId).NotEmpty()
            .WithMessage("Customer ID is required");

            RuleFor(sale => sale.Id).NotEmpty()
            .WithMessage("Sale ID is required");
        }
    }

}
