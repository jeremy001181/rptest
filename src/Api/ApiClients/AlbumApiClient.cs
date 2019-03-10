using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Models;

namespace Api.ApiClients
{

    public class AlbumApiClient : IAlbumApiClient
    {
        private readonly HttpClient client;

        public AlbumApiClient(HttpClient client) {
            this.client = client;
        }
        
        public async Task<IApiResponse> GetAlbumsAsync()
        {
            using (var res = await client.GetAsync("/albums")) {
                var albums = await res.Content.ReadAsAsync<List<Album>>();
                return new AlbumApiResponse(albums);
            }
        }
    }
}