using AutoMapper;
using Users.Core.DTO;
using Users.Core.Entities;

namespace Users.Core.Mappers;

public class ApplicationUserMappingProfile : Profile
{
    public ApplicationUserMappingProfile()
    {
        CreateMap<ApplicationUser, AuthenticationResponse>()
            .ForMember(src => src.UserID, opt => opt.MapFrom(dest => dest.UserID))
            .ForMember(src => src.Email, opt => opt.MapFrom(dest => dest.Email))
            .ForMember(src => src.PersonName, opt => opt.MapFrom(dest => dest.PersonName))
            .ForMember(src => src.Gender, opt => opt.MapFrom(dest => dest.Gender))
            .ForMember(src => src.Gender, opt => opt.Ignore())
            .ForMember(src => src.Token, opt => opt.Ignore());

        CreateMap<RegisterRequest, ApplicationUser>()
            .ForMember(src => src.Email, opt => opt.MapFrom(dest => dest.Email))
            .ForMember(src => src.Gender, opt => opt.MapFrom(dest => dest.Gender.ToString()))
            .ForMember(src => src.Password, opt => opt.MapFrom(dest => dest.Password))
            .ForMember(src => src.PersonName, opt => opt.MapFrom(dest => dest.PersonName));
    }
}