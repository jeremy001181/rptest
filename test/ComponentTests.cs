using Api.ApiClients;
using Api.Controllers;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests
{
    public class ComponentTests
    {
        [Theory]
        [InlineData(123, 1, 1)]
        [InlineData(124, 2, 3)]
        public async Task ShouldReturnSuccessAndCorrectData(int userid, int countOfAlbums, int countOfPhotos) {
            var mockHttp = new MockHttpMessageHandler();
            var albumsTestData = new List<Album>() {
                new Album{ Id = 1, UserId=123}
               ,new Album{ Id = 2, UserId=124}
               ,new Album{ Id = 3, UserId=124}
            };
            var photosTestData = new List<Photo>() {
                new Photo{ Id = 10, AlbumId=1}
               ,new Photo{ Id = 11, AlbumId=2}
               ,new Photo{ Id = 12, AlbumId=2}
               ,new Photo{ Id = 13, AlbumId=3}
                
            };

            mockHttp.When("http://jsonplaceholder.typicode.com/albums")
                    .Respond("application/json", JsonConvert.SerializeObject(albumsTestData)); // Respond with JSON

            mockHttp.When("http://jsonplaceholder.typicode.com/photos")
                    .Respond("application/json", JsonConvert.SerializeObject(photosTestData)); // Respond with JSON

            var client = mockHttp.ToHttpClient();
            client.BaseAddress = new Uri("http://jsonplaceholder.typicode.com");

            var controller = new AlbumsController(new AlbumApiClient(client), new PhotoApiClient(client));

            var result = await controller.Get(userid);

            var response = ((OkObjectResult)result.Result).Value as IList<dynamic>;
            Assert.Equal(countOfAlbums, response.Count);

            var data = JsonConvert.DeserializeObject<List<TestResponse>>(JsonConvert.SerializeObject(response));

            Assert.Equal(countOfPhotos, data.Sum(album => album.Photos.Count));
        }
    }
}
