
using DepoAPI.Application.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace DepoAPI.Application.Authorization.Handlers;

public class MinimumAgeHandler: AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        MinimumAgeRequirement requirement)
    {
        var dateOfBirthClaim = context.User.FindFirst("DateOfBirth");
        if (dateOfBirthClaim is null)
            return Task.CompletedTask;
        if(!DateTime.TryParse(dateOfBirthClaim.Value, out var dateOfBirth))
            return Task.CompletedTask;
        var userAge = DateTime.Today.Year - dateOfBirth.Year;
        //Burada ay ve gün kontrol ediliyor, eğer doğduğu yılın tarihi şu anki tarihten daha öndeyse yaş 1 yıl azaltılıyor.
        if (dateOfBirth.Date > DateTime.Today.AddYears(-userAge))
            userAge--;
        if(userAge >= requirement.MinimumAge)
            context.Succeed(requirement);
        return Task.CompletedTask;
    }
}