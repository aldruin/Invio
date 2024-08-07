using Invio.Application.DTOs;
using Invio.Application.Interfaces;
using Invio.Application.Validators;
using Invio.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly INotificationService _notificationService;

        public UsuarioService(UserManager<Usuario> usuarioManager, NotificationService notificationService)
        {
            _userManager = usuarioManager;
            _notificationService = notificationService;
        }

        public async Task<UsuarioDto?> CriaUsuarioAsync(UsuarioDto dto)
        {
            var resultadoValidacao = await new UsuarioValidator().ValidateAsync(dto);
            if (!resultadoValidacao.IsValid)
            {
                foreach (var error in resultadoValidacao.Errors)
                    _notificationService.AddNotification("UsuarioInvalido", error.ErrorMessage);
                return null;
            }

            var usuario = new Usuario()
            {
                Email = dto.Email,
                Nome = dto.Nome.Trim().ToLower()
            };

            var resultado = await _userManager.CreateAsync(usuario, dto.Password);
            if (resultado.Succeeded)
            {
                _notificationService.AddNotification("UsuarioCriado", "Usuario criado com sucesso.");
                return new UsuarioDto()
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                };
            }

            foreach (var error in resultado.Errors)
                _notificationService.AddNotification("CriarUsuarioFalhou", $"Falha ao criar o usuario: {error.Description}");
            return null;
        }

        public async Task<UsuarioDto> RemoveUsuarioAsync(ClaimsPrincipal usuario)
        {
            var usuarioLogado = await _userManager.GetUserAsync(usuario);
            if (usuarioLogado == null)
            {
                _notificationService.AddNotification("UsuarioNaoEncontrado", "O usuario nao pode ser encontrado para exclus�o.");
                return null;
            }

            var result = await _userManager.DeleteAsync(usuarioLogado);
            if (result.Succeeded)
            {
                _notificationService.AddNotification("UsuarioRemovido", "O usuario foi excluido com sucesso.");
                return new UsuarioDto()
                {
                    Id = usuarioLogado.Id,
                    Nome = usuarioLogado.Nome,
                    Email = usuarioLogado.Email
                };
            }

            foreach (var error in result.Errors)
                _notificationService.AddNotification("RemoverUsuarioFalhou", $"Falha ao excluir o usuario: {error.Description}");
            return null;
        }

        public async Task<UsuarioDto> AtualizaUsuarioAsync(ClaimsPrincipal usuario, UsuarioDto dto)
        {
            var resultadoValidacao = await new UsuarioValidator().ValidateAsync(dto);
            if (!resultadoValidacao.IsValid)
            {
                foreach (var error in resultadoValidacao.Errors)
                    _notificationService.AddNotification("UsuarioInvalido", error.ErrorMessage);
                return null;
            }

            var usuarioLogado = await _userManager.GetUserAsync(usuario);
            if (usuarioLogado == null)
            {
                _notificationService.AddNotification("UsuarioNaoEncontrado", "O usuario nao pode ser encontrado.");
                return null;
            }
            var updatedUsuario = new Usuario() { Nome = dto.Nome.Trim().ToUpper(), Email = dto.Email, Id = usuarioLogado.Id };
            var result = await _userManager.UpdateAsync(updatedUsuario);
            if (result.Succeeded)
            {
                _notificationService.AddNotification("UsuarioAtualizado", "Os  dados de usuario foram atualizados com sucesso.");
                return new UsuarioDto()
                {
                    Id = usuarioLogado.Id,
                    Nome = usuarioLogado.Nome,
                    Email = usuarioLogado.Email
                };
            }
            foreach (var error in result.Errors)
                _notificationService.AddNotification("AtualizarUsuarioFalhou", $"Falha ao atualizar o usuario: {error.Description}");
            return null;
        }
    }
}
