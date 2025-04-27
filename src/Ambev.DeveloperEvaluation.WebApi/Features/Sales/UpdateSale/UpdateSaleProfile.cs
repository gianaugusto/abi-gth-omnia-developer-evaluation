using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleProfile : Profile
    {
        public UpdateSaleProfile()
        {

            CreateMap<UpdateSaleRequest, UpdateSaleCommand>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<UpdateSaleResponse, UpdateSaleCommand>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<UpdateSaleResult, UpdateSaleCommand>()
               .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<Sale, UpdateSaleResult>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<UpdateSaleItemResult, UpdateSaleItem>();

            CreateMap<UpdateSaleItemRequest, UpdateSaleItem>();
            
            CreateMap<SaleItem, UpdateSaleItemResult>();

            CreateMap<UpdateSaleResult, UpdateSaleResponse>();

            CreateMap<UpdateSaleItemResult, SaleItemResponse>();

        }
    }
}
