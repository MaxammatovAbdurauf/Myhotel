using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyhotelApi.Database;
using MyhotelApi.Helpers.AddServiceFromAttribute;
using MyhotelApi.Objects.Options;
using StackExchange.Redis;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;
using System.Text;

namespace MyhotelApi.Extensions;

public static class ServiceCollection
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services._AddControllersAndEndpoints();
        builder.Services._AddSwaggerGen();
        builder.Services._AddCors();
        builder.Services._AddAutoDtoValidations();
        builder.Services._AddDbContext(builder.Configuration);
        builder.Services._AddJwtBearer(builder.Configuration);
        builder.Services._AddConfigurations(builder.Configuration);
        builder.Services._AddServicesViaAttribute();
        builder.Services._AddDistributedRedisCache();
        builder._AddSeriologConfig();
    }

    public static void _AddControllersAndEndpoints(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
    }

    public static void _AddCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(options =>
            {
                options.AllowAnyHeader().AllowAnyOrigin().AllowAnyOrigin().AllowAnyMethod();
            });
        });
    }

    public static void _AddDistributedRedisCache(this IServiceCollection services)
    {
        var multiplexer = ConnectionMultiplexer.Connect("localhost:6379", options =>
        {
            options.AbortOnConnectFail = false;
        });
        services.AddSingleton<IConnectionMultiplexer>(multiplexer);
    }

    public static void _AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailBody>(configuration.GetSection("EmailBody"));
        services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));
        services.Configure<JwtTokenValidationParameters>(configuration.GetSection("JwtTokenValidationParameters"));
    }

    public static void _AddJwtBearer(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("JwtTokenValidationParameters");
        var options = section.Get<JwtTokenValidationParameters>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtOptions =>
                {
                    jwtOptions.SaveToken = true;
                    jwtOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = options.ValidIssuer,
                        ValidAudience = options.ValidAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.IssuerSigningKey!))
                    };
                });
    }

    public static void _AddAutoDtoValidations(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(Program)));
        services.AddFluentValidationAutoValidation(options =>
        {
            options.DisableDataAnnotationsValidation = true;
        });
    }

    public static void _AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Database"));
            options.UseLazyLoadingProxies(true);
            //options.EnableSensitiveDataLogging();
        });
    }

    public static void _AddSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Description = "Enter given token like this: Bearer <your_token>",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                    Reference = new OpenApiReference
                        {
                            Id="Bearer",
                            Type = ReferenceType.SecurityScheme

                        },
                    },
                    new List<string>()
                }
            });

        });
    }
}