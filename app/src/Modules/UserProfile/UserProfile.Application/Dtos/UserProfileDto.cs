namespace UserProfile.Application.Dtos;

public record UserProfileDto
(
    Guid Id,
    string? Name,
    string? Role,
    string Introduction,
    string Description,
    string? Url,
    string? PublicId,
    List<CategoryDto> Categories,
    int UserFollowsCount = 0
);
