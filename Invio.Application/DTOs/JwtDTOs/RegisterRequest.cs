﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Application.DTOs.JwtDTOs
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
