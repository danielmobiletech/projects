using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JWTAuth.Migrations
{
    public class JwtService
    {
        private string secureKey = "this is a very secure key";
       
        public string Generate(int id)
        {
            var sysmetrickey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            var credentials = new SigningCredentials(sysmetrickey,SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);
            var payload = new JwtPayload(id.ToString(), null, null,null, DateTime.Today.AddDays(1));

            var securityToken = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);


        }
        public JwtSecurityToken Verify(string jwt)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var keys = Encoding.UTF8.GetBytes(secureKey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters {

                IssuerSigningKey=new SymmetricSecurityKey(keys),
                ValidateIssuer=false,
                ValidateIssuerSigningKey=true,
                ValidateAudience=false 




            },out SecurityToken validateToken);
            return (JwtSecurityToken)validateToken;
        }
    }
}
