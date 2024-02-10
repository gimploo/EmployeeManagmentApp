using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace EmployeeManagementApp.Filters {
    public class EMA_Auth : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedObjectResult(
                    "Your not authorized for this api call"
                );
                return;
            }

            // Check if the user has the required roles
            if (!string.IsNullOrEmpty(Roles) && !context.HttpContext.User.IsInRole(Roles))
            {
                // If user does not have the required roles, return forbidden
                context.Result = new ForbidResult();
            }
        }
    }

}