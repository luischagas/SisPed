using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Pessoa
    {
        public int Codigo { get; set; }

        public string Nome { get; set; }

        public string Endereco { get; set; }
    }
}
