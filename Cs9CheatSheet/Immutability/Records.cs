using System.Runtime.InteropServices;
using Xunit;

namespace Cs9CheatSheet.Immutability.Records
{
    class OktaOptionsClass
    {
        public string OktaDomain { get; set;  }
        public int Retrials { get; set; }

        public OktaOptionsClass(string oktaDomain, int retrials)
        {
            OktaDomain = oktaDomain;
            Retrials = retrials;
        }
    }

    struct OktaOptionsStruct
    {
        public string OktaDomain { get; set; }
        public int Retrials { get; set; }

        public OktaOptionsStruct(string oktaDomain, int retrials)
        {
            OktaDomain = oktaDomain;
            Retrials = retrials;
        }
    }

    record OktaOptionsNominalRecord
    {
        public string OktaDomain { get; set; }
        public int Retrials { get; set; }
    }

    record OktaOptionsPositionalRecord(string OktaDomain, int Retrials);

    public class Tests
    {
        [Fact]
        public void Test()
        {
            var options_class_1 = new OktaOptionsClass(@"https://dev-509249.okta.com", 5);
            var options_class_2 = new OktaOptionsClass(@"https://dev-509249.okta.com", 5);
            var options_class_3 = options_class_1;

            var options_struct_1 = new OktaOptionsStruct(@"https://dev-509249.okta.com", 5);
            var options_struct_2 = new OktaOptionsStruct(@"https://dev-509249.okta.com", 5);
            var options_struct_3 = options_struct_1;
            
            var options_record_1 = new OktaOptionsNominalRecord { OktaDomain = @"https://dev-509249.okta.com", Retrials = 5 };
            var options_record_2 = new OktaOptionsNominalRecord { OktaDomain = @"https://dev-509249.okta.com", Retrials = 5 };
            var options_record_3 = options_record_1;

            var options_positional = new OktaOptionsPositionalRecord(@"https://dev-509249.okta.com", 5);

            Assert.NotEqual(options_class_1, options_class_2);
            Assert.NotEqual(options_class_2, options_class_3);
            Assert.Equal(options_class_1, options_class_3);
            options_class_1.Retrials = 7;
            Assert.Equal(7, options_class_3.Retrials);
            Assert.NotEqual(options_class_1, options_class_2);
            Assert.NotEqual(options_class_2, options_class_3);
            Assert.Equal(options_class_1, options_class_3);

            Assert.Equal(options_struct_1, options_struct_2);
            Assert.Equal(options_struct_2, options_struct_3);
            Assert.Equal(options_struct_1, options_struct_3);
            options_struct_1.Retrials = 7;
            Assert.NotEqual(options_struct_1, options_struct_2);
            Assert.Equal(options_struct_2, options_struct_3);
            Assert.NotEqual(options_struct_1, options_struct_3);

            Assert.Equal(options_record_1, options_record_2);
            Assert.Equal(options_record_2, options_record_3);
            Assert.Equal(options_record_1, options_record_3);
            options_record_1.Retrials = 7;
            Assert.NotEqual(options_record_1, options_record_2);
            Assert.NotEqual(options_record_2, options_record_3);
            Assert.Equal(options_record_1, options_record_3);
        }


    }
}
