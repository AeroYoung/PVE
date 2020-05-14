using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using PVE.Models;

namespace PVE.Areas.Authorization
{
    public class AdministratorsAuthorizationHandler
        : AuthorizationHandler<OperationAuthorizationRequirement, PveData>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            PveData resource)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            // Administrators can do anything.
            if (context.User.IsInRole(Constants.AdministratorRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
