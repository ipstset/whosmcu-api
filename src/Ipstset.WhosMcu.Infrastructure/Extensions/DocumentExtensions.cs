using Ipstset.WhosMcu.Application.McuActors;
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
                        //Title = data.Title,
                        //Body = data.Body,
                        //IsStarred = data.IsStarred,
                        //FolderId = data.FolderId,
                        //UserId = data.UserId,
                        //IsPublic = data.IsPublic,
                        //DateCreated = data.DateCreated,
                        //DateModified = data.DateModified
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
