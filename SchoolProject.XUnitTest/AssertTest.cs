using FluentAssertions;
namespace SchoolProject.XUnitTest
{
    public class AssertTest
    {
        [Fact]
        public void Calculate_2_sum_3should_be_5_without_fluentAssertion()
        {
            //Arrange
            int x = 2;
            int y = 3;
            int z;

            //Act
            z = x + y;
          //Assert
          Assert.Equal(5, z);   
        }
        [Fact]
        public void Calculate_2_sum_3should_be_5_with_fluentAssertion()
        {
            //Arrange
            int x = 2;
            int y = 3;
            int z;

            //Act
            z = x + y;
            //Assert
            z.Should().Be(5,"sum 2 with 5 not equal 5");
        }
        [Fact]
        public void string_should_be_startwith_we()
        {
            
            string word = "wellcom";
            word.Should().StartWith("we");
            
            

        }
        [Fact]
        public void string_should_be_endwith_om()
        {

            string word = "wellcom";
            word.Should().EndWith("om");



        }
        [Fact]
        public void Test2()
        {
            Thread.Sleep(5000);
        }

    }
}