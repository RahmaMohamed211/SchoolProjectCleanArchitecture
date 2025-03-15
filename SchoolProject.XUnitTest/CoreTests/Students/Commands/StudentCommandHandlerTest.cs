using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Localization;
using Moq;
using SchoolProject.Core.Features.Students.Commands.Handlers;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Mapping.Students;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace SchoolProject.XUnitTest.CoreTests.Students.Commands
{
    public class StudentCommandHandlerTest
    {
        private readonly Mock<IStudentService> _studentServiceMock;
        private readonly IMapper _mapperMock;
        private readonly StudentProfile _profileMock;
        private readonly Mock<IStringLocalizer<SharedResources>> _localizerMock;
        public StudentCommandHandlerTest()
        {
            _studentServiceMock = new Mock<IStudentService>();
            _profileMock = new StudentProfile();
            var configuration = new MapperConfiguration(c => c.AddProfile(_profileMock));
            _localizerMock = new();//نفس الى فوق عادى بس دا اصدار جديد
            _mapperMock = new Mapper(configuration);
        }
        [Fact]
        public async Task Handle_AddStudent_Should_Add_Data_And_Status201()
        {
           //Arrange
            var handler= new StudentCommandHandler(_studentServiceMock.Object,_mapperMock,_localizerMock.Object);
           var addstudentCommand=new AddStudentCommand() {  Address = "Alex", DepartmentId = 1, NameAr = "رحمه", NameEn = "Rahma" };
            _studentServiceMock.Setup(x => x.AddAsync(It.IsAny<Student>())).Returns(Task.FromResult( "success"));
            //ACt
            var result =await handler.Handle(addstudentCommand, default);
            //Assert
            result.Succeeded.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.Created);
            _studentServiceMock.Verify(x => x.AddAsync(It.IsAny<Student>()),Times.Once,"Not Called");
        }
        [Fact]
        public async Task Handle_AddStudent_Should_return_Status400()
        {
            //Arrange
            var handler = new StudentCommandHandler(_studentServiceMock.Object, _mapperMock, _localizerMock.Object);
            var addstudentCommand = new AddStudentCommand() { Address = "Alex", DepartmentId = 1, NameAr = "رحمه", NameEn = "Rahma" };
            _studentServiceMock.Setup(x => x.AddAsync(It.IsAny<Student>())).Returns(Task.FromResult(""));
            //ACt
            var result = await handler.Handle(addstudentCommand, default);
            //Assert
            result.Succeeded.Should().BeFalse();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            _studentServiceMock.Verify(x => x.AddAsync(It.IsAny<Student>()), Times.Once, "Not Called");
        }
        [Fact]
        public async Task Handle_UpdateStudent_Should_return_Status404()
        {
            //Arrange
            var handler = new StudentCommandHandler(_studentServiceMock.Object, _mapperMock, _localizerMock.Object);
            var UpdatestudentCommand = new EditStudentCommand() {Id=6, Address = "Alex", DepartmentId = 1, NameAr = "رحمه", NameEn = "Rahma" };
            Student? student= null;
            int XResult = 0;
            _studentServiceMock.Setup(x => x.GetByIDAsync(UpdatestudentCommand.Id)).Returns(Task.FromResult(student)).Callback((int x) =>  XResult = x);
            //ACt
            var result = await handler.Handle(UpdatestudentCommand, default);
            //Assert
            result.Succeeded.Should().BeFalse();
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            XResult.Should().Be(6);
            Assert.Equal(XResult,UpdatestudentCommand.Id);
            _studentServiceMock.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once, "Not Called");
        }
    }
}
