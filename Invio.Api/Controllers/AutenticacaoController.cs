using Invio.Application.DTOs.JwtDTOs;
using Invio.Application.Interfaces;
using Invio.Application.Interfaces.Jwt;
using Invio.Application.Services;
using Invio.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Invio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IAuthService _authService;

        public AutenticacaoController(IUsuarioService usuarioService, IAuthService authService)
        {
            _usuarioService = usuarioService;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var userRequest = await _authService.LoginAsync(loginRequest);
                if (userRequest == null)
                    return Unauthorized(new { ErrorMessage = "Usuário ou senha inválidos" });
                return Ok(userRequest);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("refresh-user-token")]
        public async Task<IActionResult> RefreshUserToken()
        {
            try
            {
                var userResponse = await _authService.RefreshUserTokenAsync(User);

                if (userResponse == null)
                {
                    return Unauthorized(new { ErrorMessage = "Usuário expirado" });
                }

                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }

        }

    }
}
