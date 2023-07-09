namespace Tracker.Api.Services;

public class CorsService : IServiceInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        services.AddCors(policy =>
            {
                policy.AddPolicy("OpenCorsPolicy",
                    options => options.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithExposedHeaders("RefreshToken"));
            }
        );
    }
}
