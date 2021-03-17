using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuBanco.Models
{
    public class Transferencia
    {
        public int Id { get; set; }
        public int IdClienteRemetente { get; set; }
        public int IdClienteDestinatario { get; set; }
        public decimal Valor { get; set; }

        public DateTime Data { get; set; }
    }
}
