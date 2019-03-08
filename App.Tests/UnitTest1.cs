using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Threading.Tasks;
using Xunit;

namespace App.Tests
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Api.Startup>>
    {
        private readonly WebApplicationFactory<Api.Startup> _factory;

        public UnitTest1(WebApplicationFactory<Api.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/values/5")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            using (var response = await client.GetAsync(url)) {

                // Assert
                response.EnsureSuccessStatusCode(); // Status Code 200-299
                await response.Content.ReadAsStringAsync();

                Assert.Equal("text/html; charset=utf-8",
                    response.Content.Headers.ContentType.ToString());
            }
        }
    }
}
