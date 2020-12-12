using System.Collections.Generic;
using Xunit;

namespace Cs9CheatSheet.NullCoalescing.Operators
{
    public class Tests
    {
        [Fact]
        public void Null_coalescing_operator()
        {
            (object a, object b, object c, object d) = (new object(), null, new object(), null);

            Assert.Equal(a, a != null ? a : c); //old
            Assert.Equal(a, a ?? c); //new

            Assert.Equal(c, b != null ? b : c); //old
            Assert.Equal(c, b ?? c); //new

            Assert.Equal(c, d != null ? d : b != null ? b : c != null ? c : a); //old
            Assert.Equal(c, d ?? b ?? c ?? a); //new

            object[] array = { a, b, c, d };
            for(int i = 2; i < 4; i++)
            {
                foreach (var combination in Combinatory.Combinations(array, i))
                {
                    AssertCombination(combination);
                }
            }

            void AssertCombination(object[] combination)
            {
                switch(combination)
                {
                    case object[] array when array.Length == 2:
                        var (a, b) = (array[0], array[1]);
                        Assert.Equal(a != null ? a : b, a ?? b);
                        break;
                    case object[] array when array.Length == 3:
                        var (c, d, e) = (array[0], array[1], array[2]);
                        Assert.Equal(c != null ? c : d != null ? d : e, c ?? d ?? e);
                        break;
                    case object[] array when array.Length == 4:
                        var (f, g, h, i) = (array[0], array[1], array[2], array[3]);
                        Assert.Equal(f != null ? f : g != null ? g : h != null ? h : i, g ?? f ?? h ?? i);
                        break;
                }
            }
        }
        [Fact]
        public void Null_coalescing_assignment()
        {
            (object a1, object b1, object c1) = (new object(), null, new object());
            (object a2, object b2, object c2) = (new object(), null, new object());

            Assert.NotNull(a1); Assert.NotNull(a2);
            a1 = a1 != null ? a1 : c1; //old
            a2 ??= c2; //new
            Assert.NotNull(a1); Assert.NotNull(a2);
            Assert.NotEqual(a1, c1); Assert.NotEqual(a2, c2);

            Assert.Null(b1); Assert.Null(b2);
            b1 = b1 != null ? b1 : c1; //old
            b2 ??= c2; //new
            Assert.NotNull(b1); Assert.NotNull(b2);
            Assert.Equal(b1, c1); Assert.Equal(b2, c2);
        }
    }

    static class Combinatory
    {
        public static IEnumerable<T[]> Combinations<T>(T[] array, int size)
        {
            T[] result = new T[size];
            foreach (int[] j in Combinations(size, array.Length))
            {
                for (int i = 0; i < size; i++)
                {
                    result[i] = array[j[i]];
                }
                yield return result;
            }
        }
        private static IEnumerable<int[]> Combinations(int size, int length)
        {
            int[] result = new int[size];
            Stack<int> stack = new Stack<int>(size);
            stack.Push(0);
            while (stack.Count > 0)
            {
                int index = stack.Count - 1;
                int value = stack.Pop();
                while (value < length)
                {
                    result[index++] = value++;
                    stack.Push(value);
                    if (index != size) continue;
                    yield return (int[])result.Clone();
                    break;
                }
            }
        }
    }
}
