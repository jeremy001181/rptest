using System.Collections.Generic;

namespace Api.Models
{

    public class AlbumApiResponse : IApiResponse
    {
        private IList<Album> albums;

        public AlbumApiResponse(IList<Album> albums)
        {
            this.albums = albums;
        }

        public void Set(Integrator integrator)
        {
            integrator.Albums = albums;
        }
    }
}