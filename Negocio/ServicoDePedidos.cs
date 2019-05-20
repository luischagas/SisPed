using DAO;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ServicoDePedidos
    {
        private readonly RepositorioDePessoasJuridicas repositorioDePessoas;

        private readonly RepositorioDePedidos repositorioDePedidos;

        public ServicoDePedidos()
        {
            repositorioDePessoas = new RepositorioDePessoasJuridicas();
            repositorioDePedidos = new RepositorioDePedidos();
        }

        public void CadastraPedido(Pedido Pedido)
        {
            repositorioDePedidos.Add(Pedido);
        }

        public Pedido ObtemPedido(int id)
        {
            Pedido Pedido = new Pedido();

            Pedido = repositorioDePedidos.ObtemPedido(id);

            return Pedido;
        }

        public List<Pedido> ListarPedidos()
        {
            List<Pedido> pLista = new List<Pedido>();

            pLista = repositorioDePedidos.ObtemPedidos();

            return pLista.ToList();
        }

        public void AlteraPedido(Pedido Pedido)
        {
            repositorioDePedidos.Update(Pedido);
        }

        public void RemovePedido(Pedido Pedido)
        {
            repositorioDePedidos.Remove(Pedido);
        }

    }
}
