using SchoolProject.Data.Entities;
using SchoolProject.infrastructure.Abstract;
using SchoolProject.infrastructure.Repositieries;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace SchoolProject.Service.Implementations
{
    public class SubjectService : ISubjectService
    {

        #region fields
        private readonly ISubjectRepository _subjectRepository;
        private readonly IInstructorSubjectRepository _instructorSubjectRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly IStudentSubjectRepository _studentSubjectRepository;
        private readonly IStudentService _studentService;
        private readonly IDepartmentRepository _departmentRepository;
        #endregion
        #region ctor
        public SubjectService(ISubjectRepository subjectRepository,IInstructorSubjectRepository instructorSubjectRepository,IInstructorRepository instructorRepository,IStudentSubjectRepository studentSubjectRepository,IStudentService studentService,IDepartmentRepository departmentRepository)
        {
            _subjectRepository = subjectRepository;
            _instructorSubjectRepository = instructorSubjectRepository;
            _instructorRepository = instructorRepository;
            _studentSubjectRepository = studentSubjectRepository;
            _studentService = studentService;
            _departmentRepository = departmentRepository;
        }
        #endregion

        #region funtion
        public async Task<string> AddSubject(Subject subject)
        {
          
         var result= await  _subjectRepository.AddAsync(subject);
            return "Success";
        }

        public async Task<string> AddsubjectToInstructor(int InsId, int SubjId)
        {
            //get instructor
            var Instructor = await _instructorRepository.GetByIdAsync(InsId);
            //student is null
            if (Instructor == null)
                return "InstructorNotFound";
            //get subject
            var subject = await _subjectRepository.GetByIdWithStudents(SubjId);
            //is null
            if (subject == null) return "SubjectNotFound";
            //check if exist
            var IsExist = subject.Ins_Subjects.Any(e => e.InsId == InsId);
            if (IsExist) return "AlreadyExsists";
            //added
            var InsSubject = new Ins_Subject()
            {
                InsId = InsId,  
                SubId = SubjId,
            };
            var result = await _instructorSubjectRepository.AddAsync(InsSubject);
            return "Success";
        }

        public async Task<string> AddsubjectToStudent(StudentSubject studSubject)
        {
           //get student
           var student =await _studentService.GetByIDAsync(studSubject.StudID);
            //student is null
            if (student == null)
                return "StudentNotFound";
            //get subject
            var subject= await _subjectRepository.GetByIdWithStudents(studSubject.SubID);
            //is null
            if (subject == null) return "SubjectNotFound";
            //check if exist
            var IsExist=subject.StudentSubjects.Any(e=>e.StudID== studSubject.StudID);
            if (IsExist) return "AlreadyExsists";
            //added
            var studentSubject = new StudentSubject()
            {
                StudID = studSubject.StudID,
                SubID = studSubject.SubID,
                grade= studSubject.grade,
            };
            var StudetSubject = await _studentSubjectRepository.AddAsync(studentSubject);
            return "Success";
        }

        public async Task<Subject> GetByIDAsync(int id)
        {
           return await  _subjectRepository.GetByIdAsync(id);
        }

        public async Task<bool> IsNameArExist(string nameAr)
        {
            //check if the name is exist or not
            var subjectResult = _subjectRepository.GetTableAsTracking().Where(x => x.SubjectNameAr.Equals(nameAr)).FirstOrDefault();
            if (subjectResult == null) return false;
            return true;
        }

        public async Task<bool> IsNameEnExist(string nameEn)
        {
            //check if the name is exist or not
            var subjectResult = _subjectRepository.GetTableAsTracking().Where(x => x.SubjectNameEn.Equals(nameEn)).FirstOrDefault();
            if (subjectResult == null) return false;
            return true;
        }
        #endregion
    }
}
