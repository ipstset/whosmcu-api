using Ipstset.WhosMcu.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ipstset.WhosMcu.Application.McuActors
{
    public class McuActorResponse
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Src { get; set; }
        public int MovieCount => Movies != null ? Movies.Count() : 0;
        public IEnumerable<ActorMovie> Movies { get; set; }
        public IEnumerable<Link> Links { get; set; }
    }
}
