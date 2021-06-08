using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrackerApi.Data;
using TrackerApi.Library.Database;

namespace TrackerApi.Services {

    public class DbService : IServiceInstaller {

        public void InstallService(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env) {
            if (env.IsProduction()) {
                services.AddDbContext<DataContext>(
                    options => options.UseSqlServer(configuration.GetConnectionString("AuthDb"))
                );
            } else {
                services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase(new Guid().ToString()));
            }

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<DataContext>();

            services.AddTransient<ISqlDataAccess, SqlDataAccess>();
        }

    }

}