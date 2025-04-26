using Ambev.DeveloperEvaluation.Application.Sales.Shared;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Handler for retrieving a sale by its ID.
    /// </summary>
    /// <remarks>
    /// This handler processes the <see cref="GetSaleQuery"/> to retrieve a sale by its ID.
    /// It uses AutoMapper to map the <see cref="Sale"/> entity to a <see cref="SaleDto"/>.
    /// The result is a <see cref="SaleDto"/> containing the sale details.
    /// </remarks>
    public class GetSaleHandler : IRequestHandler<GetSaleQuery, SaleDto>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSaleHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The sale repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public GetSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the retrieval of a sale by its ID.
        /// </summary>
        /// <param name="request">The get sale query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the sale details.</returns>
        public async Task<SaleDto> Handle(GetSaleQuery request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId);

            if (sale == null)
            {
                throw new Exception("Sale not found");
            }

            return _mapper.Map<SaleDto>(sale);
        }
    }
}
