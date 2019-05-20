using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class RepositorioDeProdutos
    {

        public List<Produto> ObtemProdutos()
        {
            List<Produto> listProduto = new List<Produto>();

            SqlConnection con = ConnectionSQL.AbrirConexao();

            using (SqlCommand command = new SqlCommand("Select p.CodigoProduto, p.Nome, p.Preco, P.CodigoFornecedor, j.CNPJ, j.Ativa, pe.Nome as 'NomeFornecedor' From produto p inner join PessoaJuridica J on p.CodigoFornecedor = J.CodigoPJ inner join pessoa pe on j.CodigoPJ = pe.CodigoPessoa", con))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    listProduto.Add(new Produto()
                    {                          
                        Codigo = Convert.ToInt32(reader["CodigoProduto"]),
                        Nome = reader["Nome"].ToString(),
                        Preco = Convert.ToDecimal(reader["Preco"]),
                        Fornecedor = new PessoaJuridica() {
                        Codigo = Convert.ToInt32(reader["CodigoFornecedor"]),
                        Nome = reader["NomeFornecedor"].ToString(),
                        },
                    });

                    
                }
            }

            ConnectionSQL.FecharConexao();

            return listProduto;
        }

        public Produto ObtemProduto(int id)
        {
            Produto produto = new Produto();

            string SQL = "Select * From produto p inner join PessoaJuridica J on p.CodigoFornecedor = J.CodigoPJ where CodigoProduto = " + id;

            Dictionary<string, string> registro = ConnectionSQL.ExecutarComandoLeituraSQL(SQL);

            produto.Codigo = int.Parse(registro["CodigoProduto"]);
            produto.Nome = registro["Nome"];
            produto.Preco = Convert.ToDecimal(registro["Preco"]);
            produto.Fornecedor = new PessoaJuridica()
            {
                Codigo = Convert.ToInt32(registro["CodigoFornecedor"])
            };
            return produto;
        }

        public void Add(Produto produto)
        {
            string SQL = "INSERT INTO Produto (Nome, Preco, CodigoFornecedor) Values ('"
              + produto.Nome + "', " + produto.Preco + ", " + produto.Fornecedor.Codigo + ")";

            ConnectionSQL.ExecutarComandoSQL(SQL);
        }

        public void Update(Produto produto)
        {
            string SQL = "UPDATE Produto SET Nome = '" + produto.Nome +
              "', Preco = " + produto.Preco +
              ", CodigoFornecedor = " + produto.Fornecedor.Codigo +
              " WHERE CodigoProduto = " + produto.Codigo;

            ConnectionSQL.ExecutarComandoSQL(SQL);
        }

        public void Remove(Produto produto)
        {
            string SQL = "DELETE FROM Produto WHERE CodigoProduto = " + produto.Codigo;

            ConnectionSQL.ExecutarComandoSQL(SQL);
        }

        public Produto ObtemPedidoProduto(int id)
        {
            Produto produto = new Produto();

            string SQL = "Select top(1) p.CodigoProduto From produto p inner join item i on p.CodigoProduto = i.CodigoProduto inner join pedido pe on i.CodigoPedido = pe.CodigoPedido where p.CodigoProduto = " + id;

            Dictionary<string, string> registro = ConnectionSQL.ExecutarComandoLeituraSQL(SQL);

            if (registro.Count > 0)
            {
                produto.Codigo = int.Parse(registro["CodigoProduto"]);
            }

            return produto;
        }
    }
}
