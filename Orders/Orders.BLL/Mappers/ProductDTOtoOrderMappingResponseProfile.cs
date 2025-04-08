using AutoMapper;
using Orders.BLL.DTO;
using Orders.DAL.Entities;

namespace Orders.BLL.Mappers;

public class ProductDTOtoOrderMappingResponseProfile : Profile
{
    public ProductDTOtoOrderMappingResponseProfile()
    {
        CreateMap<ProductDTO, OrderItemResponse>()
          .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
          .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice));
    }
}