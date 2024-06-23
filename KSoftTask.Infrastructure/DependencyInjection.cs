using KSoftTask.Application.Interfaces;
using KSoftTask.Domain.Entities;
using KSoftTask.Infrastructure.Data;
using KSoftTask.Infrastructure.Identity;
using KSoftTask.Infrastructure.Interfaces;
using KSoftTask.Infrastructure.Repositories;
using KSoftTask.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace KSoftTask.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            SeedData.Initialize(services.BuildServiceProvider().GetService<ApplicationDbContext>());

            services.ServicesInit();
            services.RepositoriesInit();
            services.ConfigureAuthenticationInit(configuration);

            return services;
        }

        private static void ServicesInit(this IServiceCollection services)
        {
            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            services.AddScoped<ITokenService, TokenService>();
        }

        private static void RepositoriesInit(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<Author>, AuthorRepository>();
            services.AddScoped<IBaseRepository<Book>, BookRepository>();
            services.AddScoped<IBaseRepository<Publisher>, PublisherRepository>();
            services.AddScoped<IBaseRepository<AuthorBook>, AuthorBookRepository>();
            services.AddScoped<IBaseRepository<PublisherBook>, PublisherBookRepository>();
        }

        private static void ConfigureAuthenticationInit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         ValidIssuer = configuration["Jwt:Issuer"]!,
                         ValidAudience = configuration["Jwt:Audience"]!,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
                         ClockSkew = TimeSpan.Zero
                     };
                 });

            services.AddAuthorization(options => options.DefaultPolicy =
            new AuthorizationPolicyBuilder
                    (JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            });

            services.AddIdentity<ApplicationUser, IdentityRole<long>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "TeamMapWeb", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                  {
                      {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                          new string[] { }
                      }
                  });
            });
        }
    }
}
