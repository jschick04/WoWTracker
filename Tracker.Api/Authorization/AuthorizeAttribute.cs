using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tracker.Api.Entities;

namespace Tracker.Api.Authorization {

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter {

        private readonly IList<Role> _roles;

        public AuthorizeAttribute(params Role[] roles) => _roles = roles ?? new Role[] { };

        public void OnAuthorization(AuthorizationFilterContext context) {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AnonymousAttribute>().Any();

            if (allowAnonymous) { return; }

            var user = (User)context.HttpContext.Items["Account"];

            if (user is null || _roles.Any() && !_roles.Contains(user.Role)) {
                context.Result = new JsonResult(new { Error = "Unauthorized" }) {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }

    }

}