using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ipstset.WhosMcu.Api
{
    public class Constants
    {
        public const int MaxRequestLimit = 100;
        public const string ApiTokenHeader = "Wm-Token";
        public class Routes
        {

            //public class Jots
            //{
            //    public const string GetJot = "GetJot";
            //    public const string CreateJot = "CreateJot";
            //    public const string UpdateJot = "UpdateJot";
            //    public const string DeleteJot = "DeleteJot";
            //}
            public class Movies
            {
                public const string GetMovie = "GetMovie";
            }

            public class Tokens
            {
                public const string CreateToken = "CreateToken";
            }

        }
    }
}
