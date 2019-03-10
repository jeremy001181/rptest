using System.Collections.Generic;

namespace Api.Models
{
    public class PhotoApiResponse : IApiResponse
    {
        private IList<Photo> photos;

        public PhotoApiResponse(IList<Photo> photos)
        {
            this.photos = photos;
        }

        public void Set(Integrator integrator)
        {
            integrator.Photos = photos;
        }
    }
}