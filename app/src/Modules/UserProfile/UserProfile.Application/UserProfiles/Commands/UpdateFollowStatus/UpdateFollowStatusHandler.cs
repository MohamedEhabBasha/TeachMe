
using Application.Exceptions;

namespace UserProfile.Application.UserProfiles.Commands.UpdateFollowStatus;

public class UpdateFollowStatusHandler(IUserProfileUnitOfWork unitOfWork) : ICommandHandler<UpdateFollowStatusCommand, UpdateFollowStatusResult>
{
    public async Task<UpdateFollowStatusResult> Handle(UpdateFollowStatusCommand command, CancellationToken cancellationToken)
    {
        var followDto = command.FollowDto;

        var instructor = await unitOfWork.UserProfileRepository
            .GetUserProfileWithFollowersAsync(followDto.InstructorId, cancellationToken) 
            ?? throw new UserProfileNotFoundException(followDto.InstructorId);

        var studentId = new UserProfileId(followDto.StudentId);

        var student = instructor.UserFollowers
            .FirstOrDefault(i => i.StudentId == studentId);

        if (student is null)
        {
            instructor.Follow(studentId);
        }else
        {
            instructor.UnFollow(studentId);
        }

        return new UpdateFollowStatusResult(await unitOfWork.CommitAsync(cancellationToken) > 0);
    }
}
