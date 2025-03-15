using AutoMapper;
using EntityFrameworkCore.Testing.Common;
using FluentAssertions;
using Microsoft.Extensions.Localization;
using Moq;
using SchoolProject.Core.Features.Students.Queries.Handlers;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Core.Mapping.Students;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Enums;
using SchoolProject.Service.Abstracts;
using SchoolProject.XUnitTest.TestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
[assembly:CollectionBehavior(CollectionBehavior.CollectionPerAssembly,MaxParallelThreads =6)]
namespace SchoolProject.XUnitTest.CoreTests.Students.Queries
{
    public class StudentQueryHandlerTest
    {
        private readonly Mock<IStudentService> _studentServiceMock;
        private readonly IMapper _mapperMock;
        private readonly StudentProfile _profileMock;
        private readonly Mock<IStringLocalizer<SharedResources>> _localizerMock;
        public StudentQueryHandlerTest()
        {
            _studentServiceMock = new Mock<IStudentService>();
            _profileMock= new StudentProfile();
           var configuration = new MapperConfiguration(c=>c.AddProfile(_profileMock));
            _localizerMock = new();//نفس الى فوق عادى بس دا اصدار جديد
            _mapperMock=new Mapper(configuration);
        }
        [Fact]
        public async Task Handle_StudentList_Should_NotNull_And_NotEmpty()
        {
            //Arrange
            var studentList = new List<Student>()
            {
                new Student(){StudID=1,Address="Alex",DId=1,NameAr="رحمه",NameEn="Rahma"}
            };
            var query = new GetStudentListQuery();
            _studentServiceMock.Setup(x => x.GetStudentsListAsync()).Returns(Task.FromResult( studentList));
             
            var handler = new StudentQueryHandler(_studentServiceMock.Object,_mapperMock,_localizerMock.Object);
            //Act
            var result = await handler.Handle(query, default);
            //Assert
            result.Data.Should().NotBeNullOrEmpty();
            result.Succeeded.Should().BeTrue();
            result.Data.Should().BeOfType<List<GetStudentListResponse>>();
        }
        [Theory]
        //[InlineData(5)]

        [MemberData(nameof(PassDataToParmUsingMemberData.GetSecondTestData), MemberType = typeof(PassDataToParmUsingMemberData))]
        public async Task Handle_StudentById_where_student_NotFound_return_StatusCode404(int id)
        {
            //Arrange
            var department= new Department() { DId=1,DNameAr="هندسه البرمجيات",DNameEn="SE"};
            var studentList = new List<Student>()
            {
                new Student(){StudID=1,Address="Alex",DId=1,NameAr="رحمه",NameEn="Rahma",Department=department },
                new Student(){StudID=2,Address="cairo",DId=1,NameAr="محمد",NameEn="Mohamed",Department=department},
            };
            var query = new GetStudentByIDQuery(id);
            _studentServiceMock.Setup(x => x.GetStudentByIDwithIncludeAsync(id)).Returns(Task.FromResult(studentList.FirstOrDefault(x=>x.StudID==id)));

            var handler = new StudentQueryHandler(_studentServiceMock.Object, _mapperMock, _localizerMock.Object);
            //Act
            var result = await handler.Handle(query, default);
            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        [Theory]
        //[InlineData(1)]
        // [ClassData(typeof(PassDataUsingClassData))]
        [MemberData(nameof(PassDataToParmUsingMemberData.GetParamData),MemberType =typeof(PassDataToParmUsingMemberData))]
        public async Task Handle_StudentById_where_student_Found_return_StatusCode200(int id)
        {
            //Arrange
             var department = new Department() { DId = 1, DNameAr = "هندسه البرمجيات", DNameEn = "SE" };
            var studentList = new List<Student>()
            {
                new Student(){StudID=1,Address="Alex",DId=1,NameAr="رحمه",NameEn="Rahma",Department=department },
                new Student(){StudID=2,Address="cairo",DId=1,NameAr="محمد",NameEn="Mohamed",Department=department},
            };
            var query = new GetStudentByIDQuery(id);
            _studentServiceMock.Setup(x => x.GetStudentByIDwithIncludeAsync(id)).Returns(Task.FromResult(studentList.FirstOrDefault(x => x.StudID == id)));

            var handler = new StudentQueryHandler(_studentServiceMock.Object, _mapperMock, _localizerMock.Object);
            //Act
            var result = await handler.Handle(query, default);
            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Data.StudID.Should().Be(id);
            result.Data.Name.Should().Be(studentList.FirstOrDefault(x=>x.StudID==id).NameEn);
        }

        [Fact]
        public async Task Handle_StudentPaginted_Should_NotNull_And_NotEmpty()
        {
            //Arrange
            var department = new Department() { DId = 1, DNameAr = "هندسه البرمجيات", DNameEn = "SE" };
            var studentList = new AsyncEnumerable<Student>(new List<Student>
            {
                new Student(){StudID=1,Address="Alex",DId=1,NameAr="رحمه",NameEn="Rahma",Department=department}
            });
            
            var query = new GetStudentPaginatedListQuery() { 
                PageNumer=1,PageSize=10,OrderBy=StudentOrderingEnum.StudID,Search= "Rahma"
            };
            _studentServiceMock.Setup(x => x.FilterStudentPaginatedQuerable(query.OrderBy,query.Search)).Returns(studentList.AsQueryable());

            var handler = new StudentQueryHandler(_studentServiceMock.Object, _mapperMock, _localizerMock.Object);
            //Act
            var result = await handler.Handle(query, default);
            //Assert
            result.Data.Should().NotBeNullOrEmpty();
            result.Succeeded.Should().BeTrue();
            result.Data.Should().BeOfType<List<GetStudentPaginatedListResonse>>();
        }
        [Fact]
        public void Test1()
        {
            Thread.Sleep(3000);
        }
        [Fact]
        public void Test2()
        {
            Thread.Sleep(5000);
        }

    }
}
