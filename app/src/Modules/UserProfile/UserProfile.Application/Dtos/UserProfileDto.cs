namespace UserProfile.Application.Dtos;

public record UserProfileDto(Guid Id, string Introduction, string Description, List<CategoryDto> Categories, int UserFollowsCount = 0);
