using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechAPI.Application.Abstractions.Tokens;
using TextToSpeechAPI.Application.DTOs;
using TextToSpeechAPI.Domain.Entities.Identities;

namespace TextToSpeechAPI.Infrastructure.Services.Tokens
{
    public class UserTokenHandler:IUserTokenHandler
    {
        readonly IConfiguration _configuration;
        public UserTokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TokenDTO CreateAccessToken(AppUser appUser)
        {
            TokenDTO token = new();
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["UserToken:SecurityKey"]));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);


            token.Expiration = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["UserToken:LifeTimeMinute"]));
            JwtSecurityToken securityToken = new JwtSecurityToken(
                audience: _configuration["UserToken:Audience"],
                issuer: _configuration["UserToken:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: new List<Claim> { new(ClaimTypes.Name, appUser.UserName) { } }
                );
            //token oluşturucu sınıfından bir örnek alınmalı
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            token.RefreshToken = CreateRefreshToken();
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
