using System;
using Xunit;

namespace Cs9CheatSheet.FitAndFinish.CovariantReturns
{
    namespace OldWay
    {
        class Thing
        {
            public string Name { get; init; }
        }
        class Place : Thing
        {
            public string Area { get; init; }
        }
        class Country : Place
        {
            public string Capital { get; init; }
        }
        class Event
        {
            public virtual Thing Get() => new Thing { Name = "Big Bang" };
        }
        class Trip : Event
        {
            public override Thing Get() => new Place { Name = "Cruise", Area = "Mediterrnanean Sea" };
        }
        class Holiday : Trip
        {
            public override Thing Get() => new Country { Name = "Australia", Area = "Oceania", Capital = "Canberra" };
        }

        public class Tests
        {
            [Fact]
            public void Test()
            {
                var (@event, trip, holiday) = (new Event(), new Trip(), new Holiday());

                var thing = @event.Get();
                var thingName = thing.Name;

                var place = trip.Get();
                var placeName = place.Name;
                //var placeArea = place.Area; //compiler error: Area is not Thing class' member
                var place1 = (Place)trip.Get(); //cast required
                var placeArea = place1.Area; //ok

                var country = holiday.Get();
                var countryName = country.Name;
                //var countryArea = country.Area; //compiler error: Area is not Thing class' member
                //var countryCapital = country.Capital; //compiler error: Capital is not Thing class' member
                var country1 = (Place)holiday.Get(); //cast to Place
                var countryArea = country1.Area; //ok
                //var countryCapital = country1.Capital; //compiler error: Capital is not Place class' member
                var country2 = (Country)holiday.Get(); //cast to Country
                var countryCapital = country2.Capital; //ok

                Assert.Throws<InvalidCastException>(() => (Place)@event.Get()); //Runtime error
                Assert.Throws<InvalidCastException>(() => (Country)@event.Get()); //Runtime error
                Assert.Throws<InvalidCastException>(() => (Country)trip.Get()); //Runtime error
            }
        }
    }

    namespace NewWay
    {
        class Thing
        {
            public string Name { get; init; }
        }
        class Place : Thing
        {
            public string Area { get; init; }
        }
        class Country : Place
        {
            public string Capital { get; init; }
        }
        class Event
        {
            public virtual Thing Get() => new Thing { Name = "Big Bang" };
        }
        class Trip : Event
        {
            public override Place Get() => new Place { Name = "Cruise", Area = "Mediterrnanean Sea" };
        }
        class Holiday : Trip
        {
            public override Country Get() => new Country { Name = "Australia", Area = "Oceania", Capital = "Canberra" };
        }
        public class Tests
        {
            [Fact]
            public void Test()
            {
                var (@event, trip, holiday) = (new Event(), new Trip(), new Holiday());

                var thing = @event.Get();
                var thingName = thing.Name;

                var place = trip.Get();
                var placeName = place.Name;
                var placeArea = place.Area; //ok, place has already the correct type, no cast required

                var country = holiday.Get();
                var countryName = country.Name;
                var countryArea = country.Area; //ok, country has already the correct type, no cast required
                var countryCapital = country.Capital; //ok, country has already the correct type, no cast required

                //As cast is not required, the possibility of runtime errors due to wrong cast is eliminated
            }
        }
    }
}