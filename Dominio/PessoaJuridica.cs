﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class PessoaJuridica : Pessoa
    {
        public string CNPJ { get; set; }

        public bool Ativa { get; set; }
    }

    
}
