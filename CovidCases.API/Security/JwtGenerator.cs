using CovidCases.API.Models;
using CovidCases.Contract.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Claim = System.Security.Claims.Claim;
namespace CovidCases.API.Security
{
    public class JwtGenerator
    {
        private readonly AppSettings _appSettings;

        public JwtGenerator(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }


        public object GenerateToken(dynamic user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.TokenSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidIn,
                Expires = DateTime.UtcNow.AddDays(_appSettings.ExpirationInDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("Id", user.Id),
                        new Claim("Email", user.Email),
                    })
            };

            user.Token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            return user;
        }
    }
}
