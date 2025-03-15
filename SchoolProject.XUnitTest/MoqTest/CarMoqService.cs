using SchoolProject.XUnitTest.TestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.XUnitTest.MoqTest
{
    public class CarMoqService : ICarMoqService
    {
        public List<Car> CarList;
        
        public CarMoqService(List<Car> cars)
        {
            CarList=cars;   
        }
        public bool AddCar(Car car)
        {
            CarList.Add(car);
            return true;
        }

        public List<Car> GetAll()
        {
            return CarList;
        }

        public bool RemoveCar(int? id)
        {
            if(id==null) return false;
            var car =CarList.Find(x=>x.Id==id);
            if(car==null) return false;
            return CarList.Remove(car);
        }
    }
}
