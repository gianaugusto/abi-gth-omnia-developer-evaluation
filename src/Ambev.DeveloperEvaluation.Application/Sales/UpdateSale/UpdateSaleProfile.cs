using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    internal class UpdateSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for UpdateSaleCommand operation
        /// </summary>
        public UpdateSaleProfile()
        {
            CreateMap<UpdateSaleCommand, Sale>()
               .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
               .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateSaleItem, SaleItem>()
                .ConstructUsing(item => new SaleItem(item.ProductId, item.Quantity, item.UnitPrice))
                .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
                .ForMember(dest => dest.Discount, opt => opt.Ignore());

            CreateMap<Sale, UpdateSaleResult>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<SaleItem, UpdateSaleItemResult>();
        }
    }
}
