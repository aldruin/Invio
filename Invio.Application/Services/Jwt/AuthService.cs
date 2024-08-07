using Invio.Application.DTOs.JwtDTOs;
using Invio.Application.Interfaces.Jwt;
using Invio.Domain.Entities;
using Invio.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Application.Services.Jwt
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Usuario> _usuarioManager;
        private readonly IJwtService _jwtService;

        public AuthService(UserManager<Usuario> usuarioManager, IJwtService jwtService)
        {
            _usuarioManager = usuarioManager;
            _jwtService = jwtService;
        }

        public async Task<UserResponse> LoginAsync(LoginRequest request)
        {
            var user = await _usuarioManager.FindByEmailAsync(request.Email);
            if (user == null || !await _usuarioManager.CheckPasswordAsync(user, request.Password))
                return null;
            var jwtToken = await _jwtService.GenerateToken(new JwtDto(user.Id, user.Email));

            return new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                JwtToken = jwtToken,
            };
        }
    }
}
