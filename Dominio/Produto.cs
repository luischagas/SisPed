using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Produto
    {
        public int Codigo { get; set; }

        public string Nome { get; set; }

        public decimal Preco { get; set; }

        public PessoaJuridica Fornecedor { get; set; }
    }
}
