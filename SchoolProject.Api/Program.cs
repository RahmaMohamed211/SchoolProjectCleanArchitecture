
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
namespace SchoolProject.Api
{
    public class Program
    {
        public static void Main(string[] args)
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
                .AddCoreDepandencies();


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


            var app = builder.Build();

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
            app.UseAuthorization();
            

            app.MapControllers();

            app.Run();
        }
    }
}
