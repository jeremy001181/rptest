using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Models;

namespace Api.ApiClients
{
    public class PhotoApiClient : IPhotoApiClient
    {
        private readonly HttpClient client;

        public PhotoApiClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IApiResponse> GetPhotosAsync()
        {
            using (var res = await client.GetAsync("/photos")){
                var photos = await res.Content.ReadAsAsync<List<Photo>>();
                return new PhotoApiResponse(photos);
            }
        }
    }
}