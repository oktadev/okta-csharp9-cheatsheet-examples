using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cs9CheatSheet.FitAndFinish.TargetTypeInference
{
    record MyType(int Value = 0);

    public class Tests
    {
        [Fact]
        public void Create_new_instance()
        {
            var a = new MyType(12);
            MyType b = new(12);
            Assert.Equal(b, a);
        }

        [Fact]
        public void Function_call()
        {
            int Double(MyType myVar) => myVar.Value * 2;

            Assert.Equal(Double(new MyType(7)), Double(new(7)));
        }

        [Fact]
        public void Property_init()
        {
            var a = new MyType() { Value = 61 };
            MyType b = new() { Value = 61 };
            Assert.Equal(b, a);
        }
    }
}
