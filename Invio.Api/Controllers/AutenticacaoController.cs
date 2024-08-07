using Invio.Application.DTOs.JwtDTOs;
using Invio.Application.Interfaces;
using Invio.Application.Interfaces.Jwt;
using Invio.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Login([FromQuery] LoginRequest loginRequest)
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
    }
}
