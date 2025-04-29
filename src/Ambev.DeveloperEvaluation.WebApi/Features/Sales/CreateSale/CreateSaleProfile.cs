using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleRequest, CreateSaleCommand>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<CreateSaleResponse, CreateSaleCommand>();
            CreateMap<CreateSaleItemRequest, CreateSaleItem>();
            CreateMap<Sale, CreateSaleResult>();
            CreateMap<CreateSaleResult, CreateSaleResponse>();
        }
    }
}
