using Microsoft.EntityFrameworkCore;
using SchoolProject.infrastructure.Data;
using SchoolProject.Service.Abstracts;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.infrastructure.Abstract.Functions;
using SchoolProject.infrastructure.Abstract;
using SchoolProject.infrastructure.Repositieries;
using static Azure.Core.HttpHeader;
using SchoolProject.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace SchoolProject.Service.Implementations
{
    public class InstructorService:IInstructorService
    {

        #region fields
        private readonly APPDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;
        private readonly IInstructorRepository _instructorRepository;
        private readonly IInstructorFunctionsRepository _instructorFunctionsRepository;
        #endregion
        #region ctor
        public InstructorService(APPDBContext context,IHttpContextAccessor httpContextAccessor,IFileService fileService,IInstructorRepository instructorRepository ,IInstructorFunctionsRepository instructorFunctionsRepository)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _fileService = fileService;
            _instructorRepository = instructorRepository;
            _instructorFunctionsRepository = instructorFunctionsRepository;
        }



        #endregion
        #region functions
        public async Task<decimal> GetSalarySummationOfInstructor()
        {
            decimal result = 0;
            
                result =await _instructorFunctionsRepository.GetSalarySummationOfInstructor("select * from dbo. GetInstructorData()");

            
            return  result;
        }

        public async Task<bool> IsNameArExist(string nameAr)
        {
            //check if the name is exist or not
            var instructorResult = _instructorRepository.GetTableAsTracking().Where(x => x.ENameAr.Equals(nameAr)).FirstOrDefault();
            if (instructorResult == null) return false;
            return true;
        }

        public async Task<bool> IsNameArExistExcludeSelf(string nameAr, int id)
        {
            var instructorResult = await _instructorRepository.GetTableAsTracking().Where(x => x.ENameAr.Equals(nameAr) & x.InsId!=id).FirstOrDefaultAsync();
            if (instructorResult == null) return false;
            return true;
        }

        public async Task<bool> IsNameEnExist(string nameEn)
            {
            //check if the name is exist or not
            var instructorResult = await _instructorRepository.GetTableAsTracking().Where(x => x.ENameEn.Equals(nameEn)).FirstOrDefaultAsync();
            if (instructorResult == null) return false;
            return true;
        }

        public async Task<bool> IsNameEnExistExcludeSelf(string nameEn, int id)
                {
            var instructorResult = await _instructorRepository.GetTableAsTracking().Where(x => x.ENameEn.Equals(nameEn) & x.InsId!=id).FirstOrDefaultAsync();
            if (instructorResult == null) return false;
            return true;
                }
              //  result =await _instructorFunctionsRepository.GetSalarySummationOfInstructor("select * from dbo. GetInstructorData()", cmd);

        public async Task<string> AddInstrucorAsync(Instructor instructor, IFormFile file)
        {
            var context = _httpContextAccessor.HttpContext.Request;
            var baseurl = context.Scheme + "://" + context.Host;
            var imageUrl = await _fileService.UploadImage("Instructors", file);
            switch (imageUrl)
            {
                case "FailedToUploadImage": return "FailedToUploadImage";
                case "NoImage": return "NoImage";
            }
            instructor.Image=baseurl+ imageUrl;
            try
            {
                var result = await _instructorRepository.AddAsync(instructor);
                return "Success";
            }
            catch (Exception)
            {
                return "FailedInAdd";
            }
          
           
        }

        public async Task<List<Instructor>> GetInstructorListAsync()
        {
           return await _instructorRepository.GetInstructorAsync();
        }

        public async  Task<Instructor> GetInstructorByIdAsyncwithInclude(int id)
        {
            var instructor = _instructorRepository.GetTableNoTracking()
              .Include(x => x.department)
              .Where(x => x.InsId == id)
              .FirstOrDefault();
            return instructor;
        }

        #endregion
    }
}
