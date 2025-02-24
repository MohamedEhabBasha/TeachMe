using Microsoft.AspNetCore.Mvc;
using UserProfile.Application.UserProfiles.Commands.UpdateFollowStatus;
using UserProfile.Application.UserProfiles.Commands.UpdatePhoto;
using UserProfile.Application.UserProfiles.Commands.UpdateUserProfile;
using UserProfile.Application.UserProfiles.Queries.GetFollowingInstructorsById;
using UserProfile.Application.UserProfiles.Queries.GetUserProfileById;

namespace app.API.Modules.UserProfile;

public class UserProfileController(ISender sender) : BaseController
{
    [HttpPut]
    public async Task<ActionResult<UpdateUserProfileResponse>> UpdateUserProfile(UpdateUserProfileRequest request)
    {
        var command = request.Adapt<UpdateUserProfileCommand>();

        var result = await sender.Send(command);

        var response = result.Adapt<UpdateUserProfileResponse>();

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
    [HttpPut("updateProfilePhoto")]
    public async Task<ActionResult<UpdateUserProfilePhotoResponse>> UpdateUserProfilePhoto(UpdateUserProfilePhotoRequest request)
    {
        var command = request.Adapt<UpdateUserProfilePhotoCommand>();

        var result = await sender.Send(command);

        var response = result.Adapt<UpdateUserProfilePhotoResponse>();

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
    [HttpPut("updateProfileFollowStatus")]
    public async Task<ActionResult<UpdateProfileFollowStatusResponse>> UpdateUserProfileFollowStatus(UpdateProfileFollowStatusRequest request)
    {
        var command = request.Adapt<UpdateFollowStatusCommand>();

        var result = await sender.Send(command);

        var response = result.Adapt<UpdateProfileFollowStatusResponse>();

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
    [HttpGet("userProfileById")]
    public async Task<ActionResult<GetUserProfileByIdResponse>> GetUserProfileById(GetUserProfileByIdRequest request)
    {
        var command = request.Adapt<GetUserProfileByIdQuery>();

        var result = await sender.Send(command);

        var response = result.Adapt<GetUserProfileByIdResponse>();

        return Ok(response.UserProfileDto);
    }
    [HttpGet("followingInstructors")]
    public async Task<ActionResult<GetFollowingInstructorsByIdResponse>> GetFollowingInstructorsById(GetFollowingInstructorsByIdRequest request)
    {
        //var command = request.Adapt<GetFollowingInstructorsByIdQuery>();

        var result = await sender.Send(new GetFollowingInstructorsByIdQuery(request.StudentRequest));

        //var response = result.Adapt<GetFollowingInstructorsByIdResponse>();

        return Ok(result.UserProfiles);
    }
}
