using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
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
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The sale repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the creation of a new sale.
        /// </summary>
        /// <param name="request">The create sale command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the create sale result.</returns>
        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = _mapper.Map<Sale>(request);
            sale.SaleDate = DateTime.UtcNow;
            sale.Customer = request.Customer;
            sale.IsCancelled = false;

            await _saleRepository.AddAsync(sale);

            return _mapper.Map<CreateSaleResult>(sale);
        }
    }
}
