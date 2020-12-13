using System.Linq;
using Xunit;

namespace Cs9CheatSheet.PatternMatching.SwitchExpression
{
    static class ScoreEvaluator
    {
        static internal string SwitchStatement(int score)
        {
            var ret = string.Empty;
            switch(score)
            {
                case 0:
                    ret = "Unclassified";
                    break;
                case 1:
                case 2:
                case 3:
                    ret = "Bad";
                    break;
                case 4:
                case 5:
                    ret = "Ordinary";
                    break;
                case 6:
                case 7:
                    ret = "Good";
                    break;
                case 8:
                case 9:
                    ret = "Excellent";
                    break;
                case 10:
                    ret = "Outstanding";
                    break;
                default:
                    ret = "Invalid Score";
                    break;
            }

            return ret;
        }

        static internal string SwitchExpression(int score) => score switch
        {
            0 => "Unclassified",
            > 0 and <= 3 => "Bad",
            4 or 5 => "Ordinary",
            >= 6 and <= 7 => "Good",
            8 or 9 => "Excellent",
            10 => "Outstanding",
            _ => "Invalid Score"
        };
    }
    
    public class Tests
    {
        [Fact]
        public void Test()
        {
            Assert.All(
                Enumerable.Range(-1, 12), 
                i => Assert.Equal(ScoreEvaluator.SwitchExpression(i), ScoreEvaluator.SwitchStatement(i)));
        }
    }
}
