using SchoolProject.XUnitTest.TestModels;
using SchoolProject.XUnitTest.MoqTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using System.Diagnostics;
using Moq;
namespace SchoolProject.XUnitTest
{
    public class MoqOperationTest
    {
        private readonly Mock<List<Car>> _mockMoqService=new();
        [Fact]
        public void Add_Car()
        {
            //Arrange
            var car = new Car() { Id = 2, Name = "Toyota", Color = "Red" };
            var CarMoqService = new CarMoqService(_mockMoqService.Object);
            //act
            var addResult = CarMoqService.AddCar(car);
            var Carlist = CarMoqService.GetAll();
            //_mockMoqService.Setup(x => x.AddCar(car));

            //Assert
            addResult.Should().BeTrue();
            Carlist.Should().NotBeNull(); 
           Carlist.Should().HaveCount(1);

        }
        //[Fact]
        //public void Remove_Car()
        //{
        //    //Arrange
        //    var car = new Car() { Id = 2, Name = "Toyota", Color = "Red" };
        //    //act
        //    var RemoveResult = _carMoqService.RemoveCar(2);
        //    var Carlist = _carMoqService.GetAll();
        //    //Assert
        //    Carlist.Should().HaveCount(1);

        [Fact]
        public void Test2()
        {
            Thread.Sleep(5000);
        }

    }
}
