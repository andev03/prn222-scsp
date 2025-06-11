using AutoMapper;
using BusinessObjects.Models.Entities;
using SCSP.BusinessLogic.DTOs.Commands;
using SCSP.BusinessLogic.DTOs.Queries;
using SCSP.DataAccess.Models;

namespace SCSP.BusinessLogic.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserAccount, UserAccountResponse>();
        CreateMap<UserProfile, UserProfileResponse>();

        CreateMap<RegisterUserCommand, UserAccount>()
            .ForMember(dest => dest.Guid, opt => opt.Ignore())
            .ForMember(dest => dest.IdentityId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Disabled, opt => opt.Ignore())
            .ForMember(dest => dest.UserProfile, opt => opt.Ignore());

        CreateMap<UpdateUserProfileCommand, UserProfile>()
            .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.UserGuid))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Disabled, opt => opt.Ignore())
            .ForMember(dest => dest.Gu, opt => opt.Ignore())
            .ForMember(dest => dest.AvatarUrl, opt => opt.Ignore());
    }
}