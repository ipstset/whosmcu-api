using Ipstset.WhosMcu.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ipstset.WhosMcu.Application.McuActors
{
    public class ActorMovie
    {

        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IEnumerable<MovieCharacter> Characters { get; set; }
        public IEnumerable<Link> Links { get; set; }
    }
}
