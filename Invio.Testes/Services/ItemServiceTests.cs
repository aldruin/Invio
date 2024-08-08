using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Invio.Application.DTOs;
using Invio.Application.Interfaces;
using Invio.Application.Services;
using Invio.Domain.Entities;
using Invio.Domain.Enums;
using Invio.Domain.Repositories;
using Xunit;

namespace Invio.Tests
{
    public class ItemServiceTests
    {
        private readonly Mock<IItemRepository> _itemRepositoryMock;
        private readonly Mock<IEquipeRepository> _equipeRepositoryMock;
        private readonly Mock<INotificationService> _notificationServiceMock;
        private readonly ItemService _itemService;

        public ItemServiceTests()
        {
            _itemRepositoryMock = new Mock<IItemRepository>();
            _equipeRepositoryMock = new Mock<IEquipeRepository>();
            _notificationServiceMock = new Mock<INotificationService>();
            _itemService = new ItemService(_itemRepositoryMock.Object, _equipeRepositoryMock.Object, _notificationServiceMock.Object);
        }

        [Fact]
        public async Task CriarItemAsync_Valido_DeveCriarItem()
        {
            // Arrange
            var dto = new ItemDto
            {
                Nome = "Item 1",
                Quantidade = 10,
                Descricao = "Descrição do item",
                Categoria = ItemCategoria.Consumivel.ToString(),
                EquipeId = Guid.NewGuid(),
                DataFornecimento = DateTime.Now
            };

            _itemRepositoryMock.Setup(r => r.AdicionarAsync(It.IsAny<Item>())).Returns(Task.CompletedTask);
            _equipeRepositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Equipe { Items = new List<Item>() });

            // Act
            var result = await _itemService.CriarItemAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.Nome.Should().Be(dto.Nome);
            result.Quantidade.Should().Be(dto.Quantidade);
            result.Descricao.Should().Be(dto.Descricao);
            result.Categoria.Should().Be(dto.Categoria);
        }

        [Fact]
        public async Task AtualizarItemAsync_Valido_DeveAtualizarItem()
        {
            // Arrange
            var itemId = Guid.NewGuid();
            var dto = new ItemDto
            {
                Id = itemId,
                Nome = "Item Atualizado",
                Quantidade = 20,
                Descricao = "Descrição atualizada",
                Categoria = ItemCategoria.Permanente.ToString(),
                EquipeId = Guid.NewGuid(),
                DataFornecimento = DateTime.Now,
                DataTermino = DateTime.Now.AddDays(10)
            };

            var item = new Item
            {
                Id = itemId,
                Nome = "Item Original",
                Quantidade = 10,
                Descricao = "Descrição original",
                Categoria = ItemCategoria.Consumivel,
                EquipeId = Guid.NewGuid(),
                DataFornecimento = DateTime.Now
            };

            _itemRepositoryMock.Setup(r => r.ObterPorIdAsync(itemId)).ReturnsAsync(item);
            _itemRepositoryMock.Setup(r => r.AtualizarAsync(It.IsAny<Item>())).Returns(Task.CompletedTask);

            // Act
            var result = await _itemService.AtualizarItemAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.Nome.Should().Be(dto.Nome);
            result.Quantidade.Should().Be(dto.Quantidade);
            result.Descricao.Should().Be(dto.Descricao);
            result.Categoria.Should().Be(dto.Categoria);
            result.DataTermino.Should().Be(dto.DataTermino);
        }

        [Fact]
        public async Task ObterItemPorEquipeIdAsync_Existente_DeveRetornarItens()
        {
            // Arrange
            var equipeId = Guid.NewGuid();
            var items = new List<Item>
            {
                new Item { Id = Guid.NewGuid(), Nome = "Item 1", Quantidade = 5, Categoria = ItemCategoria.Consumivel },
                new Item { Id = Guid.NewGuid(), Nome = "Item 2", Quantidade = 10, Categoria = ItemCategoria.Permanente }
            };

            _itemRepositoryMock.Setup(r => r.ObterItemsPorEquipeIdAsync(equipeId)).ReturnsAsync(items);

            // Act
            var result = await _itemService.ObterItemPorEquipeIdAsync(equipeId);

            // Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(2);
            result[0].Nome.Should().Be("Item 1");
            result[1].Nome.Should().Be("Item 2");
        }

        [Fact]
        public async Task RemoverItemAsync_Existente_DeveRemoverItem()
        {
            // Arrange
            var itemId = Guid.NewGuid();
            var item = new Item { Id = itemId, Nome = "Item a ser removido" };

            _itemRepositoryMock.Setup(r => r.ObterPorIdAsync(itemId)).ReturnsAsync(item);
            _itemRepositoryMock.Setup(r => r.RemoverPorIdAsync(itemId)).Returns(Task.CompletedTask);

            // Act
            var result = await _itemService.RemoverItemAsync(itemId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(itemId);
            result.Nome.Should().Be(item.Nome);
        }
    }
}
