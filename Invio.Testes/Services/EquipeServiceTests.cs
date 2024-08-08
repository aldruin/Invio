using FluentAssertions;
using Invio.Application.DTOs;
using Invio.Application.Interfaces;
using Invio.Application.Services;
using Invio.Domain.Entities;
using Invio.Domain.Enums;
using Invio.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Invio.Tests.Services
{
    public class EquipeServiceTests
    {
        private readonly Mock<IEquipeRepository> _equipeRepositoryMock;
        private readonly Mock<INotificationService> _notificationServiceMock;
        private readonly EquipeService _equipeService;

        public EquipeServiceTests()
        {
            _equipeRepositoryMock = new Mock<IEquipeRepository>();
            _notificationServiceMock = new Mock<INotificationService>();
            _equipeService = new EquipeService(_equipeRepositoryMock.Object, _notificationServiceMock.Object);
        }

        [Fact]
        public async Task CriarEquipeAsync_DeveRetornarEquipeDto_QuandoCriacaoForBemSucedida()
        {
            // Arrange
            var equipeDto = new EquipeDto { Nome = "Equipe 1", Categoria = EquipeCategoria.Instalacao };
            var equipe = new Equipe { Id = Guid.NewGuid(), Nome = "Equipe 1", Categoria = EquipeCategoria.Instalacao };

            _equipeRepositoryMock.Setup(repo => repo.AdicionarAsync(It.IsAny<Equipe>())).Returns(Task.CompletedTask);

            // Act
            var resultado = await _equipeService.CriarEquipeAsync(equipeDto);

            // Assert
            resultado.Should().NotBeNull();
            resultado!.Nome.Should().Be(equipeDto.Nome);
            resultado.Categoria.Should().Be(equipeDto.Categoria);

            _notificationServiceMock.Verify(ns => ns.AddNotification("EquipeCriada", "Equipe criada com sucesso"), Times.Once);
        }

        [Fact]
        public async Task CriarEquipeAsync_DeveRetornarNull_QuandoValidacaoFalhar()
        {
            // Arrange
            var equipeDto = new EquipeDto(); // DTO vazio para forçar falha na validação

            // Act
            var resultado = await _equipeService.CriarEquipeAsync(equipeDto);

            // Assert
            resultado.Should().BeNull();

            _notificationServiceMock.Verify(ns => ns.AddNotification("EquipeInvalida", It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task RemoverEquipeAsync_DeveRetornarEquipeDto_QuandoExclusaoForBemSucedida()
        {
            // Arrange
            var equipeId = Guid.NewGuid();
            var equipe = new Equipe { Id = equipeId, Nome = "Equipe 1", Categoria = EquipeCategoria.Manutencao };

            _equipeRepositoryMock.Setup(repo => repo.ObterPorIdAsync(equipeId)).ReturnsAsync(equipe);
            _equipeRepositoryMock.Setup(repo => repo.RemoverPorIdAsync(equipeId)).Returns(Task.CompletedTask);

            // Act
            var resultado = await _equipeService.RemoverEquipeAsync(equipeId);

            // Assert
            resultado.Should().NotBeNull();
            resultado!.Id.Should().Be(equipe.Id);

            _notificationServiceMock.Verify(ns => ns.AddNotification("EquipeExcluida", "A equipe foi excluída com sucesso."), Times.Once);
        }

        [Fact]
        public async Task RemoverEquipeAsync_DeveRetornarNull_QuandoEquipeNaoForEncontrada()
        {
            // Arrange
            var equipeId = Guid.NewGuid();

            _equipeRepositoryMock.Setup(repo => repo.ObterPorIdAsync(equipeId)).ReturnsAsync((Equipe?)null);

            // Act
            var resultado = await _equipeService.RemoverEquipeAsync(equipeId);

            // Assert
            resultado.Should().BeNull();

            _notificationServiceMock.Verify(ns => ns.AddNotification("FalhaExcluirEquipe", "A equipe não pode ser encontrada para exclusão"), Times.Once);
        }

        [Fact]
        public async Task ObterEquipeAsync_DeveRetornarListaDeEquipes_QuandoEquipesForemEncontradas()
        {
            // Arrange
            var equipes = new List<Equipe>
            {
                new Equipe { Id = Guid.NewGuid(), Nome = "Equipe 1", Categoria = EquipeCategoria.Instalacao },
                new Equipe { Id = Guid.NewGuid(), Nome = "Equipe 2", Categoria = EquipeCategoria.Manutencao }
            };

            _equipeRepositoryMock.Setup(repo => repo.ObterTodosAsync()).ReturnsAsync(equipes);

            // Act
            var resultado = await _equipeService.ObterEquipeAsync();

            // Assert
            resultado.Should().NotBeNull();
            resultado!.Count.Should().Be(2);
            resultado[0]!.Nome.Should().Be("Equipe 1");
            resultado[1]!.Nome.Should().Be("Equipe 2");
        }

        [Fact]
        public async Task ObterEquipeAsync_DeveRetornarNull_QuandoNenhumaEquipeForEncontrada()
        {
            // Arrange
            _equipeRepositoryMock.Setup(repo => repo.ObterTodosAsync()).ReturnsAsync(new List<Equipe>());

            // Act
            var resultado = await _equipeService.ObterEquipeAsync();

            // Assert
            resultado.Should().BeNull();

            _notificationServiceMock.Verify(ns => ns.AddNotification("EquipesNaoEncontradas", "Nenhuma equipe encontrada."), Times.Once);
        }

        [Fact]
        public async Task ObterEquipePorIdAsync_DeveRetornarEquipeDto_QuandoEquipeForEncontrada()
        {
            // Arrange
            var equipeId = Guid.NewGuid();
            var equipe = new Equipe { Id = equipeId, Nome = "Equipe 1", Categoria = EquipeCategoria.Instalacao };

            _equipeRepositoryMock.Setup(repo => repo.ObterPorIdAsync(equipeId)).ReturnsAsync(equipe);

            // Act
            var resultado = await _equipeService.ObterEquipePorIdAsync(equipeId);

            // Assert
            resultado.Should().NotBeNull();
            resultado!.Nome.Should().Be("Equipe 1");
            resultado.Id.Should().Be(equipeId);
        }

        [Fact]
        public async Task ObterEquipePorIdAsync_DeveRetornarNull_QuandoEquipeNaoForEncontrada()
        {
            // Arrange
            var equipeId = Guid.NewGuid();

            _equipeRepositoryMock.Setup(repo => repo.ObterPorIdAsync(equipeId)).ReturnsAsync((Equipe?)null);

            // Act
            var resultado = await _equipeService.ObterEquipePorIdAsync(equipeId);

            // Assert
            resultado.Should().BeNull();

            _notificationServiceMock.Verify(ns => ns.AddNotification("EquipeNaoEncontrada", "Nenhuma equipe encontrada."), Times.Once);
        }

        [Fact]
        public async Task AtualizarEquipeAsync_DeveRetornarEquipeDto_QuandoAtualizacaoForBemSucedida()
        {
            // Arrange
            var equipeDto = new EquipeDto { Id = Guid.NewGuid(), Nome = "Equipe Atualizada", Categoria = EquipeCategoria.Manutencao };
            var equipe = new Equipe { Id = equipeDto.Id.Value, Nome = "Equipe 1", Categoria = EquipeCategoria.Instalacao };

            _equipeRepositoryMock.Setup(repo => repo.ObterPorIdAsync(equipeDto.Id.Value)).ReturnsAsync(equipe);
            _equipeRepositoryMock.Setup(repo => repo.AtualizarAsync(It.IsAny<Equipe>())).Returns(Task.CompletedTask);

            // Act
            var resultado = await _equipeService.AtualizarEquipeAsync(equipeDto);

            // Assert
            resultado.Should().NotBeNull();
            resultado!.Nome.Should().Be(equipeDto.Nome);

            _notificationServiceMock.Verify(ns => ns.AddNotification("EquipeAtualizada", "Equipe atualizada com sucesso"), Times.Once);
        }

        [Fact]
        public async Task AtualizarEquipeAsync_DeveRetornarNull_QuandoValidacaoFalhar()
        {
            // Arrange
            var equipeDto = new EquipeDto(); // DTO vazio para forçar falha na validação

            // Act
            var resultado = await _equipeService.AtualizarEquipeAsync(equipeDto);

            // Assert
            resultado.Should().BeNull();

            _notificationServiceMock.Verify(ns => ns.AddNotification("EquipeInvalida", It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task AtualizarEquipeAsync_DeveRetornarNull_QuandoEquipeNaoForEncontrada()
        {
            // Arrange
            var equipeDto = new EquipeDto { Id = Guid.NewGuid(), Nome = "Equipe Atualizada", Categoria = EquipeCategoria.Manutencao };

            _equipeRepositoryMock.Setup(repo => repo.ObterPorIdAsync(equipeDto.Id.Value)).ReturnsAsync((Equipe?)null);

            // Act
            var resultado = await _equipeService.AtualizarEquipeAsync(equipeDto);

            // Assert
            resultado.Should().BeNull();

            _notificationServiceMock.Verify(ns => ns.AddNotification("EquipeNaoEncontrada", "A equipe não foi encontrada"), Times.Once);
        }
    }
}
