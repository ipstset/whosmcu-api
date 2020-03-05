using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ipstset.WhosMcu.Api.Models
{
    public class ErrorDetailModel
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Field { get; set; }
    }
}
