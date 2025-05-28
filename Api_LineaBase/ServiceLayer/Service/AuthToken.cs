using Commun.Helpers;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ServiceLayer.Service
{
    public class AuthToken : IAuthToken
    {
        private readonly IConfiguration configuration;

        public AuthToken(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }

        public string GenerarToken(AuthModel authModel)
        {
            string SecretPassword = JwtSettings.Jwt_SecretPassword;
            string Issuer = JwtSettings.Jwt_Issuer;
            string Audience = JwtSettings.Jwt_Audience;
            string Expire = JwtSettings.Jwt_Expire;
            string key = JwtSettings.Jwt_Key;
            string ApiKey = JwtSettings.Jwt_Apikey;

            if (key.Equals(authModel.Key) && ApiKey.Equals(authModel.ApiKey))
            {
                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretPassword));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
                var header = new JwtHeader(signingCredentials);

                var payload = new JwtPayload(
                        issuer: Issuer,
                        audience: Audience,
                        claims: null,
                        notBefore: DateTime.Now,
                        expires: DateTime.Now.AddMinutes(Convert.ToInt32(Expire))
                    );

                var token = new JwtSecurityToken(header, payload);

                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            else
                return null;
        }
    }
}
