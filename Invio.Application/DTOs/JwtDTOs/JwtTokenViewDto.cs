using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Application.DTOs.JwtDTOs
{
    public class JwtTokenViewDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }

}
