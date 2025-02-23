namespace UserProfile.Domain.UserProfiles.ValueObjects;
public record UserProfileId(Guid Value) : TypedIdValueBase(Value)
{
}
