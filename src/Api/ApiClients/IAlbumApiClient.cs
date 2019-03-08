using System.Threading.Tasks;

namespace Api.ApiClients
{
    public interface IAlbumApiClient
    {
        Task<IApiResponse> GetAlbumsAsync();
    }
}