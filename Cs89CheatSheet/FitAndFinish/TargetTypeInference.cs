using Xunit;

namespace Cs9CheatSheet.FitAndFinish.TargetTypeInference
{
    record MyType(int Value = 0);

    public class Tests
    {
        [Fact]
        public void Create_new_instance()
        {
            var a = new MyType(12); //old way
            MyType b = new (12); //new way
            Assert.Equal(b, a);
        }

        [Fact]
        public void Function_call()
        {
            int Double(MyType myVar) => myVar.Value * 2;

            var a = Double(new MyType(7)); //old way
            var b = Double(new (7)); //new way

            Assert.Equal(b, a);
        }

        [Fact]
        public void Property_init()
        {
            var a = new MyType() { Value = 61 }; //old way
            MyType b = new() { Value = 61 }; // new way
            Assert.Equal(b, a);
        }
    }
}
