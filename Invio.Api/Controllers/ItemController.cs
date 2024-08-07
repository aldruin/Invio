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
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly INotificationService _notificationService;

        public ItemController(IItemService itemService, INotificationService notificationService)
        {
            _itemService = itemService;
            _notificationService = notificationService;
        }

        [HttpPost("criar")]
        public async Task<IActionResult> CriarItem([FromBody] ItemDto dto)
        {
            var itemCriado = await _itemService.CriarItemAsync(dto);
            var notificacoes = _notificationService.GetNotifications();

            if (itemCriado != null)
            {
                var resposta = new { itemCriado, notificacoes };
                return Ok(resposta);
            }
            return BadRequest(notificacoes);
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> AtualizarItem([FromBody] ItemDto dto)
        {
            var itemAtualizado = await _itemService.AtualizarItemAsync(dto);
            var notificacoes = _notificationService.GetNotifications();

            if (itemAtualizado != null)
            {
                var resposta = new { itemAtualizado, notificacoes };
                return Ok(resposta);
            }
            return BadRequest(notificacoes);
        }

        [HttpGet("obter/{equipeId}")]
        public async Task<IActionResult> ObterItensPorEquipeId(Guid equipeId)
        {
            var items = await _itemService.ObterItemPorEquipeIdAsync(equipeId);
            var notificacoes = _notificationService.GetNotifications();

            if (items != null)
            {
                var resposta = new { items, notificacoes };
                return Ok(resposta);
            }
            return BadRequest(notificacoes);
        }

        [HttpGet("obter-todos")]
        public async Task<IActionResult> ObterItems()
        {
            var items = await _itemService.ObterItemsAsync();
            var notificacoes = _notificationService.GetNotifications();

            if (items != null)
            {
                var resposta = new { items, notificacoes };
                return Ok(resposta);
            }
            return BadRequest(notificacoes);
        }

        [HttpDelete("excluir/{id}")]
        public async Task<IActionResult> ExcluirItem(Guid id)
        {
            var itemExcluido = await _itemService.RemoverItemAsync(id);
            var notificacoes = _notificationService.GetNotifications();

            if (itemExcluido != null)
            {
                var resposta = new { itemExcluido, notificacoes };
                return Ok(resposta);
            }
            return BadRequest(notificacoes);
        }
    }
}
