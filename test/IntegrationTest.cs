using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests
{
    public partial class IntegrationTest : IClassFixture<WebApplicationFactory<Api.Startup>>
    {
        private readonly WebApplicationFactory<Api.Startup> _factory;

        public IntegrationTest(WebApplicationFactory<Api.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(3)]
        public async Task AlbumsController_EndpointsReturnSuccessAndCorrectData(int userid)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            using (var response = await client.GetAsync($"/api/albums/{userid}")) {

                // Assert
                response.EnsureSuccessStatusCode(); 
                var result = await response.Content.ReadAsAsync<List<TestResponse>>();

                Assert.Equal(new List<int> { userid },
                    result.Select(r => r.Album.UserId).Distinct());

                foreach (var item in result)
                {
                    Assert.Equal(new List<int> { item.Album.Id },
                        item.Photos.Select(r => r.AlbumId).Distinct());                    
                }
            }
        }
    }
}
