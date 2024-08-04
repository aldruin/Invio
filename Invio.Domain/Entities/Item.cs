using Invio.Domain.Base;
using Invio.Domain.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Domain.Entities
{
    public class Item : Entity
    {
        public string? Nome { get; set; }
        public int? Quantidade { get; set; }
        public string? Descricao { get; set; }
        public ItemCategoria? Categoria { get; set; }
        public Equipe? Equipe { get; set; }
        public Guid? EquipeId { get; set; }
        public DateTime? DataFornecimento { get; set; }
        public DateTime? DataTermino { get; set; }

        public Item() { }
    }
}
