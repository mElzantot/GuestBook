using GuestBook.BAL.DTO;
using GuestBook.DAL.Entites;
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

namespace GuestBook.BAL.Services
{
    public class TokenServiceProvider : ITokenServiceProvider
    {
        private readonly IConfiguration _configuration;
        public TokenServiceProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthResponseDTO GenerateAccessToken(Guest guest)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: GenerateUserClaim(guest),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                 );
            return new AuthResponseDTO
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = GenerateRefreshToken(),
                ExpiryDate = DateTime.Now.AddMinutes(int.Parse(_configuration["JWT:LifeTimeInMinutes"]))
            };
        }

        private List<Claim> GenerateUserClaim(Guest guest)
        {
            return new List<Claim>
                {
                    new Claim(ClaimTypes.Name, guest.GuestName),
                    new Claim(ClaimTypes.NameIdentifier, guest.Id.ToString()),
                };
        }

        private string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[32];
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }


    }
}
