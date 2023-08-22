using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace NotesApi.Tests
{
    public class HelloControllerTests
    {
        private readonly HttpClient _client;

        public HelloControllerTests()
        {
            // Arrange
            var factory = new WebApplicationFactory<Startup>();
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Post_ReturnsHelloMessage()
        {
            // Act
            var response = await _client.PostAsJsonAsync("/hello", new { name = "someone" });
            var content = await response.Content.ReadFromJsonAsync<dynamic>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("hello someone", (string)content.message);
        }
    }
}
