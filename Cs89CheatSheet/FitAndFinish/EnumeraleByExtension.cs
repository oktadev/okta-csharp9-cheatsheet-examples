using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cs9CheatSheet.FitAndFinish.EnumeraleByExtension
{
    class EnumerableTour : IEnumerable
    {
        public string Day1 => "New York";
        public string Day2 => "Boston";
        public string Day3 => "Washington DC";
        public IEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }
        class Enumerator : IEnumerator
        {
            EnumerableTour _tour;

            public Enumerator(EnumerableTour tour)
            {
                _tour = tour;
            }

            int _index;
            public object Current => _index switch
            {
                0 => _tour.Day1,
                1 => _tour.Day2,
                2 => _tour.Day3,
                _ => "Please buy a new Tour"
            };

            public bool MoveNext() => (_index = Math.Max(_index++, 3)) < 3;

            public void Reset() => _index = 0;
        }
    }
    public class NonEnumerableTour
    {
        public string Day1 => "Chicago";
        public string Day2 => "Las Vegas";
        public string Day3 => "Miami";
    }

    static public class Extensions
    {
        public class MyEnumerator
        {
            NonEnumerableTour _tour;
            int _index = 0;

            public MyEnumerator(NonEnumerableTour tour)
            {
                _tour = tour;
            }

            public string Current => _index switch
            {
                0 => _tour.Day1,
                1 => _tour.Day2,
                2 => _tour.Day3,
                _ => "Please buy a new Tour"
            };

            public bool MoveNext() => (_index = Math.Max(_index++, 3)) < 3;
        }
        public static MyEnumerator GetEnumerator(this NonEnumerableTour tour) => new MyEnumerator(tour);
    }

    public class Tests
    {
        [Fact]
        public void This_tour_is_for_me()
        {
            var lovedCities = new HashSet<string>( new [] { "New York", "Chicago", "Washington DC", "Las Vegas", "Los Angeles", "Boston", "Miami" });

            foreach (var city in new EnumerableTour()) Assert.Contains(city, lovedCities);
            foreach (var city in new NonEnumerableTour()) Assert.Contains(city, lovedCities);
        }
    }
}
