using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Application.Telegram.Helpers
{

    public class Temperatures
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
        private readonly HttpClient _client = new HttpClient();

        
        public async Task<string> FindCountry(double longitude, double latitude)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var response = await _client.GetAsync($"https://nominatim.openstreetmap.org/reverse?lat={latitude}&lon={longitude}&format=json");

            var address = await response.Content.ReadFromJsonAsync<Temperatures>();
            
            return address.Address.Country;
        }
    }
}
