using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Models
{
    public class Usuario
    {
        public string? NomeCompleto { get; set; }
        public string? Endereco { get; set; }
        public string? Rg { get; set; }
        public string? Cpf { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}
