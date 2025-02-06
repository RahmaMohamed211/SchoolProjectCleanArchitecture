using SchoolProject.infrastructure.InfastructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.infrastructure.Abstract.Views
{
    public interface IViewRepository<T>:IGenericRepositoryAsync<T> where T : class
    {

    }
}
