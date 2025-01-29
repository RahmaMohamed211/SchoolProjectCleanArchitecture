using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.infrastructure.Data;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public static class ServiceRegistration
{
    public static IServiceCollection AddServiceRegistration(this IServiceCollection services,IConfiguration configuration)
    {
        
        services.AddIdentity<User, Role>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            //lockout setting
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            //user settings
            options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
        })
        .AddEntityFrameworkStores<APPDBContext>()
        .AddDefaultTokenProviders();

        // Jwt Authentication
        var jwtSettings = new JwtSettings();
        var emailSettings = new EmailSettings();
        configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
        configuration.GetSection(nameof(emailSettings)).Bind(emailSettings);
        services.AddSingleton(jwtSettings);
        services.AddSingleton(emailSettings);


        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
         .AddJwtBearer(x =>
         {
             x.RequireHttpsMetadata = false;
             x.SaveToken = true;
             x.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = jwtSettings.ValidateIssuser,
                 ValidIssuers = new[] { jwtSettings.Issuser },
                 ValidateIssuerSigningKey = jwtSettings.ValidateIssuserSigingKey,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                 ValidAudience = jwtSettings.Audience,
                 ValidateAudience = jwtSettings.ValidateAudience,
                 ValidateLifetime = jwtSettings.ValidateLifeTime,
             };
         });
        //Swagger Gn بيضيف كلام ل swagger
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "School Project", Version = "v1" });
            c.EnableAnnotations();

            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
            {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
            }
           });
        });

        services.AddAuthorization(option =>
        {
            option.AddPolicy("CreateStudent", policy =>
            {
                policy.RequireClaim("Create Student", "True");
            });
            option.AddPolicy("DeleteStudent", policy =>
            {
                policy.RequireClaim("Delete Student", "True");
            });
            option.AddPolicy("EditStudent", policy =>
            {
                policy.RequireClaim("Edit Student", "True");
            });
        });

        services.AddAuthorization(option =>
        {
            option.AddPolicy("CreateStudent", policy =>
            {
                policy.RequireClaim("Create Student", "True");
                      
            });
            option.AddPolicy("DeleteStudent", policy =>
            {
                policy.RequireClaim("Delete Student", "True");


            });
            option.AddPolicy("EditStudent", policy =>
            {
                policy.RequireClaim("Edit Student", "True");


            });
        });


        return services;
    }
}
