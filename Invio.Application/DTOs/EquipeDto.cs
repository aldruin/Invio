using Invio.Domain.Entities;
using Invio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Application.DTOs
{
    public class EquipeDto
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public EquipeCategoria? Categoria { get; set; }
        public List<Item> Items { get; set; }

        public EquipeDto() { }
    }
}
