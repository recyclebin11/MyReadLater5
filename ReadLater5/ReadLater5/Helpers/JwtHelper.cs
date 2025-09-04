using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ReadLater5.Constants;

namespace ReadLater5.Helpers
{
    public static class JwtHelper
    {
        public static string GetToken(IdentityUser user, IConfiguration configuration, out DateTime expires)
        {
            var utcNow = DateTime.UtcNow;
            List<Claim> claims = new();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(utcNow).ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64));

            expires = utcNow.AddHours(336);
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[ConfigurationConstants.JWT.Key]));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                notBefore: utcNow,
                expires: expires,
                audience: configuration[ConfigurationConstants.JWT.Audience],
                issuer: configuration[ConfigurationConstants.JWT.Issuer]
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }
    }
}
