using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ipstset.WhosMcu.Api.ApiTokens
{
    public class ApiTokenSettings
    {
        public IEnumerable<string> Issuers { get; set; }
        public IEnumerable<string> Audiences { get; set; }
        public int MinutesToExpire { get; set; }
        public string Secret { get; set; }
    }
}
