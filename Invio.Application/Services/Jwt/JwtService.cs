using Invio.Application.DTOs.JwtDTOs;
using Invio.Application.Interfaces.Jwt;
using Invio.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Application.Services.Jwt
{
    public sealed class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(JwtDto jwtDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(GetTokenDescriptor(jwtDto));

            return tokenHandler.WriteToken(token);
        }

        public JwtTokenViewDto ReadToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);

            return
                new JwtTokenViewDto
                {
                    Id = Guid.Parse(jwtSecurityToken.Claims.FirstOrDefault(u => u.Type == "nameidentifier")?.Value),
                    Email = jwtSecurityToken.Claims.FirstOrDefault(u => u.Type == "email")?.Value
                };
        }

        private SecurityTokenDescriptor GetTokenDescriptor(JwtDto jwtDto)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSecurity:SecurityKey"]);

            var userCLaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, jwtDto.Id.ToString()),
                new Claim(ClaimTypes.Email, jwtDto.Email)
            };

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userCLaims),
                Expires = DateTime.UtcNow.AddHours(int.Parse(_configuration["JwtSecurity:Expiration"])),
                SigningCredentials = credentials
            };

            return tokenDescriptor;
        }
    }
}
