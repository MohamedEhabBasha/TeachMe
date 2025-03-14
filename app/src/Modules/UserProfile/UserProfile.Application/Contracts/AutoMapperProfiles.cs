namespace UserProfile.Application.Contracts;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Domain.UserProfiles.UserProfile, UserProfileDto>()
            .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id.Value))
            .ForCtorParam("Categories", opt => opt.MapFrom(src => src.Categories.Select(c => new CategoryDto(c.Name)).ToList()))
            .ForCtorParam("Url", opt => opt.MapFrom(src => src.Photo.Url))
            .ForCtorParam("PublicId", opt => opt.MapFrom(src => src.Photo.PublicId))
            .ForCtorParam("UserFollowsCount", opt => opt.MapFrom(src => src.UserFollowers.Count));
    }
}
