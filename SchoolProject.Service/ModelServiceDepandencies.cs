using Microsoft.Extensions.DependencyInjection;
using SchoolProject.infrastructure.Abstract;
using SchoolProject.infrastructure.Repositieries;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.Implementations;

namespace SchoolProject.Service
{
    public static class ModelServiceDepandencies
    {
        public static IServiceCollection AddServiceDepandencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IAuthenicationService, AuthenicationService>();
            return services;
        }
    }
}
