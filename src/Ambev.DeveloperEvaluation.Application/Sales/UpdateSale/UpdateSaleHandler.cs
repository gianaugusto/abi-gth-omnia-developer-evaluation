
namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    using System.Threading;
    using System.Threading.Tasks;
    using Ambev.DeveloperEvaluation.Application.Events;
    using Ambev.DeveloperEvaluation.Domain.Repositories;
    using AutoMapper;
    using FluentValidation;
    using MediatR;

    /// <summary>
    /// Handler for updating an existing sale.
    /// </summary>
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository saleRepository;
        private readonly IMapper mapper;
        private readonly ISaleEventProducer producer;

        public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper, ISaleEventProducer producer)
        {
            this.saleRepository = saleRepository;
            this.mapper = mapper;
            this.producer = producer;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await saleRepository.GetByIdAsync(command.Id);

            if (sale == null)
            {
                throw new KeyNotFoundException($"Sale {command.Id} not found");
            }

            if (sale.CustomerId != command.CustomerId)
            {
                throw new UnauthorizedAccessException($"Customer {command.CustomerId} doesn't have proper rights to perform action.");
            }

            // update values
            mapper.Map(command, sale);

            // calculate amount
            sale.CalculateTotalSaleAmount();

            // update database
            await saleRepository.UpdateAsync(sale);

            // produce event
            await this.producer.PublishSaleModifiedAsync(sale.Id);

            return this.mapper.Map<UpdateSaleResult>(sale);
        }
    }
}
