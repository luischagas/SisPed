using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ItemDoPedido
    {
        public int Codigo { get; set; }

        public Pedido Pedido { get; set; }

        public Produto Produto { get; set; }

        public int Quantidade { get; set; }

        public decimal ValorUnitario { get; set; }
    }
}
