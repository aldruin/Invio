using Invio.Application.DTOs.JwtDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Application.Interfaces.Jwt
{
    public interface IJwtService
    {
        Task<string> GenerateToken(JwtDto jwtDto);
        Task<JwtTokenViewDto> ReadTokenAsync(string token);
    }
}
