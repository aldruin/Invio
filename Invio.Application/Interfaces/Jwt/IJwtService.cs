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
        string GenerateToken(JwtDto jwtDto);
        JwtTokenViewDto ReadToken(string token);
    }
}
