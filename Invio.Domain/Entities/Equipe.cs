using Invio.Domain.Base;
using Invio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Domain.Entities
{
    public class Equipe : Entity
    {
        public string? Nome { get; set; }
        public EquipeCategoria? Categoria { get; set; }
        public List<Item>? Items { get; set; } = new List<Item>();

        public Equipe() { }
    }
}
