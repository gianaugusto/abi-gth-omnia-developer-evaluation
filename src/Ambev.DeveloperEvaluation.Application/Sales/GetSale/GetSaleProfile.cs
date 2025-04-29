using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Profile for mapping between GetSaleCommand entity and CreateUserResponse
/// </summary>
public class GetSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetSaleCommand operation
    /// </summary>
    public GetSaleProfile()
    {
        CreateMap<GetSaleCommand, GetSaleResult>();

        CreateMap<Sale, GetSaleResult>();

        CreateMap<SaleItem, GetSaleItemResult>(); 
    }
}
