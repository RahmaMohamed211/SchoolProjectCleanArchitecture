using EntityFrameworkCore.Testing.Common;
using FluentAssertions;
using Moq;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.XUnitTest.Wrappers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.XUnitTest.ServicesTest.ExtentionMethod
{
    public class ExtentionMethodTest
    {
        private readonly Mock<IPaginatedService<Student>> _PaginatedServiceMock;
        public ExtentionMethodTest()
        {
            _PaginatedServiceMock= new Mock<IPaginatedService<Student>>();
        }
        [Theory]
        [InlineData(1,10)]
        public async Task ToPaginatedListAsync_Should_Return_List(int pageNumber,int PageSize)
        {
            //Arrange 
            var department = new Department() { DId = 1, DNameAr = "هندسه البرمجيات", DNameEn = "SE" };
            var studentList = new AsyncEnumerable<Student>(new List<Student>
            {
                new Student(){StudID=1,Address="Alex",DId=1,NameAr="رحمه",NameEn="Rahma",Department=department}
            });
            var Paginatedresult = new PaginatedResult<Student>(studentList.ToList());
            _PaginatedServiceMock.Setup(x=>x.ReturnPaginatedResult(studentList, pageNumber, PageSize)).Returns(Task.FromResult(Paginatedresult));
            //Act
            var result = await _PaginatedServiceMock.Object.ReturnPaginatedResult(studentList, pageNumber, PageSize);
            //Assert
            result.Data.Should().NotBeNullOrEmpty();
        }
    }
}
