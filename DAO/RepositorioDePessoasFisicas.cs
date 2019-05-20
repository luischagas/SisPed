using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class RepositorioDePessoasFisicas
    {
        public List<PessoaFisica> ObtemPessoasFisicas()
        {
            List<PessoaFisica> listPessoa = new List<PessoaFisica>();

            SqlConnection con = ConnectionSQL.AbrirConexao();

            using (SqlCommand command = new SqlCommand("Select CodigoPessoa, Nome, Endereco, CPF, DataNascimento, (CASE SEXO WHEN 'M' THEN 'Masculino' WHEN 'F' THEN 'Feminino' END) as Sexo From Pessoa p inner join PessoaFisica F on p.CodigoPessoa = F.CodigoPF", con))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    listPessoa.Add(new PessoaFisica()
                    {
                        Codigo = Convert.ToInt32(reader["CodigoPessoa"]),
                        Nome = reader["Nome"].ToString(),
                        Endereco = reader["Endereco"].ToString(),
                        CPF = reader["CPF"].ToString(),
                        DataNascimento = Convert.ToDateTime(reader["DataNascimento"]),
                        Sexo = reader["Sexo"].ToString()
                    });
                }
            }

            ConnectionSQL.FecharConexao();

            return listPessoa;
        }

        public PessoaFisica ObtemPessoa(int id)
        {
            PessoaFisica pessoa = new PessoaFisica();

            string SQL = "Select CodigoPessoa, Nome, Endereco, CPF, DataNascimento, Sexo From Pessoa p inner join PessoaFisica F on p.CodigoPessoa = F.CodigoPF where CodigoPessoa = " + id;

            Dictionary<string, string> registro = ConnectionSQL.ExecutarComandoLeituraSQL(SQL);

            pessoa.Codigo = int.Parse(registro["CodigoPessoa"]);
            pessoa.Nome = registro["Nome"];
            pessoa.Endereco = registro["Endereco"];
            pessoa.CPF = registro["CPF"];
            pessoa.DataNascimento = Convert.ToDateTime(registro["DataNascimento"]);
            pessoa.Sexo = registro["Sexo"];
            return pessoa;
        }

        public void Add(PessoaFisica pessoa)
        {
            string SQL2 = "";

            string SQL = "INSERT INTO Pessoa (Nome, Endereco) Values ('"
              + pessoa.Nome + "', '" + pessoa.Endereco + "') SELECT SCOPE_IDENTITY()";

            pessoa.Codigo = ConnectionSQL.ExecutarComandoSQL(SQL);

            SQL2 = "INSERT INTO PessoaFisica Values ("
         + pessoa.Codigo + ", '" + pessoa.CPF + "', '" + pessoa.DataNascimento.ToString("yyyy-MM-dd") + "', '" + pessoa.Sexo + "')";

            ConnectionSQL.ExecutarComandoSQL(SQL2);
        }

        public void Update(PessoaFisica pessoa)
        {
            string SQL = "UPDATE Pessoa SET Nome = '" + pessoa.Nome +
              "', Endereco = '" + pessoa.Endereco +
              "' WHERE CodigoPessoa = " + pessoa.Codigo;

            ConnectionSQL.ExecutarComandoSQL(SQL);

            string SQL2 = "UPDATE PessoaFisica SET CPF = '" + pessoa.CPF +
             "', DataNascimento = '" + pessoa.DataNascimento +
             "', Sexo = '" + pessoa.Sexo +
             "' WHERE CodigoPF = " + pessoa.Codigo;

            ConnectionSQL.ExecutarComandoSQL(SQL2);
        }

        public void Remove(PessoaFisica pessoa)
        {
            string SQL = "DELETE FROM PessoaFisica WHERE CodigoPF = " + pessoa.Codigo;

            ConnectionSQL.ExecutarComandoSQL(SQL);

            string SQL2 = "DELETE FROM Pessoa WHERE CodigoPessoa = " + pessoa.Codigo;

            ConnectionSQL.ExecutarComandoSQL(SQL2);
        }

        public PessoaFisica ObtemPedidoPessoa(int id)
        {
            PessoaFisica pessoa = new PessoaFisica();

            string SQL = "Select top(1) pf.CodigoPF From PessoaFisica pf inner join Pedido P on pf.CodigoPF = p.CodigoComprador where pf.CodigoPF =  " + id;

            Dictionary<string, string> registro = ConnectionSQL.ExecutarComandoLeituraSQL(SQL);

            if (registro.Count > 0) {
                pessoa.Codigo = int.Parse(registro["CodigoPF"]);
            }
            
            return pessoa;
        }

    }
}
