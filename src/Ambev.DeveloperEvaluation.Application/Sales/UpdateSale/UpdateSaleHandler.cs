using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Handler for updating an existing sale.
    /// </summary>
    /// <remarks>
    /// This handler processes the <see cref="UpdateSaleCommand"/> to update an existing sale.
    /// It uses AutoMapper to map the command to a <see cref="Sale"/> entity and saves it
    /// to the repository. The result is a <see cref="Unit"/> indicating the operation's success.
    /// </remarks>
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, Unit>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The sale repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the update of an existing sale.
        /// </summary>
        /// <param name="request">The update sale command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the unit value.</returns>
        public async Task<Unit> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId);

            if (sale == null)
            {
                throw new Exception("Sale not found");
            }

            _mapper.Map(request, sale);

            await _saleRepository.UpdateAsync(sale);

            return Unit.Value;
        }
    }
}
