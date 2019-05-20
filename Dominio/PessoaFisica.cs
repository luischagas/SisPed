using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class PessoaFisica : Pessoa
    {
        public string CPF { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Sexo { get; set; }
    }
}
