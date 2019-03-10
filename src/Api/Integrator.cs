using System;
using System.Collections.Generic;
using System.Linq;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api
{
    public class Integrator
    {
        public IList<Photo> Photos { get; internal set; }
        public IList<Album> Albums { get; internal set; }

        internal IList<dynamic> GetAlumsByUserId(int userId)
        {
            return Albums.Where(album => album.UserId == userId)
                .GroupJoin(Photos, (a) => a.Id, (p) => p.AlbumId, (a, p) => new {
                    Album = a, Photos = p
                }).ToList<dynamic>();
        }
    }
}