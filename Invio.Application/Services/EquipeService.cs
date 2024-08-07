using Invio.Application.DTOs;
using Invio.Application.Interfaces;
using Invio.Application.Validators;
using Invio.Domain.Entities;
using Invio.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Application.Services
{
    public class EquipeService : IEquipeService
    {
        private readonly IEquipeRepository _equipeRepository;
        private readonly INotificationService _notificationService;

        public EquipeService(IEquipeRepository equipeRepository, INotificationService notificationService)
        {
            _equipeRepository = equipeRepository;
            _notificationService = notificationService;
        }

        public async Task<EquipeDto?> CriarEquipeAsync(EquipeDto dto)
        {
            try
            {
                var validacao = await new EquipeValidator().ValidateAsync(dto);
                if (!validacao.IsValid)
                {
                    foreach (var erro in validacao.Errors)
                        _notificationService.AddNotification("EquipeInvalida", erro.ErrorMessage);
                    return null;
                }

                var equipe = new Equipe()
                {
                    Nome = dto.Nome,
                    Categoria = dto.Categoria
                };

                await _equipeRepository.AdicionarAsync(equipe);

                _notificationService.AddNotification("EquipeCriada", "Equipe criada com sucesso");
                return new EquipeDto()
                {
                    Nome = equipe.Nome,
                    Categoria = equipe.Categoria
                };
            }
            catch (Exception ex)
            {
                _notificationService.AddNotification("FalhaCriarEquipe", $"Falha ao criar a equipe: {ex.Message}");
                return null;
            }
        }

        public async Task<EquipeDto?> RemoverEquipeAsync(Guid id)
        {
            try
            {
                var equipe = await _equipeRepository.ObterPorIdAsync(id);
                if (equipe == null)
                {
                    _notificationService.AddNotification("FalhaExcluirEquipe", "A equipe não pode ser encontrada para exclusão");
                    return null;
                }

                await _equipeRepository.RemoverPorIdAsync(id);

                _notificationService.AddNotification("EquipeExcluida", "A equipe foi excluída com sucesso.");
                return new EquipeDto()
                {
                    Nome = equipe.Nome,
                    Categoria = equipe.Categoria
                };
            }
            catch (Exception ex)
            {
                _notificationService.AddNotification("FalhaExcluirEquipe", $"Falha ao deletar a equipe: {ex.Message}");
                return null;
            }
        }

        public async Task<List<EquipeDto?>> ObterEquipeAsync()
        {
            try
            {
                var equipes = await _equipeRepository.ObterTodosAsync();
                if (!equipes.Any())
                {
                    _notificationService.AddNotification("EquipesNaoEncontradas", "Nenhuma equipe encontrada.");
                    return null;
                }

                var equipesDto = equipes.Select(equipe => new EquipeDto
                {
                    Id = equipe.Id,
                    Nome = equipe.Nome,
                    Categoria = equipe.Categoria,
                    Items = equipe.Items
                }).ToList();

                return equipesDto;
            }
            catch (Exception ex)
            {
                _notificationService.AddNotification("FalhaBuscarEquipes", $"Falha ao buscar as equipes: {ex.Message}");
                return null;
            }
        }

        public async Task<EquipeDto?> ObterEquipePorIdAsync(Guid id)
        {
            try
            {
                var equipe = await _equipeRepository.ObterPorIdAsync(id);
                if (equipe == null)
                {
                    _notificationService.AddNotification("EquipeNaoEncontrada", "Nenhuma equipe encontrada.");
                    return null;
                }
                return new EquipeDto()
                {
                    Nome = equipe.Nome,
                    Id = equipe.Id,
                    Categoria = equipe.Categoria,
                    Items = equipe.Items
                };
            }
            catch (Exception ex)
            {
                _notificationService.AddNotification("FalhaBuscarEquipe", $"Falha ao buscar a equipe: {ex.Message}");
                return null;
            }
        }

        public async Task<EquipeDto?> AtualizarEquipeAsync(EquipeDto dto)
        {
            try
            {
                var validacao = await new EquipeValidator().ValidateAsync(dto);
                if (!validacao.IsValid)
                {
                    foreach (var erro in validacao.Errors)
                        _notificationService.AddNotification("EquipeInvalida", erro.ErrorMessage);
                    return null;
                }

                var equipe = await _equipeRepository.ObterPorIdAsync(dto.Id);
                if (equipe == null)
                {
                    _notificationService.AddNotification("EquipeNaoEncontrada", "A equipe não foi encontrada");
                    return null;
                }

                equipe.Nome = dto.Nome;
                equipe.Categoria = dto.Categoria;

                await _equipeRepository.AtualizarAsync(equipe);

                _notificationService.AddNotification("EquipeAtualizada", "Equipe atualizada com sucesso");
                return new EquipeDto()
                {
                    Nome = equipe.Nome,
                    Categoria = equipe.Categoria,
                    Items = equipe.Items
                };
            }
            catch (Exception ex)
            {
                _notificationService.AddNotification("FalhaAtualizarEquipe", $"Falha ao atualizar a equipe: {ex.Message}");
                return null;
            }
        }
    }
}
