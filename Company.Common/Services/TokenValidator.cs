using Company.Common.Inerfaces;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Company.Common.Services
{
    public class TokenValidator : ITokenValidator
    {
        public string ReturnUserId(HttpRequest req)
        {
            var token = req.Headers["Authorization"].FirstOrDefault().Split(" ")[1];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = handler.ReadToken(token) as JwtSecurityToken;
            return tokenS.Claims.First(claim => claim.Type == "nameid").Value;
        }
    }
}
