namespace Cs9CheatSheet.IndicesAndRanges.Slicing
{
    using System;
    using System.Linq;
    using Xunit;
    using static TheCollection;

    internal class TheCollection
    {
        static int milliseconds = DateTime.Now.Millisecond;
        public static Random Rand = new Random(milliseconds);
        public static int[] a = Enumerable.Range(0, Rand.Next(100, 1000)).Select(i => Rand.Next()).ToArray();
    }

    public class Tests
    {
        [Fact]
        public void Past_end()
        {
            Assert.Throws<IndexOutOfRangeException>(() => a[a.Length]);
            Assert.Throws<IndexOutOfRangeException>(() => a[^0]);
        }
        [Fact]
        public void First_element()
        {
            Assert.Equal(a[0], a[^a.Length]);
        }
        [Fact]
        public void Last_element()
        {
            Assert.Equal(a[a.Length - 1], a[^1]);
        }
        [Fact]
        public void First_15()
        {
            Assert.Equal(a.Take(15), a[..15]);
        }
        [Fact]
        public void Last_27()
        {
            Assert.Equal(a.Skip(a.Length - 27).Take(27), a[^27..]);
        }
        [Fact]
        public void From_11_to_43()
        {
            Assert.Equal(a.Skip(11).Take(32), a[11..43]);
        }
        [Fact]
        public void From_37_to_6_back()
        {
            Assert.Equal(a.Skip(a.Length - 37).Take(37 - 6), a[^37..^6]);
        }
        [Fact]
        public void Starting_slice()
        {
            int to = Rand.Next(a.Length);
            Assert.Equal(a.Take(to), a[..to]);
        }
        [Fact]
        public void Ending_slice()
        {
            int from = Rand.Next(a.Length);
            Assert.Equal(a.Skip(from), a[from..]);
        }
        [Fact]
        public void Any_slice()
        {
            int from = Rand.Next(a.Length / Rand.Next(2, 4));
            int size = Rand.Next(a.Length / Rand.Next(3, 5));
            int to = from + size;
            Assert.Equal(a.Skip(from).Take(size), a[from..to]);
        }
        [Fact]
        public void Any_slice_back()
        {
            int from = Rand.Next(a.Length / Rand.Next(2, 4));
            int size = Rand.Next(a.Length / Rand.Next(3, 5));
            int to = from + size;
            Assert.Equal(a.Skip(a.Length - to).Take(size), a[^to..^from]);
        }
    }
}
