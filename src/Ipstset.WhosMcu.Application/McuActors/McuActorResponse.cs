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

        public int MovieCount
        {
            get
            {
                var count = 0;
                if (Characters != null)
                {
                    count += Characters.Sum(character => character.Movies.Count());
                }

                return count;
            }
        }

        public string CharacterNames
        {
            get
            {
                var names = Characters.Aggregate("", (current, character) => current + (character.FullName + ", "));

                if (names.EndsWith(", "))
                    names = names.Substring(0, names.Length - 2);
                return names;
            }
        }

        public IEnumerable<ActorCharacter> Characters { get; set; }
        public IEnumerable<Link> Links { get; set; }
    }
}
