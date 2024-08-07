using Invio.Application.DTOs;
using Invio.Application.Interfaces;
using Invio.Application.Validators;
using Invio.Domain.Entities;
using Invio.Domain.Enums;
using Invio.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IEquipeRepository _equipeRepository;
        private readonly INotificationService _notificationService;

        public ItemService(IItemRepository itemRepository, IEquipeRepository equipeRepository, INotificationService notificationService)
        {
            _itemRepository = itemRepository;
            _equipeRepository = equipeRepository;
            _notificationService = notificationService;
        }

        public async Task<ItemDto?> AtualizarItemAsync(ItemDto dto)
        {
            try
            {
                var validacao = await new ItemValidator().ValidateAsync(dto);
                if (!validacao.IsValid)
                {
                    foreach (var error in validacao.Errors)
                        _notificationService.AddNotification("ItemInvalido.", error.ErrorMessage);
                    return null;
                }

                var item = await _itemRepository.ObterPorIdAsync(dto.Id);
                if (item == null)
                {
                    _notificationService.AddNotification("ItemNaoEncontrado", "O item não foi encontrado.");
                    return null;
                }

                item.Nome = dto.Nome;
                item.Quantidade = dto.Quantidade;
                item.Descricao = dto.Descricao;
                item.Categoria = Enum.TryParse<ItemCategoria>(dto.Categoria, out var itemClass) ? itemClass : (ItemCategoria?)null;
                item.EquipeId = dto.EquipeId;
                item.DataFornecimento = dto.DataFornecimento;
                item.DataTermino = dto.DataTermino;

                await _itemRepository.AtualizarAsync(item);

                _notificationService.AddNotification("ItemAtualizado", "Item atualizado com sucesso.");
                return new ItemDto()
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Quantidade = item.Quantidade,
                    Descricao = item.Descricao,
                    Categoria = item.Categoria?.ToString(),
                    EquipeId = item.EquipeId,
                    DataFornecimento = item.DataFornecimento,
                    DataTermino = item.DataTermino
                };
            }
            catch (Exception ex)
            {
                _notificationService.AddNotification("AtualizarItemFalhou", $"Falha ao atualizar o item: {ex.Message}");
                return null;
            }
        }

        public async Task<ItemDto?> CriarItemAsync(ItemDto dto)
        {
            try
            {
                var validacao = await new ItemValidator().ValidateAsync(dto);
                if (!validacao.IsValid)
                {
                    foreach (var error in validacao.Errors)
                        _notificationService.AddNotification("ItemInvalido", error.ErrorMessage);
                    return null;
                }

                var item = new Item()
                {
                    Nome = dto.Nome,
                    Quantidade = dto.Quantidade,
                    Descricao = dto.Descricao,
                    Categoria = Enum.TryParse<ItemCategoria>(dto.Categoria, out var itemCategoria) ? itemCategoria : (ItemCategoria?)null,
                    EquipeId = dto.EquipeId,
                    DataFornecimento = dto.DataFornecimento,
                    DataTermino = dto.DataTermino
                };

                await _itemRepository.AdicionarAsync(item);

                var equipe = await _equipeRepository.ObterPorIdAsync(item.EquipeId);
                equipe.Items.Add(item);

                _notificationService.AddNotification("ItemCriado", "Item criado com sucesso.");
                return new ItemDto()
                {
                    Id = item.Id,
                    Quantidade = item.Quantidade,
                    Descricao = item.Descricao,
                    Categoria = item.Categoria.ToString(),
                    EquipeId = item.EquipeId,
                    DataFornecimento = item.DataFornecimento,
                    DataTermino = item.DataTermino
                };

            }
            catch (Exception Ex)
            {
                _notificationService.AddNotification("CriarItemFalhou", $"Falha ao criar o item: {Ex.Message}");
                return null;
            }
        }

        public async Task<List<ItemDto?>> ObterItemPorEquipeIdAsync(Guid equipeId)
        {
            try
            {
                var items = await _itemRepository.ObterItemsPorEquipeIdAsync(equipeId);

                if (!items.Any())
                {
                    _notificationService.AddNotification("FalhaObterItems", "Nenhum item encontrado para a equipe especificada.");
                    return null;
                }

                var itemDtoList = items.Select(item => new ItemDto
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Quantidade = item.Quantidade,
                    Descricao = item.Descricao,
                    Categoria = item.Categoria?.ToString(),
                    EquipeId = item.EquipeId,
                    DataFornecimento = item.DataFornecimento,
                    DataTermino = item.DataTermino
                }).ToList();
                return itemDtoList;
            }
            catch (Exception ex)
            {
                _notificationService.AddNotification("FalhaObterItems", $"Falha ao buscar os itens: {ex.Message}");
                return null;
            }


        }

        public async Task<List<ItemDto?>> ObterItemsAsync()
        {
            try
            {
                var items = await _itemRepository.ObterTodosAsync();

                if (items == null || !items.Any())
                {
                    _notificationService.AddNotification("ObterItemsFalhou", "Nenhum item encontrado.");
                    return null;
                }

                var itemDtoList = items.Select(item => new ItemDto
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Quantidade = item.Quantidade,
                    Descricao = item.Descricao,
                    Categoria = item.Categoria?.ToString(),
                    EquipeId = item.EquipeId,
                    DataFornecimento = item.DataFornecimento,
                    DataTermino = item.DataTermino
                }).ToList();

                return itemDtoList;
            }
            catch (Exception ex)
            {
                _notificationService.AddNotification("ObterItemsFalhou", $"Falha ao buscar os itens: {ex.Message}");
                return null;
            }
        }

        public async Task<ItemDto?> RemoverItemAsync(Guid id)
        {
            try
            {
                var item = await _itemRepository.ObterPorIdAsync(id);
                if (item == null)
                {
                    _notificationService.AddNotification("FalhaRemoverItem", "O item não pode ser encontrado para exclusão");
                    return null;
                }

                await _itemRepository.RemoverPorIdAsync(id);
                _notificationService.AddNotification("ItemRemovido", "Item excluído com sucesso.");
                return new ItemDto()
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Quantidade = item.Quantidade,
                    Descricao = item.Descricao,
                    Categoria = item.Categoria?.ToString(),
                    EquipeId = item.EquipeId,
                    DataFornecimento = item.DataFornecimento,
                    DataTermino = item.DataTermino
                };
            }
            catch (Exception ex)
            {
                _notificationService.AddNotification("FalhaRemoverItem", $"Falha ao deletar o item: {ex.Message}");
                return null;
            }
        }
    }
}
