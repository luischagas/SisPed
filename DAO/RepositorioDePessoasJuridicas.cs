using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class RepositorioDePessoasJuridicas
    {
        public List<PessoaJuridica> ObtemPessoasJuridicas()
        {
            List<PessoaJuridica> listPessoa = new List<PessoaJuridica>();

            SqlConnection con = ConnectionSQL.AbrirConexao();

            using (SqlCommand command = new SqlCommand("Select * From Pessoa p inner join PessoaJuridica J on p.CodigoPessoa = J.CodigoPJ", con))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    listPessoa.Add(new PessoaJuridica()
                    {
                        Codigo = Convert.ToInt32(reader["CodigoPessoa"]),
                        Nome = reader["Nome"].ToString(),
                        Endereco = reader["Endereco"].ToString(),
                        CNPJ = reader["CNPJ"].ToString(),
                        Ativa = Convert.ToBoolean(reader["Ativa"])
                    });
                }
            }

            ConnectionSQL.FecharConexao();

            return listPessoa;
        }

        public PessoaJuridica ObtemPessoa(int id)
        {
            PessoaJuridica pessoa = new PessoaJuridica();

            string SQL = "Select CodigoPessoa, Nome, Endereco, CNPJ, Ativa From Pessoa p inner join PessoaJuridica J on p.CodigoPessoa = J.CodigoPJ where CodigoPessoa = " + id;

            Dictionary<string, string> registro = ConnectionSQL.ExecutarComandoLeituraSQL(SQL);

            pessoa.Codigo = int.Parse(registro["CodigoPessoa"]);
            pessoa.Nome = registro["Nome"];
            pessoa.Endereco = registro["Endereco"];
            pessoa.CNPJ = registro["CNPJ"];
            pessoa.Ativa = Convert.ToBoolean(registro["Ativa"]);
            return pessoa;
        }

        public void Add(PessoaJuridica pessoa)
        {
            string SQL2 = "";

            string SQL = "INSERT INTO Pessoa (Nome, Endereco) Values ('"
              + pessoa.Nome + "', '" + pessoa.Endereco + "') SELECT SCOPE_IDENTITY()";

            pessoa.Codigo = ConnectionSQL.ExecutarComandoSQL(SQL);

            SQL2 = "INSERT INTO PessoaJuridica Values ("
         + pessoa.Codigo + ", '" + pessoa.CNPJ + "', '" + pessoa.Ativa + "')";

            ConnectionSQL.ExecutarComandoSQL(SQL2);
        }

        public void Update(PessoaJuridica pessoa)
        {
            string SQL = "UPDATE Pessoa SET Nome = '" + pessoa.Nome +
              "', Endereco = '" + pessoa.Endereco +
              "' WHERE CodigoPessoa = " + pessoa.Codigo;

            ConnectionSQL.ExecutarComandoSQL(SQL);

            string SQL2 = "UPDATE PessoaJuridica SET CNPJ = '" + pessoa.CNPJ +
             "', Ativa = '" + pessoa.Ativa +
             "' WHERE CodigoPJ = " + pessoa.Codigo;

            ConnectionSQL.ExecutarComandoSQL(SQL2);
        }

        public void Remove(PessoaJuridica pessoa)
        {
            string SQL = "DELETE FROM PessoaJuridica WHERE CodigoPJ = " + pessoa.Codigo;

            ConnectionSQL.ExecutarComandoSQL(SQL);

            string SQL2 = "DELETE FROM Pessoa WHERE CodigoPessoa = " + pessoa.Codigo;

            ConnectionSQL.ExecutarComandoSQL(SQL2);
        }

        public PessoaJuridica ObtemProdutoPessoa(int id)
        {
            PessoaJuridica pessoa = new PessoaJuridica();

            string SQL = "Select top(1) pj.CodigoPJ From PessoaJuridica pj inner join Produto P on pj.CodigoPJ = p.CodigoFornecedor where pj.CodigoPJ = " + id;

            Dictionary<string, string> registro = ConnectionSQL.ExecutarComandoLeituraSQL(SQL);

            if (registro.Count > 0)
            {
                pessoa.Codigo = int.Parse(registro["CodigoPJ"]);
            }

            return pessoa;
        }
    }
}
