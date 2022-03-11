using Application.Features.MapFeature.Queries;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Application.IntegrationTests.controllers
{
    public class MapsControllerTests : IClassFixture<TestWebAppFactory<Program>>
    {
        private readonly ITestOutputHelper _output;
        private readonly HttpClient _client;
        private readonly TestWebAppFactory<Program> _factory;

        public MapsControllerTests(TestWebAppFactory<Program> factory, ITestOutputHelper output)
        {
            _output = output;

            _factory = factory;
            _client = factory.CreateClient();
        }

        //[Fact]
        //public async Task GET_maps()
        //{
        //    var response = await _client.GetAsync("api/maps");
        //    response.StatusCode.Should().Be(HttpStatusCode.OK);

        //    _output.WriteLine(response.StatusCode.ToString());

        //    var responseString = await response.Content.ReadAsStringAsync();
        //    var mapList = JsonConvert.DeserializeObject<GetMapListQueryResponse>(responseString);
        //    mapList.Results.Should().HaveCount(3);
        //}
    }
}
