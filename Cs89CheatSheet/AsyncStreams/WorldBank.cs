using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cs9CheatSheet.AsyncStreams.WorldBank
{
    public interface IDataSource
    {
        Task<string[]> DownloadIso2CodesAsync();
        IEnumerable<Task<JsonElement>> DownloadCountries(string[] iso2Codes);
        IAsyncEnumerable<JsonElement> DownloadCountriesStream(string[] iso2Codes);
    }

    abstract class DataSource : IDataSource
    {
        static async Task<JsonElement.ArrayEnumerator> Fetch(string url)
        {
            var webClient = new WebClient();
            var str = await webClient.DownloadStringTaskAsync(url);
            var worldBankResponse = JsonDocument.Parse(str);
            var array = worldBankResponse.RootElement.EnumerateArray();
            array.MoveNext(); //header
            array.MoveNext(); //country(ies)
            return array;
        }
        public async Task<string[]> DownloadIso2CodesAsync()
        {
            var array = await Fetch(@"http://api.worldbank.org/v2/country?format=json&per_page=100");
            var countries = array.Current.EnumerateArray();
            var iso2Codes = countries.Select(country => country.GetProperty("iso2Code").GetString()).ToArray();
            return iso2Codes;
        }
        public IEnumerable<Task<JsonElement>> DownloadCountries(string[] iso2Codes)
        {
            for (int i = 0, n = StartFeed(iso2Codes); i < n; i++)
            {
                yield return DownloadCountryAsync(i, iso2Codes[i]);
            }
        }
        public async IAsyncEnumerable<JsonElement> DownloadCountriesStream(string[] iso2Codes)
        {
            for (int i = 0, n = StartFeed(iso2Codes); i < n; i++)
            {
                yield return await DownloadCountryAsync(i, iso2Codes[i]);
            }
        }
        protected virtual int StartFeed(string[] codes) { return codes.Length; }
        protected virtual async Task<JsonElement> DownloadCountryAsync(int i, string isoCode)
        {
            var array = await Fetch(@$"http://api.worldbank.org/v2/country/{isoCode}?format=json");
            var country = array.Current;
            return country;
        }
    }

    class CountriesDb : DataSource { }

    class CountriesIoT : DataSource
    {
        static SemaphoreSlim[] _semaphores;

        protected override async Task<JsonElement> DownloadCountryAsync(int i, string isoCode)
        {
            await _semaphores[i].WaitAsync();
            return await base.DownloadCountryAsync(i, isoCode);
        }

        protected override int StartFeed(string[] codes)
        {
            var n = codes.Length;
            async Task Feed()
            {
                for (int i = 0; i < n; i++)
                {
                    await Task.Delay(1000);
                    _semaphores[i].Release();
                }
            }
            _semaphores = Enumerable.Range(0, n).Select(_ => new SemaphoreSlim(0, 1)).ToArray();
            Task.Run(Feed);
            return n;
        }
    }

    public abstract class Tests
    {
        IDataSource DataSource { get; init; }

        protected Tests(IDataSource dataSource) => DataSource = dataSource;

        async Task<string[]> GetRandomIsoCodes()
        {
            var iso2Codes = await DataSource.DownloadIso2CodesAsync();
            var random = new Random();
            return Enumerable.Range(0, 10).Select(i => random.Next(i * 10, i * 10 + 10)).Select(i => iso2Codes[i]).ToArray();
        }

        [Fact]
        public async Task TestBlock()
        {
            var selection = await GetRandomIsoCodes();
            var countries = await Task.WhenAll(DataSource.DownloadCountries(selection).ToArray());
            foreach (var country in countries) AssertCountry(country);
        }

        [Fact]
        public async Task TestStream()
        {
            var selection = await GetRandomIsoCodes();
            var countries = DataSource.DownloadCountriesStream(selection);
            await foreach (var country in countries) AssertCountry(country);
        }

        void AssertCountry(JsonElement country) {/*...*/}
    }


    public class TestsDb : Tests
    {
        public TestsDb() : base(new CountriesDb()) {}
    }

    public class TestsIoT : Tests
    {
        public TestsIoT() : base(new CountriesIoT()) { }
    }

}
