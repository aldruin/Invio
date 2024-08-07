using Invio.Application.DTOs;
using Invio.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EquipeController : ControllerBase
    {
        private readonly IEquipeService _equipeService;
        private readonly INotificationService _notificationService;

        public EquipeController(IEquipeService equipeService, INotificationService notificationService)
        {
            _equipeService = equipeService;
            _notificationService = notificationService;
        }

        [HttpPost("criar")]
        public async Task<IActionResult> CriarEquipe([FromBody] EquipeDto dto)
        {
            var equipeCriada = await _equipeService.CriarEquipeAsync(dto);
            var notificacoes = _notificationService.GetNotifications();

            if (equipeCriada != null)
            {
                var resposta = new { equipeCriada, notificacoes };
                return Ok(resposta);
            }
            return BadRequest(notificacoes);
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> AtualizarEquipe([FromBody] EquipeDto dto)
        {
            var equipeAtualizada = await _equipeService.AtualizarEquipeAsync(dto);
            var notificacoes = _notificationService.GetNotifications();

            if (equipeAtualizada != null)
            {
                var resposta = new { equipeAtualizada, notificacoes };
                return Ok(resposta);
            }
            return BadRequest(notificacoes);
        }

        [HttpGet("obter/{id}")]
        public async Task<IActionResult> ObterEquipePorId(Guid id)
        {
            var equipe = await _equipeService.ObterEquipePorIdAsync(id);
            var notificacoes = _notificationService.GetNotifications();

            if (equipe != null)
            {
                var resposta = new { equipe, notificacoes };
                return Ok(resposta);
            }
            return BadRequest(notificacoes);
        }

        [HttpGet("obter-todos")]
        public async Task<IActionResult> ObterEquipes()
        {
            var equipes = await _equipeService.ObterEquipeAsync();
            var notificacoes = _notificationService.GetNotifications();

            if (equipes != null)
            {
                var resposta = new { equipes, notificacoes };
                return Ok(resposta);
            }
            return BadRequest(notificacoes);
        }

        [HttpDelete("excluir/{id}")]
        public async Task<IActionResult> ExcluirEquipe(Guid id)
        {
            var equipeExcluida = await _equipeService.RemoverEquipeAsync(id);
            var notificacoes = _notificationService.GetNotifications();

            if (equipeExcluida != null)
            {
                var resposta = new { equipeExcluida, notificacoes };
                return Ok(resposta);
            }
            return BadRequest(notificacoes);
        }
    }
}
