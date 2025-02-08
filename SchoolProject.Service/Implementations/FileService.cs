using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Implementations
{
    public class FileService:IFileService
    {

        #region fields
        private readonly IWebHostEnvironment _webHostEnvironment;

        #endregion
        #region ctor
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion
        #region functions
        public async Task<string> UploadImage(string Location, IFormFile file)
        {
            var path = _webHostEnvironment.WebRootPath+"/" +Location+"/"+"/";
            var extention=Path.GetExtension(file.FileName);
            var fileName= Guid.NewGuid().ToString().Replace("-",string.Empty)+extention;
            if (file.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = File.Create(path + fileName))
                    {
                        await file.CopyToAsync(fileStream);
                        await fileStream.FlushAsync();
                        return $"/{Location}/{fileName}";
                    }
                }
                catch (Exception ex) 
                {
                    return "FailedToUploadImage";
                }
            }
            else
            {
                return "NoImage";
            }

        }
        #endregion
    }
}
