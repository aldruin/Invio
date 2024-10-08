﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Application.DTOs.JwtDTOs
{
    public class JwtDto
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string Nome { get; private set; }


        public JwtDto(Guid id, string email, string nome)
        {
            Id = id;
            Nome = nome;
            Email = email;
        }
    }
}
