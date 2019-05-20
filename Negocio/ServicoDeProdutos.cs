using DAO;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ServicoDeProdutos
    {
        private readonly RepositorioDePessoasJuridicas repositorioDePessoas;

        private readonly RepositorioDeProdutos repositorioDeProdutos;

        public ServicoDeProdutos()
        {
            repositorioDePessoas = new RepositorioDePessoasJuridicas();
            repositorioDeProdutos = new RepositorioDeProdutos();
        }

        public void CadastraProduto(Produto produto)
        {
            repositorioDeProdutos.Add(produto);
        }

        public Produto ObtemProduto(int id)
        {
            Produto produto = new Produto();

            produto = repositorioDeProdutos.ObtemProduto(id);

            return produto;
        }

        public List<Produto> ListarProdutos()
        {
            List<Produto> pLista = new List<Produto>();

            pLista = repositorioDeProdutos.ObtemProdutos();

            return pLista.ToList();
        }

        public void AlteraProduto(Produto produto)
        {
            repositorioDeProdutos.Update(produto);
        }

        public void RemoveProduto(Produto produto)
        {
            repositorioDeProdutos.Remove(produto);
        }

        public Produto ObtemPedidoProduto(int id)
        {
            Produto produto = new Produto();

            produto = repositorioDeProdutos.ObtemPedidoProduto(id);

            return produto;
        }

    }
}
