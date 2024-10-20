using Invio.Application.DTOs;

namespace Invio.Application.Interfaces
{
  public interface IEmailService
  {
    Task<bool> EnviarEmailAsync(EnviarEmailDto enviarEmail);
  }
}
