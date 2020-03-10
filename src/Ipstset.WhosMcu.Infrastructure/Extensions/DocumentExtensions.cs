using Ipstset.WhosMcu.Application.Actors;
using Ipstset.WhosMcu.Application.McuActors;
using Ipstset.WhosMcu.Application.Movies;
using Ipstset.WhosMcu.Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ipstset.WhosMcu.Infrastructure.Extensions
{
    public static class DocumentExtensions
    {
        public static McuActorResponse ToMcuActorResponse(this SqlDocument document)
        {
            try
            {
                //parse data
                var data = JsonConvert.DeserializeObject<McuActorResponse>(document.Data);
                if (data != null)
                    return new McuActorResponse
                    {
                        Id = data.Id,
                        FirstName = data.FirstName,
                        LastName = data.LastName,
                        FullName = data.FullName,
                        Characters = data.Characters
                    };

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static MovieResponse ToMovieResponse(this SqlDocument document)
        {
            try
            {
                //parse data
                var data = JsonConvert.DeserializeObject<MovieResponse>(document.Data);
                if (data != null)
                    return new MovieResponse
                    {
                        Id = data.Id,
                        Title = data.Title,
                        ReleaseDate = data.ReleaseDate
                    };

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static ActorResponse ToActorResponse(this SqlDocument document)
        {
            try
            {
                //parse data
                var data = JsonConvert.DeserializeObject<ActorResponse>(document.Data);
                if (data != null)
                    return new ActorResponse
                    {
                        Id = data.Id,
                        FullName = data.FullName,
                        FirstName = data.FirstName,
                        LastName = data.LastName,
                        Src = data.Src
                    };

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
