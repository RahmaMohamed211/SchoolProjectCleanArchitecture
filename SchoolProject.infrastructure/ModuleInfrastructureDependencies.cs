using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Data.Entities.Views;
using SchoolProject.infrastructure.Abstract;
using SchoolProject.infrastructure.Abstract.Functions;
using SchoolProject.infrastructure.Abstract.Procedures;
using SchoolProject.infrastructure.Abstract.Views;
using SchoolProject.infrastructure.InfastructureBases;
using SchoolProject.infrastructure.Repositieries;
using SchoolProject.infrastructure.Repositieries.Functions;
using SchoolProject.infrastructure.Repositieries.Procedures;
using SchoolProject.infrastructure.Repositieries.Views;

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
            services.AddTransient<IRefershTokenRepository, RefershTokenRepository>();

            //views
            services.AddTransient<IViewRepository<ViewDepartment>, ViewDepartmentRepository>();

            //procedures
            services.AddTransient<IDepartmentStudentCountProcRepository, DepartmentStudentCountProcRepository>();

            //functions
            services.AddTransient<IInstructorFunctionsRepository, InstructorFunctionsRepository>();


            services.AddTransient(typeof(IGenericRepositoryAsync<>), (typeof(GenericRepositoryAsync<>)));
            return services;
        }
    }
}
