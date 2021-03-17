using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuBanco.Models
{
    public class Deposito
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public decimal Valor { get; set; }

        public DateTime Data { get; set; }
    }
}
