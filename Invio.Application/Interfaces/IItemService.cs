using Invio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Application.Interfaces
{
    public interface IItemService
    {
        Task<ItemDto?> CriarItemAsync(ItemDto dto);
        Task<List<ItemDto?>> ObterItemPorEquipeIdAsync(Guid equipeId);
        Task<List<ItemDto?>> ObterItemsAsync();
        Task<ItemDto?> RemoverItemAsync(Guid id);
        Task<ItemDto?> AtualizarItemAsync(ItemDto dto);
    }
}
