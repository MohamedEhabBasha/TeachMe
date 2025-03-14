namespace UserProfile.Application.UserProfiles.Commands.UpdateFollowStatus;

public class UpdateFollowStatusHandler(IUserProfileUnitOfWork unitOfWork) : ICommandHandler<UpdateFollowStatusCommand, UpdateFollowStatusResult>
{
    public async Task<UpdateFollowStatusResult> Handle(UpdateFollowStatusCommand command, CancellationToken cancellationToken)
    {
        var followDto = command.FollowDto;

        var instructorSpec = new UserProfilesSpecification
            (
                new UserProfileSpecParams(false, false, ["userFollows"], u => u.Id == new UserProfileId(followDto.InstructorId))
            );

        var instructor = await unitOfWork.UserProfileRepository
            .GetEntityWithSpec(instructorSpec, cancellationToken)
            ?? throw new UserProfileNotFoundException(followDto.InstructorId);

        var studentSpec = new UserProfilesSpecification
        (
            new UserProfileSpecParams(false, false, ["userFollows"], u => u.Id == new UserProfileId(followDto.StudentId))
        );

        var student = await unitOfWork.UserProfileRepository
            .GetEntityWithSpec(studentSpec, cancellationToken)
            ?? throw new UserProfileNotFoundException(followDto.InstructorId);

        var studentId = new UserProfileId(followDto.StudentId);

        var isStudent = instructor.UserFollowers
            .FirstOrDefault(i => i.StudentId == studentId);

        if (isStudent is null)
        {
            instructor.Follow(studentId);
            student.Follow(new UserProfileId(followDto.InstructorId));
        }else
        {
            instructor.UnFollow(studentId);
            student.UnFollow(new UserProfileId(followDto.InstructorId));
        }

        return new UpdateFollowStatusResult(await unitOfWork.CommitAsync(cancellationToken) > 0);
    }
}
