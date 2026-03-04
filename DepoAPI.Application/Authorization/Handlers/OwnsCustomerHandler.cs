using System.Security.Claims;
using DepoAPI.Application.Authorization.Requirements;
using DepoAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace DepoAPI.Application.Authorization.Handlers;

public class OwnsCustomerHandler: AuthorizationHandler<OwnsCustomerRequirement, Customer>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OwnsCustomerRequirement requirement,
        Customer customer)
    {
        if (customer is null)
            return Task.CompletedTask;
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var roles = context.User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
        if (roles.Contains("Admin"))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
        if(customer.OwnerId.ToString() == userId)
            context.Succeed(requirement);
        return Task.CompletedTask;
    }
}