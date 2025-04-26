using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Shared;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.Mapping
{
    /// <summary>
    /// AutoMapper profile for mapping between sale-related DTOs and entities.
    /// </summary>
    /// <remarks>
    /// This profile defines mappings for creating, updating, and retrieving sales.
    /// It uses AutoMapper to map between <see cref="CreateSaleCommand"/> and <see cref="Sale"/>,
    /// <see cref="SaleItemDto"/> and <see cref="SaleItem"/>, and <see cref="Sale"/> and <see cref="SaleDto"/>.
    /// </remarks>
    public class SaleProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaleProfile"/> class.
        /// </summary>
        public SaleProfile()
        {
            CreateMap<CreateSaleCommand, Sale>()
                .ForMember(dest => dest.SaleDate, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.IsCancelled, opt => opt.Ignore())
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<SaleItemDto, SaleItem>()
                .ForMember(dest => dest.IsCancelled, opt => opt.Ignore());

            CreateMap<Sale, CreateSaleResult>()
                .ForMember(dest => dest.SaleId, opt => opt.MapFrom(src => src.Id));

            CreateMap<Sale, SaleDto>()
                .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.Items));
        }
    }
}
