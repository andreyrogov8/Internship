using Domain.Authentication;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Application.IntegrationTests.controllers
{
    public class UserControllerTests : IClassFixture<TestWebAppFactory<Program>>
    {
        private readonly ITestOutputHelper _output;
        private readonly HttpClient _client;

        public UserControllerTests(TestWebAppFactory<Program> factory, ITestOutputHelper output)
        {
            _output = output;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetToken()
        {
            var userCredentials = new TokenRequest { Email = "admin@gmail.com", Password = "Pa$$w0rd" };

            var json = JsonConvert.SerializeObject(userCredentials);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/users/token", data);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            _output.WriteLine(response.StatusCode.ToString());

            var responseString = await response.Content.ReadAsStringAsync();
            var tokenInfo = JsonConvert.DeserializeObject<AuthenticationModel>(responseString);
            tokenInfo.Roles.Should().Contain("Administrator");

            _output.WriteLine($"User Full Name: {tokenInfo.FirstName} {tokenInfo.LastName}");
            _output.WriteLine($"Token: {tokenInfo.Token}");
        }
    }
}
