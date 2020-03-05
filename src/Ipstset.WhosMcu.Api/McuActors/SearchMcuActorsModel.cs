using Ipstset.WhosMcu.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ipstset.WhosMcu.Api.McuActors
{
    public class SearchMcuActorsModel: QueryModel
    {
        public string Name { get; set; }
    }
}
