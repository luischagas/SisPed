using DAO;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ServicoDePessoasJuridicas
    {
        private readonly RepositorioDePessoasJuridicas repositorioDePessoas;

        public ServicoDePessoasJuridicas()
        {
            repositorioDePessoas = new RepositorioDePessoasJuridicas();
        }

        public void CadastraPessoa(PessoaJuridica pessoa)
        {
            repositorioDePessoas.Add(pessoa);
        }

        public PessoaJuridica ObtemPessoa(int id)
        {
            PessoaJuridica pessoa = new PessoaJuridica();

            pessoa = repositorioDePessoas.ObtemPessoa(id);

            return pessoa;
        }

        public List<PessoaJuridica> ListaPessoas()
        {
            List<PessoaJuridica> pLista = new List<PessoaJuridica>();

            pLista = repositorioDePessoas.ObtemPessoasJuridicas();

            return pLista.ToList();
        }

        public void AlteraPessoa(PessoaJuridica pessoa)
        {
            repositorioDePessoas.Update(pessoa);
        }

        public void RemovePessoa(PessoaJuridica pessoa)
        {
            repositorioDePessoas.Remove(pessoa);
        }

        public PessoaJuridica ObtemProdutoPessoa(int id)
        {
            PessoaJuridica pessoa = new PessoaJuridica();

            pessoa = repositorioDePessoas.ObtemProdutoPessoa(id);

            return pessoa;
        }

    }
}
