using Ambev.DeveloperEvaluation.Application.Events;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Handler for creating a new sale.
    /// </summary>
    /// <remarks>
    /// This handler processes the <see cref="CreateSaleCommand"/> to create a new sale.
    /// It uses AutoMapper to map the command to a <see cref="Sale"/> entity and saves it
    /// to the repository. The result is a <see cref="CreateSaleResult"/> containing the
    /// ID of the newly created sale.
    /// </remarks>
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository saleRepository;
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly ISaleEventProducer producer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The sale repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="mediator">The IMediator instance.</param>
        public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IMediator mediator, ISaleEventProducer producer)
        {
            this.saleRepository = saleRepository;
            this.mapper = mapper;
            this.mediator = mediator;
            this.producer = producer;
        }

        /// <summary>
        /// Handles the creation of a new sale.
        /// </summary>
        /// <param name="command">The create sale command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the create sale result.</returns>
        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            // Validate if customer exists
            var user = await mediator.Send(new GetUserCommand(command.CustomerId), cancellationToken);

            // map on changes on request
            var sale = mapper.Map<Sale>(command);

            // calculate amount
            sale.CalculateTotalSaleAmount();

            // add to database
            await saleRepository.AddAsync(sale);

            // produce event
            await this.producer.PublishSaleCreatedAsync(sale.Id);

            return mapper.Map<CreateSaleResult>(sale);
        }
    }
}
