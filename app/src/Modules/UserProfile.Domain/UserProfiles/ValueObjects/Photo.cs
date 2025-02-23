namespace UserProfile.Domain.UserProfiles.ValueObjects;

public record Photo : ValueObject
{
    public Photo() { }
    public Photo(string url, string publicId)
    {
        Url = url;
        PublicId = publicId;
    }
    public string? Url { get; private set; }
    public string? PublicId { get; private set; }

    public static Photo Of(string url, string publicId) 
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(url);
        ArgumentException.ThrowIfNullOrWhiteSpace(publicId);

        return new Photo(url, publicId);
    }

}
