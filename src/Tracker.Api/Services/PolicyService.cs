using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Tracker.Api.Entities;

namespace Tracker.Api.Services;

public class PolicyService : IServiceInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        services.Configure<AuthorizationOptions>(options =>
            {
                options.AddPolicy("AdminPolicy",
                    policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim(ClaimTypes.Role, Role.Admin.ToString());
                    }
                );
            }
        );
    }
}
