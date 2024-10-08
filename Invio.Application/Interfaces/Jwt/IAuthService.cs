﻿using Invio.Application.DTOs.JwtDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Application.Interfaces.Jwt
{
    public interface IAuthService
    {
        Task<UserResponse> LoginAsync(LoginRequest request);
        Task<UserResponse> RefreshUserAsync(ClaimsPrincipal user);
    }
}
