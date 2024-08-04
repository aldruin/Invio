﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Application.DTOs
{
    public class UsuarioDto
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public UsuarioDto() { }
    }
}
