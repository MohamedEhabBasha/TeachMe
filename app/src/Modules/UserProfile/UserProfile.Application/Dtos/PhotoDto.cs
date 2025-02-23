using Microsoft.AspNetCore.Http;

namespace UserProfile.Application.Dtos;

public record PhotoDto(Guid Id, IFormFile File);