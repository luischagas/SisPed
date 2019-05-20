using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class RepositorioDePedidos
    {

        public List<Pedido> ObtemPedidos()
        {
            List<Pedido> listPedido = new List<Pedido>();

            SqlConnection con = ConnectionSQL.AbrirConexao();

            using (SqlCommand command = new SqlCommand("select p.CodigoPedido, p.CodigoComprador, p.CodigoVendedor, p.DataPedido, i.CodigoItem, i.CodigoProduto, i.Qtd, i.ValorUnitario, pe.Nome as 'NomeComprador', pe2.Nome as 'NomeVendedor', pt.Nome as 'NomeProduto' from pedido p inner join item i on p.CodigoPedido = i.CodigoPedido inner join Pessoa pe on p.CodigoComprador = pe.CodigoPessoa inner join produto pt on i.CodigoProduto = pt.CodigoProduto inner join Pessoa pe2 on p.CodigoVendedor = pe2.CodigoPessoa", con))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    listPedido.Add(new Pedido()
                    {
                        Codigo = Convert.ToInt32(reader["CodigoPedido"]),
                        Comprador = new PessoaFisica()
                        {
                            Codigo = Convert.ToInt32(reader["CodigoComprador"]),
                            Nome = reader["NomeComprador"].ToString(),
                        },
                        Vendedor = new PessoaJuridica()
                        {
                            Codigo = Convert.ToInt32(reader["CodigoVendedor"]),
                            Nome = reader["NomeVendedor"].ToString(),
                        },
                        DataPedido = Convert.ToDateTime(reader["DataPedido"]),
                        Item = new ItemDoPedido()
                        {
                            Codigo = Convert.ToInt32(reader["CodigoItem"]),
                            Quantidade = Convert.ToInt32(reader["Qtd"]),
                            ValorUnitario = Convert.ToInt32(reader["ValorUnitario"]),
                            Produto = new Produto()
                            {
                                Codigo = Convert.ToInt32(reader["CodigoProduto"]),
                                Nome = reader["NomeProduto"].ToString(),
                            }
                        },

                    });


                }
            }

            ConnectionSQL.FecharConexao();

            return listPedido;
        }

        public Pedido ObtemPedido(int id)
        {
            Pedido Pedido = new Pedido();

            string SQL = "select p.CodigoPedido, CodigoComprador, CodigoVendedor, DataPedido, CodigoProduto, CodigoItem, Qtd, ValorUnitario from pedido p inner join item i on p.CodigoPedido = i.CodigoPedido where p.CodigoPedido = " + id;

            Dictionary<string, string> registro = ConnectionSQL.ExecutarComandoLeituraSQL(SQL);

            Pedido.Codigo = Convert.ToInt32(registro["CodigoPedido"]);
            Pedido.Comprador = new PessoaFisica()
            {
                Codigo = Convert.ToInt32(registro["CodigoComprador"])
            };
            Pedido.Vendedor = new PessoaJuridica()
            {
                Codigo = Convert.ToInt32(registro["CodigoVendedor"])
            };
            Pedido.DataPedido = Convert.ToDateTime(registro["DataPedido"]);
            Pedido.Item = new ItemDoPedido()
            {
                Codigo = Convert.ToInt32(registro["CodigoItem"]),
                Quantidade = Convert.ToInt32(registro["Qtd"]),
                ValorUnitario = Convert.ToInt32(registro["ValorUnitario"]),
                Produto = new Produto()
                {
                    Codigo = Convert.ToInt32(registro["CodigoProduto"])
                }
            };
            return Pedido;
        }

        public void Add(Pedido pedido)
        {
            string SQL2 = "";

            string SQL = "INSERT INTO Pedido (CodigoComprador, CodigoVendedor, DataPedido) Values ("
              + pedido.Comprador.Codigo + ", " + pedido.Vendedor.Codigo + ", '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "') SELECT SCOPE_IDENTITY()";

            pedido.Codigo = ConnectionSQL.ExecutarComandoSQL(SQL);

            SQL2 = "INSERT INTO Item Values ("
         + pedido.Codigo + ", '" + pedido.Item.Produto.Codigo + "', '" + pedido.Item.Quantidade + "', '" + pedido.Item.ValorUnitario + "')";

            ConnectionSQL.ExecutarComandoSQL(SQL2);
        }

        public void Update(Pedido pedido)
        {
            string SQL = "UPDATE Pedido SET CodigoComprador = '" + pedido.Comprador.Codigo +
              "', CodigoVendedor = " + pedido.Vendedor.Codigo +
              ", DataPedido = '" + pedido.DataPedido.ToString("yyyy-MM-dd hh:mm:ss") +
              "' WHERE CodigoPedido = " + pedido.Codigo;

            ConnectionSQL.ExecutarComandoSQL(SQL);

            string SQL2 = "UPDATE Item SET CodigoProduto = '" + pedido.Item.Produto.Codigo +
             "', Qtd = " + pedido.Item.Quantidade +
             ", ValorUnitario = " + pedido.Item.ValorUnitario +
             " WHERE CodigoPedido = " + pedido.Codigo;

            ConnectionSQL.ExecutarComandoSQL(SQL2);
        }

        public void Remove(Pedido pedido)
        {
            string SQL = "DELETE FROM Item WHERE CodigoPedido = " + pedido.Codigo;

            ConnectionSQL.ExecutarComandoSQL(SQL);

            string SQL2 = "DELETE FROM Pedido WHERE CodigoPedido = " + pedido.Codigo;

            ConnectionSQL.ExecutarComandoSQL(SQL2);
        }
    }
}
