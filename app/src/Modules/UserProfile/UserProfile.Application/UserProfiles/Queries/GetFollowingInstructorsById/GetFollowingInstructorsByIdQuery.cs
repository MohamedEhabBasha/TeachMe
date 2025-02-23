﻿namespace UserProfile.Application.UserProfiles.Queries.GetFollowingInstructorsById;

public record GetFollowingInstructorsByIdQuery(Guid StudentId) : IQuery<GetFollowingInstructorsByIdResult>;
public record GetFollowingInstructorsByIdResult(IReadOnlyCollection<UserProfileDto> UserProfiles);
