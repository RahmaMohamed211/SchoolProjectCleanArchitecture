using SchoolProject.XUnitTest.TestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.XUnitTest.MoqTest
{
    public interface ICarMoqService
    {
        public bool AddCar(Car car);

        public bool RemoveCar(int? id);
        public List<Car> GetAll();
    }
}
