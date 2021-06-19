using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tracker.Api.Data;
using Tracker.Api.Services.Extensions;

namespace Tracker.Api {

    public class Startup {

        public Startup(IConfiguration configuration, IWebHostEnvironment environment) {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.InstallServicesInAssembly(Configuration, Environment);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, DataContext data) {
            if (Environment.IsDevelopment()) {
                app.UseDeveloperExceptionPage();

                app.ConfigureSwagger(Configuration);
            } else {
                data.Database.Migrate();
            }

            app.UseHttpsRedirection();
            app.UseCors("OpenCorsPolicy");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.ConfigureMiddleware();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

    }

}