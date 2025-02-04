
using Microsoft.EntityFrameworkCore;
using SchoolProject.infrastructure.Abstract;
using SchoolProject.infrastructure.Data;
using SchoolProject.infrastructure.Repositieries;
using SchoolProject.infrastructure;
using SchoolProject.Service;
using SchoolProject.Core;
using SchoolProject.Core.Middelware;
using Microsoft.Identity.Client;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.infrastructure.Seeder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using SchoolProject.Core.Filters;
namespace SchoolProject.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //connection to sql server

            builder.Services.AddDbContext<APPDBContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext"));

            });

            #region Dependency injection
        
            builder.Services.AddInfrastructureDependencies()
                .AddServiceDepandencies()
                .AddCoreDepandencies()
                .AddServiceRegistration(builder.Configuration);


         
           


            #endregion


            #region Localization



            builder.Services.AddControllersWithViews();
            builder.Services.AddLocalization(opt =>
                {
                    opt.ResourcesPath = "";
                });

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("ar-EG"),
        new CultureInfo("fr-FR"),
        new CultureInfo("de-DE")
    };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });






            #endregion

            #region Allowcors
            var CORS = "_cors";
            builder.Services.AddCors(Options =>
            {
                Options.AddPolicy(name: CORS,
                    policy => {
                        policy.AllowAnyHeader();
                        policy.AllowAnyMethod();
                        policy.AllowAnyOrigin();
                      
                    });
                    
            });


            #endregion

            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>(); 
            builder.Services.AddTransient<IUrlHelper>(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);

            });
            builder.Services.AddTransient<AuthFilter>();
            var app = builder.Build();

            using(var scope = app.Services.CreateScope())
            {
                var userManger = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                await RoleSeeder.SeedAsync(roleManger);
                await UserSeeder.SeedAsync(userManger);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            #region Localization Middleware
            var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);




            #endregion


            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseCors(CORS);

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.MapControllers();

            app.Run();
        }
    }
}
