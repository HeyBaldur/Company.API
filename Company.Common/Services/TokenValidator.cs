using Company.Common.Inerfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Common.Services
{
    public class TokenValidator : ITokenValidator
    {
        public async Task<int> ReturnUserId(HttpRequest req)
        {
            int userId = 0;
            await Task.Factory.StartNew(() =>
            {
                var token = req.Headers["Authorization"].FirstOrDefault().Split(" ")[1];
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token);
                var tokenS = handler.ReadToken(token) as JwtSecurityToken;
                var nameId = tokenS.Claims.First(claim => claim.Type == "nameid").Value;
                userId = Convert.ToInt32(nameId);
                return userId;
            });

            return userId;
        }
    }
}
