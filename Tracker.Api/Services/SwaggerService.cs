using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Tracker.Api.Services {

    public class SwaggerService : IServiceInstaller {

        public void InstallService(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env) {
            services.AddSwaggerGen(
                config => {
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                    if (File.Exists(xmlPath)) { config.IncludeXmlComments(xmlPath); }

                    config.SwaggerDoc("v1", new OpenApiInfo { Title = "WoW Tracker API", Version = "v1" });

                    config.AddSecurityDefinition(
                        "Bearer",
                        new OpenApiSecurityScheme {
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.Http,
                            Scheme = "Bearer",
                            BearerFormat = "JWT",
                            Description = "To access this API use the bearer token format 'Bearer {your token here}'"
                        }
                    );

                    config.AddSecurityRequirement(
                        new OpenApiSecurityRequirement {
                            {
                                new OpenApiSecurityScheme {
                                    Reference = new OpenApiReference {
                                        Id = "Bearer",
                                        Type = ReferenceType.SecurityScheme
                                    },
                                    Name = "Bearer",
                                    In = ParameterLocation.Header,
                                    Scheme = "Bearer"
                                },
                                Array.Empty<string>()
                            }
                        }
                    );
                }
            );
        }

    }

}