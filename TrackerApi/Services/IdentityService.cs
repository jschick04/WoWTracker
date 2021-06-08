using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TrackerApi.Managers;
using TrackerApi.Settings;

namespace TrackerApi.Services {

    public class IdentityService : IServiceInstaller {

        public void InstallService(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env) {
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);

            var tokenValidationParameters = new TokenValidationParameters {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddSingleton(tokenValidationParameters);

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));
            services.AddAuthentication(
                config => {
                    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            ).AddJwtBearer(
                config => {
                    config.SaveToken = true;
                    config.TokenValidationParameters = tokenValidationParameters;
                }
            );

            services.AddTransient<ITokenManager, TokenManager>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IIdentityManager, IdentityManager>();
        }

    }

}