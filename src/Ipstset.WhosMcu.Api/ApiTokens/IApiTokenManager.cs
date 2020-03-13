using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Ipstset.WhosMcu.Api.ApiTokens
{
    public interface IApiTokenManager
    {
        string CreateToken();
        bool ValidateToken(string token);
        JwtSecurityToken ReadToken(string token);
    }
}
