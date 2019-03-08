using Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.ApiClients
{
    public interface IPhotoApiClient
    {
        Task<IApiResponse> GetPhotosAsync();
    }
}