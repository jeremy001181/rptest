using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Api.ApiClients;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IPhotoApiClient photoApiClient;
        private readonly IAlbumApiClient albumApiClient;

        public ValuesController(IPhotoApiClient photoApiClient, IAlbumApiClient albumApiClient)
        {
            this.photoApiClient = photoApiClient;
            this.albumApiClient = albumApiClient;
        }
        // GET api/values/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> Get(int userId)
        {

            var handlers = await Task.WhenAll(
                  photoApiClient.GetPhotosAsync()
                , albumApiClient.GetAlbumsAsync());

            var integrator = new Integrator();

            foreach (var handler in handlers) {
                handler.Handle(integrator);
            }

            return new OkObjectResult(integrator.GetAlumsByUserId(userId));
        }

    }
}
