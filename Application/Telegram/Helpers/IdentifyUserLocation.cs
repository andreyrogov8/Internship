using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Application.Telegram.Helpers
{

    public class LocationInfo
    {

        [JsonProperty("address")]
        public Address Address { get; set; }

    }


    public class Address
    {
        [JsonProperty("country")]
        public string Country { get; set; }

    }


    public class IdentifyUserLocation
    {
        private readonly HttpClient _client;

        public IdentifyUserLocation(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
        }

        public async Task<string> FindCountryAsync(double longitude, double latitude)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

            _client.DefaultRequestHeaders.Add("Accept-Language", "en");

            _client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var response = await _client.GetAsync($"https://nominatim.openstreetmap.org/reverse?lat={latitude}&lon={longitude}&format=json");

            var address = await response.Content.ReadFromJsonAsync<LocationInfo>();

            return address.Address.Country;
        }
    }
}
