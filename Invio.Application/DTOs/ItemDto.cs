using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Application.DTOs
{
    public class ItemDto
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public int? Quantidade { get; set; }
        public string? Descricao { get; set; }
        public string? Categoria { get; set; }
        public Guid? EquipeId { get; set; }
        public DateTime? DataFornecimento { get; set; }
        public DateTime? DataTermino { get; set; }

        public ItemDto() { }
    }
}
