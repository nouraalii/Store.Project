using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Persistence.Data;
using Persistence.Identity;
using Services;
using Shared;
using Shared.ErrorsModels;
using Store.Project.Api.Middlewares;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Store.Project.Api.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection RegisterAllServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddBuiltInServices();
            services.AddSwaggerServices();

            services.AddInfrastructureService(configuration);
            services.AddIdentityServices();
            services.AddApplicationServices(configuration);

            services.ConfigureServices();

            services.ConfigureJwtServices(configuration);

            return services;
        }


        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {
            services.AddControllers();

            return services;
        } 
        private static IServiceCollection ConfigureJwtServices(this IServiceCollection services , IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };
            });

            return services;
        }

        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser,IdentityRole>()
                    .AddEntityFrameworkStores<StoreIdentityDbContext>();

            return services;
        }

        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        private static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(m => m.Value.Errors.Any())
                                 .Select(m => new ValidationError()
                                 {
                                     Field = m.Key,
                                     Errors = m.Value.Errors.Select(e => e.ErrorMessage)
                                 });

                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }

        public static async Task<WebApplication> ConfigureMiddlewares(this WebApplication app)
        {
            await app.InitializeDatabaseAsync();

            app.UseGlobalErrorHandling();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            return app;
        }

        private static async Task<WebApplication> InitializeDatabaseAsync(this WebApplication app)
        {
            #region Seeding
            using var Scope = app.Services.CreateScope();
            var dbInitializer = Scope.ServiceProvider.GetRequiredService<IDbInitializer>(); //Ask CLR TO create object
            await dbInitializer.InitializeAsync();
            await dbInitializer.InitializeIdentityAsync();
            #endregion

            return app;
        }

        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            return app;
        }
    }
}
