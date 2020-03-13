using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Ipstset.WhosMcu.Api.ApiTokens
{
    public class ApiTokenManager:IApiTokenManager
    {

        private ApiTokenSettings _settings;

        public ApiTokenManager(ApiTokenSettings settings)
        {
            _settings = settings;
        }

        public string CreateToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //todo: handle multiple issuers, audiences
            var claims = new List<Claim> {
                new Claim("iss",_settings.Issuers.First()),
                new Claim("aud",_settings.Audiences.First()),
                new Claim("sub",Guid.NewGuid().ToString()) 
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: GetTokenExpireDate(),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public bool ValidateToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var param = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuers = _settings.Issuers,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidAudiences = _settings.Audiences,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret)),
                    ValidateLifetime = true
                };
                handler.ValidateToken(token, param, out var validatedToken);

                var jwt = (JwtSecurityToken)validatedToken;
                AppUser.SessionId = jwt.Subject;
                return true;
            }
            catch (SecurityTokenException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public JwtSecurityToken ReadToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(token);
        }

        private DateTime GetTokenExpireDate()
        {
            return DateTime.UtcNow.AddMinutes(_settings.MinutesToExpire);
        }

    }
}
