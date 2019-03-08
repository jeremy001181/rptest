using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Controllers;
using Api.Models;
using Microsoft.Extensions.Options;

namespace Api.ApiClients
{

    public class AlbumApiClient : IAlbumApiClient
    {
        private readonly IHttpClientFactory factory;
        private readonly ApiOption apiOption;

        public AlbumApiClient(IHttpClientFactory factory, IOptions<ApiOption> options)
        {
            this.factory = factory;
            this.apiOption = options.Value;
        }

        public async Task<IApiResponse> GetAlbumsAsync()
        {
            var client = factory.CreateClient();

            using (var res = await client.GetAsync(apiOption.AlbumsApi)) {
                return await res.Content.ReadAsAsync<List<Album>>();
            }
        }
    }
}