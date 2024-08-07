using Invio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Application.Interfaces
{
    public interface IEquipeService
    {
        Task<EquipeDto?> CriarEquipeAsync(EquipeDto dto);
        Task<List<EquipeDto?>> ObterEquipeAsync();
        Task<EquipeDto?> ObterEquipePorIdAsync(Guid id);
        Task<EquipeDto?> AtualizarEquipeAsync(EquipeDto dto);
        Task<EquipeDto?> RemoverEquipeAsync(Guid id);
    }
}
