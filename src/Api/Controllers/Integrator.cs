using System;
using System.Collections.Generic;
using System.Linq;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class Integrator
    {
        public Integrator()
        {
        }

        public IList<Photo> Photos { get; internal set; }
        public IList<Album> Albums { get; internal set; }

        internal IEnumerable<dynamic> GetAlumsByUserId(int userId)
        {
            //from post in databasePosts
            //join meta in database.Post_Metas on post.ID equals meta.Post_ID
            //where post.ID == id
            //select new { Post = post, Meta = meta };
            return Albums.Where(album => album.UserId == userId)
                .Join(Photos, (a) => a.Id, (p) => p.AlbumId, (a, p) => new {
                    Album = a, Photos = p
                });
        }
    }
}