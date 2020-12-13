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

    public class Tests
    {
        [Fact]
        public void Test()
        {
            //class semantic: 2 objects are created and 3 references
            //The variables option_class_* represent the references, not the content
            var options_class_1 = new OktaOptionsClass(@"https://dev-509249.okta.com", 5);
            var options_class_2 = new OktaOptionsClass(@"https://dev-509249.okta.com", 5);
            //Reference copy
            var options_class_3 = options_class_1;

            //struct semantic: 3 objects are created and no references
            //The variables option_struct_* represent content
            var options_struct_1 = new OktaOptionsStruct(@"https://dev-509249.okta.com", 5);
            var options_struct_2 = new OktaOptionsStruct(@"https://dev-509249.okta.com", 5);
            //value copy
            var options_struct_3 = options_struct_1;

            //record semantic: as class semantic when instanciating
            var options_record_1 = new OktaOptionsNominalRecord { OktaDomain = @"https://dev-509249.okta.com", Retrials = 5 };
            var options_record_2 = new OktaOptionsNominalRecord { OktaDomain = @"https://dev-509249.okta.com", Retrials = 5 };
            //Reference copy
            var options_record_3 = options_record_1;

            //class semantic: despite pointing to identical contents, only variables _1 and _3 are compared equal
            //this is because the compiler generates reference, not content, comparison code
            Assert.NotEqual(options_class_1, options_class_2);
            Assert.NotEqual(options_class_2, options_class_3);
            Assert.Equal(options_class_1, options_class_3);
            options_class_1.Retrials = 7;
            Assert.Equal(7, options_class_3.Retrials);
            //class semantic: only content has been changed, not references, so comparisons are unchanged
            Assert.NotEqual(options_class_1, options_class_2);
            Assert.NotEqual(options_class_2, options_class_3);
            Assert.Equal(options_class_1, options_class_3);

            //struct semantic: compiler generates value comparison (no reference is created for structs)
            Assert.Equal(options_struct_1, options_struct_2);
            Assert.Equal(options_struct_2, options_struct_3);
            Assert.Equal(options_struct_1, options_struct_3);
            options_struct_1.Retrials = 7;
            //struct semantic: the variables option_struct_* represent the content
            //so the change in value is reflected in variable comparison
            Assert.NotEqual(options_struct_1, options_struct_2);
            Assert.Equal(options_struct_2, options_struct_3);
            Assert.NotEqual(options_struct_1, options_struct_3);

            //record semantic: even though the variables represent references, the compiler generates
            //value comparison code. The behavior is like struct
            Assert.Equal(options_record_1, options_record_2);
            Assert.Equal(options_record_2, options_record_3);
            Assert.Equal(options_record_1, options_record_3);
            options_record_1.Retrials = 7;
            //record semantic: after a content change, comparisons behave as class, not struct (variables are references)
            Assert.NotEqual(options_record_1, options_record_2);
            Assert.NotEqual(options_record_2, options_record_3);
            Assert.Equal(options_record_1, options_record_3);
        }
    }
}
