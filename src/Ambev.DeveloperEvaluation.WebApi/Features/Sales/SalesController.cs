namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    using System;
    using System.Threading.Tasks;
    using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
    using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
    using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
    using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
    using Ambev.DeveloperEvaluation.WebApi.Common;
    using Ambev.DeveloperEvaluation.WebApi.Extensions;
    using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
    using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
    using Asp.Versioning;
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SalesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateSaleCommand>(request);
            command.CustomerId = HttpContext.User.GetCustomerId();

            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
            {
                Success = true,
                Message = "Sales created successfully",
                Data = _mapper.Map<CreateSaleResponse>(response)
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSale(Guid id)
        {
            var command = new GetSaleCommand { Id = id, CustomerId = HttpContext.User.GetCustomerId() };
            var result = await _mediator.Send(command);

            return Ok(new ApiResponseWithData<GetSaleResult>
            {
                Success = true,
                Message = "Sale retrieved successfully",
                Data = _mapper.Map<GetSaleResult>(result)
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale(Guid id, [FromBody] UpdateSaleRequest request)
        {
            var command = _mapper.Map<UpdateSaleCommand>(request);
            command.Id = id;
            command.CustomerId = HttpContext.User.GetCustomerId();

            var result = await _mediator.Send(command);

            return Ok(new ApiResponseWithData<UpdateSaleResponse>
            {
                Success = true,
                Message = "Sale updated successfully",
                Data = _mapper.Map<UpdateSaleResponse>(result)
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(Guid id)
        {
            var command = new DeleteSaleCommand { Id = id, CustomerId = HttpContext.User.GetCustomerId() };

            await _mediator.Send(command);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Sale deleted successfully"
            });
        }
    }
}
