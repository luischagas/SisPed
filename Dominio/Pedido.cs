using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Pedido
    {
        public int Codigo { get; set; }

        public PessoaFisica Comprador { get; set; }

        public PessoaJuridica Vendedor { get; set; }

        public DateTime DataPedido { get; set; }

        public ItemDoPedido Item { get; set; }
    }
}
