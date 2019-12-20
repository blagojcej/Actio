using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Actio.Services.Activities.Test.Integration.Controllers
{
    public class HomeControllerTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public HomeControllerTest()
        {
            _server=new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task home_controller_get_should_return_string_content()
        {
            var response = await _client.GetAsync("/api/home");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            content.Should().BeEquivalentTo("Hello from Actio.Services.Activities API!");
        }
    }
}
