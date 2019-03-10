using Api.Models;
using System.Collections.Generic;

namespace Api.Tests
{
        public class TestResponse
        {
            public Album Album{ get; set; }
            public List<Photo> Photos { get; set; }
        }
}
