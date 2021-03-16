using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuBanco.Models
{
    public class Usuario
    {
        public string Id { get; set; }

        public string UserName { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public int IdPerfil { get; set; }
        public string Nome { get; set; }
    }
}
