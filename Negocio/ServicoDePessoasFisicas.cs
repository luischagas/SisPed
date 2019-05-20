using DAO;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ServicoDePessoasFisicas
    {
        private readonly RepositorioDePessoasFisicas repositorioDePessoas;

        public ServicoDePessoasFisicas()
        {
            repositorioDePessoas = new RepositorioDePessoasFisicas();
        }

        public void CadastraPessoa(PessoaFisica pessoa)
        {
            repositorioDePessoas.Add(pessoa);
        }

        public PessoaFisica ObtemPessoa(int id)
        {
            PessoaFisica pessoa = new PessoaFisica();

            pessoa = repositorioDePessoas.ObtemPessoa(id);

            return pessoa;
        }

        public List<PessoaFisica> ListaPessoas()
        {
            List<PessoaFisica> pLista = new List<PessoaFisica>();

            pLista = repositorioDePessoas.ObtemPessoasFisicas();

            return pLista.ToList();
        }

        public void AlteraPessoa(PessoaFisica pessoa)
        {
            repositorioDePessoas.Update(pessoa);
        }

        public void RemovePessoa(PessoaFisica pessoa)
        {
            repositorioDePessoas.Remove(pessoa);
        }

        public PessoaFisica ObtemPedidoPessoa(int id)
        {
            PessoaFisica pessoa = new PessoaFisica();

            pessoa = repositorioDePessoas.ObtemPedidoPessoa(id);

            return pessoa;
        }

    }
}
