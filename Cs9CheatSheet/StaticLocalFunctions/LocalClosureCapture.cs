using Xunit;

namespace Cs9CheatSheet.StaticLocalFunctions.LocalClosureCapture
{
    //class Sample
    //{
    //    internal static (int, int) Method()
    //    {
    //        int x = 1, y = 2, z = 0, w = 0;

    //        CalculateWithClosure();
    //        w = CalculateWithoutClosure(x, y);

    //        return (z, w);

    //        void CalculateWithClosure()
    //        {
    //            z = x + y;
    //        }

    //        //static: trying to use x, y, z or w will cause a compiler error
    //        static int CalculateWithoutClosure(int a, int b)
    //        {
    //            return a + b;
    //        }
    //    }
    //}

    public class Tests
    {
        [Fact]
        public void Test()
        {
            int x = 1;

            int AddWithCapture(int a, int b)
            {
                x = 5;
                return a + b;
            }

            //static: trying to use x, y, z or w will cause a compiler error
            static int AddWithoutCapture(int a, int b)
            {
                //x = 5; // Error
                return a + b;
            }


            //static local functions CANNOT change external in-scope locals (x here)
            Assert.Equal(2, AddWithoutCapture(x, 1));
            //No side effects
            Assert.Equal(2, x + 1);

            //Non static local functions CAN change external in-scope locals (x here)
            Assert.Equal(2, AddWithCapture(x, 1));
            //Here the side effect is to change the result of a test with the same inputs
            Assert.NotEqual(2, x + 1);

        }
    }
}
