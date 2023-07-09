using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Tracker.Api.Data;
using Tracker.Api.Library.Database;

namespace Tracker.Api.Services;

public class DbService : IServiceInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase(new Guid().ToString()));
        }
        else
        {
            var builder = new SqlConnectionStringBuilder(configuration.GetConnectionString("AuthDb"))
            {
                ApplicationName = "WoW Tracker API"
            };

            services.AddDbContext<DataContext>(
                options => options.UseSqlServer(builder.ConnectionString)
            );
        }

        //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        //    .AddRoles<IdentityRole>()
        //    .AddEntityFrameworkStores<DataContext>();

        services.AddTransient<ISqlDataAccess, SqlDataAccess>();
    }
}
