﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag.Generation.Processors.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.OpenApi
{
    public static class SwaggerServiceExtension
    {
        public static IServiceCollection AddOPenApiDocumentation(this IServiceCollection services , IConfiguration configuration)
        {
            var swaggerSettings = configuration.GetSection("SwaggerSettings").Get<SwaggerSettings>();

            services.AddEndpointsApiExplorer();
            _ = services.AddOpenApiDocument((document, ServiceProvider) =>
            {
                document.PostProcess = doc =>
                {
                    doc.Info.Title = swaggerSettings.Title;
                    doc.Info.Description = swaggerSettings.Description;
                    doc.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = swaggerSettings.ContactName,
                        Email = swaggerSettings.ContactEmail,
                        Url = swaggerSettings.ContactUrl,
                    };
                    doc.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = swaggerSettings.LicenseName,
                        Url = swaggerSettings.LicenseUrl,
                    };
                };
                document.AddSecurity(JwtBearerDefaults.AuthenticationScheme, new NSwag.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter Your Bearer(jwt) token to access this API",
                    In = NSwag.OpenApiSecurityApiKeyLocation.Header,
                    Type = NSwag.OpenApiSecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT"
                });
                document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor());
                document.OperationProcessors.Add(new SwaggerGlobalAuthProcessor());
                document.OperationProcessors.Add(new TenantHeaderAttributeProcessor()); 

            });
            return services;
        }

        public static IApplicationBuilder USeOPenApiDocumentation(this IApplicationBuilder app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi(options =>
            {
                options.DefaultModelExpandDepth = -1;
                options.DocExpansion = "none";
                options.TagsSorter = "alpha";
            });
            return app;
        }
    }
}
