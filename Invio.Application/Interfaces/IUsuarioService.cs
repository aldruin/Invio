using Invio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDto?> CriaUsuarioAsync(UsuarioDto dto);
        Task<UsuarioDto?> RemoveUsuarioAsync(ClaimsPrincipal user);
        Task<UsuarioDto?> AtualizaUsuarioAsync(ClaimsPrincipal user, UsuarioDto dto);
    }
}
