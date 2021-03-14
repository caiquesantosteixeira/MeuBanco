using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuBanco.Models
{
    public class Saque
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public decimal Valor { get; set; }

    }
}
