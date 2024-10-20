using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Invio.Application.Interfaces;
using Invio.Application.DTOs;
using Microsoft.Extensions.Configuration;

namespace Invio.Application.Services
{
  public class EmailService : IEmailService
  {
    private readonly IConfiguration _config;
    public EmailService(IConfiguration config)
    {
      _config = config;
    }
    
    public async Task<bool> EnviarEmailAsync(EnviarEmailDto enviarEmail)
    {
      MailjetClient client = new MailjetClient(_config["MailJet:ApiKey"], _config["MailJet:SecretKey"]);
      
      var email = new TransactionalEmailBuilder()
        .WithFrom(new SendContact(_config["Email:From"], _config["Email:ApplicationName"]))
        .WithSubject(enviarEmail.Assunto)
        .WithHtmlPart(enviarEmail.Corpo)
        .WithTo(new SendContact(enviarEmail.Para))
        .Build();
      
      var response = await client.SendTransactionalEmailAsync(email);
      if(response.Messages != null)
      {
        if (response.Messages[0].Status == "success")
          return true;
      }

      return false;
    }
  }
}