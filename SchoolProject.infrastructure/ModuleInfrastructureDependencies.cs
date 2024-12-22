using Microsoft.Extensions.DependencyInjection;
using SchoolProject.infrastructure.Abstract;
using SchoolProject.infrastructure.InfastructureBases;
using SchoolProject.infrastructure.Repositieries;

namespace SchoolProject.infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
           
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IInstructorRepository, InstructorRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();

            services.AddTransient(typeof(IGenericRepositoryAsync<>), (typeof(GenericRepositoryAsync<>)));
            return services;
        }
    }
}
