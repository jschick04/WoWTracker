using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tracker.Api.Services {

    public class CorsService : IServiceInstaller {

        public void InstallService(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env) {
            services.AddCors(
                policy => {
                    policy.AddPolicy(
                        "OpenCorsPolicy",
                        options =>
                            options.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .WithExposedHeaders("RefreshToken")
                    );
                }
            );
        }

    }

}