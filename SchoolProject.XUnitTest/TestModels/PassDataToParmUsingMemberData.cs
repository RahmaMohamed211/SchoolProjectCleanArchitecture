using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.XUnitTest.TestModels
{
    public class PassDataToParmUsingMemberData 
    {
        public static IEnumerable<object[]> GetParamData()
        {
            return new List<object[]>
            {
                new object[]{1},
                new object[]{2},
            };
         }
        public static IEnumerable<object[]> GetSecondTestData()
        {
            return new List<object[]>
            {
                new object[]{10},
                new object[]{3},
            };
        }
        //public IEnumerator<object[]> GetEnumerator()
        //{
        //    return (IEnumerator<object[]>)GetParamData();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //   return GetEnumerator();
        //}
    }
}
