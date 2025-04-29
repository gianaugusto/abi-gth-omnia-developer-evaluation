namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

using AutoMapper;

/// <summary>
/// Profile for mapping DeleteUser feature requests to commands
/// </summary>
public class DeleteSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteUser feature
    /// </summary>
    public DeleteSaleProfile()
    {
        CreateMap<Guid, DeleteSaleRequest>();
    }
}