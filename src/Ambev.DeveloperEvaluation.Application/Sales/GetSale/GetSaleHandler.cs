namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Ambev.DeveloperEvaluation.Domain.Repositories;
    using AutoMapper;
    using MediatR;

    /// <summary>
    /// Handler for retrieving a sale by its ID.
    /// </summary>
    public class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleResult>
    {
        private readonly ISaleRepository saleRepository;
        private readonly IMapper mapper;

        public GetSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            this.saleRepository = saleRepository;
            this.mapper = mapper;
        }

        public async Task<GetSaleResult> Handle(GetSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await saleRepository.GetByIdAsync(request.Id);

            if (sale == null)
            {
                throw new KeyNotFoundException($"Sale {request.Id} not found");
            }

            if (sale.CustomerId != request.CustomerId)
            {
                throw new UnauthorizedAccessException($"Customer {request.CustomerId} doesn't have proper rights to perform action.");
            }

            return this.mapper.Map<GetSaleResult>(sale);
        }
    }
}
