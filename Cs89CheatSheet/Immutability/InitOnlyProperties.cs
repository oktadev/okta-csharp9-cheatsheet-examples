using Xunit;

namespace Cs9CheatSheet.Immutability.InitOnlyProperties
{
    class OktaOptionsClass_Mutable
    {
        public string OktaDomain { get; set; }
        public int Retrials { get; set; }
    }

    class OktaOptionsClass_Immutable_Wrong
    {
        public string OktaDomain { get; }
        public int Retrials { get; }
    }

    class OktaOptionsClass_Immutable_Constructor
    {
        public string OktaDomain { get; }
        public int Retrials { get; }

        public OktaOptionsClass_Immutable_Constructor(string oktaDomain, int retrials)
        {
            OktaDomain = oktaDomain;
            Retrials = retrials;
        }
    }

    class OktaOptionsClass_Immutable_Ok_Init
    {
        public string OktaDomain { get; init; }
        public int Retrials { get; init; }
    }

    public class Tests
    {
        [Fact]
        public void Test()
        {
            var options_mutable = 
                new OktaOptionsClass_Mutable { 
                    OktaDomain = 
                    @"https://dev-509249.okta.com", Retrials = 3 };

            options_mutable.Retrials = 9458257; //properties can be set at any time

            //Compiler error, cannot set properties (at all)
            //var options_immutable_wrong = 
            //    new OktaOptionsClass_Immutable_Wrong { 
            //        OktaDomain = @"https://dev-509249.okta.com", 
            //        Retrials = 3 };

            //Ok, use constructor
            var options_immutable_ok = new OktaOptionsClass_Immutable_Constructor(@"https://dev-509249.okta.com", 3);
            //options_immutable_ok.Retrials = 9458257; //design time error: properties are readonly

            //Ok, new init-only  properties
            var options_immutable_ok_init = 
                new OktaOptionsClass_Immutable_Ok_Init { 
                    OktaDomain = @"https://dev-509249.okta.com", 
                    Retrials = 3 };
            //options_immutable_ok_init.Retrials = 9458257; //design time error: properties are readonly

        }
    }
}
