using System.Collections.Generic;
using System.Threading.Tasks;
using Api.ApiClients;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IPhotoApiClient photoApiClient;
        private readonly IAlbumApiClient albumApiClient;

        public AlbumsController(IAlbumApiClient albumApiClient, IPhotoApiClient photoApiClient)
        {
            this.photoApiClient = photoApiClient;
            this.albumApiClient = albumApiClient;
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> Get(int userId)
        {
            var responses = await Task.WhenAll(
                  photoApiClient.GetPhotosAsync()
                , albumApiClient.GetAlbumsAsync());

            var integrator = new Integrator();

            foreach (var res in responses) {
                res.Set(integrator);
            }

            var albums = integrator.GetAlumsByUserId(userId);

            return new OkObjectResult(albums);
        }

    }
}
