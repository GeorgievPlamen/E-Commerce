using AutoMapper;
using Products.BLL.DTO;
using Products.DAL.Entities;

namespace Products.BLL.Mappers;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<ProductAddRequest, Product>().ReverseMap();
        CreateMap<ProductUpdateRequest, Product>().ReverseMap();
        CreateMap<Product, ProductResponse>().ReverseMap();
    }
}