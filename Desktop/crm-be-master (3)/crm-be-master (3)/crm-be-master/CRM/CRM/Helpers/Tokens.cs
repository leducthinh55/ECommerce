using CRM.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CRM.Helpers
{
    public class Tokens
    {
        public static async Task<object> GenerateJwt(ClaimsIdentity identity
                                                    , IJwtFactory jwtFactory
                                                    , string userName
                                                    , JwtIssuerOptions jwtOptions
                                                    , JsonSerializerSettings serializerSettings
                                                    , IList<String> roles = null)
        {
            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                access_token = await jwtFactory.GenerateEncodedToken(userName, identity, roles),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds,
                roles = roles,
            };

            return response;
        }
    }
}
