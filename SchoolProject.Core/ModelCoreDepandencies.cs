using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Core.Behaviors;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.Implementations;
using System.Reflection;

namespace SchoolProject.Core
{
    public static class ModelCoreDepandencies
    {
        public static IServiceCollection AddCoreDepandencies(this IServiceCollection services)
        {
            //configratation of madiator
            services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            //configration of AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //Get validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>)); 
            return services;
        }

    }
}
