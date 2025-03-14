namespace UserProfile.Application.UserProfiles.Commands.UpdateUserProfile;

public class UpdateUserProfileHandler(IUserProfileUnitOfWork unitOfWork) : ICommandHandler<UpdateUserProfileCommand, UpdateUserProfileResult>
{
    public async Task<UpdateUserProfileResult> Handle(UpdateUserProfileCommand command, CancellationToken cancellationToken)
    {
        var userProfileDto = command.UserProfileDto;

        var spec = new UserProfilesSpecification
        (
            new UserProfileSpecParams(false, false, ["category"], u => u.Id == new UserProfileId(userProfileDto.Id))
        );

        var userProfile = await unitOfWork.UserProfileRepository
            .GetEntityWithSpec(spec, cancellationToken)
            ?? throw new UserProfileNotFoundException(userProfileDto.Id);

        userProfile.Update(userProfileDto.Introduction, userProfileDto.Description);

        UpdateCategories(userProfile, userProfileDto.Categories);

        return new UpdateUserProfileResult(await unitOfWork.CommitAsync(cancellationToken) > 0);

    }

    private static void UpdateCategories(Userprofile userProfile, List<CategoryDto> newCategories)
    {
        var currentCategoriesNames = userProfile.Categories.Select(c => c.Name).ToList();

        var newCategoryNames = newCategories.Select(c => c.Name).ToList();

        var categoriesToAdd = newCategoryNames.Except(currentCategoriesNames).ToList();
        var categoriesToRemove = currentCategoriesNames.Except(newCategoryNames).ToList();

        foreach (var categoryName in categoriesToAdd)
        {
            userProfile.AddCategory(categoryName);
        }
        foreach (var categoryName in categoriesToRemove)
        {
            userProfile.RemoveCategory(categoryName);
        }
    }
}
