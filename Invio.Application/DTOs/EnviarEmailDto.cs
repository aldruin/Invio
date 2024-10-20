namespace Invio.Application.DTOs
{
  public class EnviarEmailDto
  {
    public EnviarEmailDto( string para, string assunto, string corpo){}

    public string Para {get; set;}
    public string Assunto {get; set;}
    public string Corpo {get; set;}
  } 
}