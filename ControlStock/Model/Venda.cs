using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Venda
    {
        public int Codigo { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
        public double ValorTotal { get; set; }
        public double Desconto { get; set; }
        public string Ativo { get; set; }
    }
}
