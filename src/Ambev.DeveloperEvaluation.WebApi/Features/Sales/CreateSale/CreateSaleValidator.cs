﻿using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Validator for CreateUserRequest that defines validation rules for sale creation.
    /// </summary>
    public class CreateSaleValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleValidator()
        {
            RuleFor(sale => sale.SaleNumber).NotEmpty();
            RuleFor(sale => sale.Branch).NotEmpty();
            RuleFor(sale => sale.Items).NotEmpty().Must(items => items.Count > 0);
        }

    }
}
