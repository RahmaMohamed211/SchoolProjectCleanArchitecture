using SchoolProject.Data.Entities;
using SchoolProject.infrastructure.InfastructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.infrastructure.Abstract
{
    public interface ISubjectRepository:IGenericRepositoryAsync<Subject>
    {
    }
}
