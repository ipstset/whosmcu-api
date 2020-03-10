using System;
using System.Collections.Generic;
using System.Text;

namespace Ipstset.WhosMcu.Application.McuActors
{
    public class ActorCharacter
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public IEnumerable<CharacterMovie> Movies { get; set; }
    }
}
