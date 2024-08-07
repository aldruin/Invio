using Invio.Application.DTOs;
using Invio.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Invio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly INotificationService _notificationService;

        public UsuarioController(IUsuarioService usuarioService, INotificationService notificationService)
        {
            _usuarioService = usuarioService;
            _notificationService = notificationService;
        }

        [HttpPost("criar")]
        public async Task<IActionResult> CriarUsuario([FromBody] UsuarioDto dto)
        {
            var usuarioCriado = await _usuarioService.CriaUsuarioAsync(dto);
            var notificacoes = _notificationService.GetNotifications();

            if (usuarioCriado != null)
            {
                var resposta = new { usuarioCriado, notificacoes };
                return Ok(resposta);
            }
            return BadRequest(notificacoes);
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> AtualizarUsuario([FromBody] UsuarioDto dto)
        {
            var usuarioAtual = HttpContext.User;
            var usuarioAtualizado = await _usuarioService.AtualizaUsuarioAsync(usuarioAtual, dto);
            var notificacoes = _notificationService.GetNotifications();

            if (usuarioAtualizado != null)
            {
                var resposta = new { usuarioAtualizado, notificacoes };
                return Ok(resposta);
            }
            return BadRequest(notificacoes);
        }

        [HttpDelete("excluir")]
        public async Task<IActionResult> ExcluirUsuario()
        {
            var usuarioAtual = HttpContext.User;
            var usuarioExcluido = await _usuarioService.RemoveUsuarioAsync(usuarioAtual);
            var notificacoes = _notificationService.GetNotifications();

            if (usuarioExcluido != null)
            {
                var resposta = new { usuarioExcluido, notificacoes };
                return Ok(resposta);
            }
            return BadRequest(notificacoes);
        }
    }
}
