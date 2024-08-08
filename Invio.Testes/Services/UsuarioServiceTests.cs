using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using Invio.Application.DTOs;
using Invio.Application.Interfaces;
using Invio.Application.Services;
using Invio.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace Invio.Tests.Services
{
    public class UsuarioServiceTests
    {
        private readonly Mock<UserManager<Usuario>> _userManagerMock;
        private readonly Mock<INotificationService> _notificationServiceMock;
        private readonly UsuarioService _usuarioService;

        public UsuarioServiceTests()
        {
            var userStoreMock = new Mock<IUserStore<Usuario>>();
            _userManagerMock = new Mock<UserManager<Usuario>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            _notificationServiceMock = new Mock<INotificationService>();

            _usuarioService = new UsuarioService(_userManagerMock.Object, _notificationServiceMock.Object);
        }

        [Fact]
        public async Task CriaUsuarioAsync_DeveRetornarUsuarioDto_QuandoUsuarioEhCriadoComSucesso()
        {
            // Arrange
            var usuarioDto = new UsuarioDto { Email = "teste@example.com", Nome = "Teste", Password = "Senha123!" };

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<Usuario>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var resultado = await _usuarioService.CriaUsuarioAsync(usuarioDto);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Email.Should().Be("teste@example.com");
            resultado.Nome.Should().Be("TESTE");

            _userManagerMock.Verify(x => x.CreateAsync(It.IsAny<Usuario>(), It.IsAny<string>()), Times.Once);
            _notificationServiceMock.Verify(n => n.AddNotification("UsuarioCriado", "Usuario criado com sucesso."), Times.Once);
        }

        [Fact]
        public async Task RemoveUsuarioAsync_DeveRetornarUsuarioDto_QuandoUsuarioEhRemovidoComSucesso()
        {
            // Arrange
            var usuario = new Usuario { Id = Guid.NewGuid(), Nome = "TESTE", Email = "teste@example.com" };
            var claimsPrincipal = new ClaimsPrincipal();

            _userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(usuario);

            _userManagerMock.Setup(x => x.DeleteAsync(It.IsAny<Usuario>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var resultado = await _usuarioService.RemoveUsuarioAsync(claimsPrincipal);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Id.Should().Be(usuario.Id);
            resultado.Email.Should().Be(usuario.Email);

            _userManagerMock.Verify(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);
            _userManagerMock.Verify(x => x.DeleteAsync(It.IsAny<Usuario>()), Times.Once);
            _notificationServiceMock.Verify(n => n.AddNotification("UsuarioRemovido", "O usuario foi excluido com sucesso."), Times.Once);
        }

        [Fact]
        public async Task AtualizaUsuarioAsync_DeveRetornarUsuarioDto_QuandoUsuarioEhAtualizadoComSucesso()
        {
            // Arrange
            var usuarioDto = new UsuarioDto { Nome = "Novo Nome", Email = "novo@example.com" };
            var usuario = new Usuario { Id = Guid.NewGuid(), Nome = "TESTE", Email = "teste@example.com" };
            var claimsPrincipal = new ClaimsPrincipal();

            _userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(usuario);

            _userManagerMock.Setup(x => x.UpdateAsync(It.IsAny<Usuario>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var resultado = await _usuarioService.AtualizaUsuarioAsync(claimsPrincipal, usuarioDto);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Nome.Should().Be("NOVO NOME");
            resultado.Email.Should().Be("novo@example.com");

            _userManagerMock.Verify(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);
            _userManagerMock.Verify(x => x.UpdateAsync(It.IsAny<Usuario>()), Times.Once);
            _notificationServiceMock.Verify(n => n.AddNotification("UsuarioAtualizado", "Os  dados de usuario foram atualizados com sucesso."), Times.Once);
        }
    }
}


//[Fact]
//public async Task AtualizaUsuarioAsync_DeveRetornarUsuarioDto_QuandoUsuarioEhAtualizadoComSucesso()
//{
//    // Arrange
//    var usuarioDto = new UsuarioDto { Nome = "Novo Nome", Email = "novo@example.com", Password = "Senha123!" };
//    var usuario = new Usuario { Id = Guid.NewGuid().ToString(), Nome = "TESTE", Email = "teste@example.com" };
//    var claimsPrincipal = new ClaimsPrincipal();

//    _userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
//        .ReturnsAsync(usuario);

//    _userManagerMock.Setup(x => x.UpdateAsync(It.IsAny<Usuario>()))
//        .ReturnsAsync(IdentityResult.Success);

//    // Act
//    var resultado = await _usuarioService.AtualizaUsuarioAsync(claimsPrincipal, usuarioDto);

//    // Assert
//    resultado.Should().NotBeNull("O resultado não deve ser nulo quando a atualização for bem-sucedida.");
//    resultado.Nome.Should().Be("NOVO NOME");
//    resultado.Email.Should().Be("novo@example.com");

//    _userManagerMock.Verify(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);
//    _userManagerMock.Verify(x => x.UpdateAsync(It.IsAny<Usuario>()), Times.Once);
//    _notificationServiceMock.Verify(n => n.AddNotification("UsuarioAtualizado", "Os  dados de usuario foram atualizados com sucesso."), Times.Once);
//}
