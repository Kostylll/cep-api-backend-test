using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CepApi.Application.Abstraction.Domain.Models
{
    public record Login
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}

