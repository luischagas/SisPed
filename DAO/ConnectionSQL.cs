using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class ConnectionSQL
    {
        private static SqlConnection con;

        public static SqlConnection AbrirConexao()
        {
            if (con == null)
            { con = new SqlConnection("Data Source=serverinfnetluis.database.windows.net;Initial Catalog=BDPosNET;;UID=luis;PWD=Infnet12345"); }

            if (con.State == System.Data.ConnectionState.Closed)
            { con.Open(); }

            return con;
        }

        public static void FecharConexao()
        {
            if (con != null && con.State == System.Data.ConnectionState.Open)
            { con.Close(); }
        }

        public static int ExecutarComandoSQL(string strComando)
        {

            int IdRetorno;
            SqlConnection con = AbrirConexao();

            using (SqlCommand command = new SqlCommand(strComando, con))
            {
                IdRetorno = Convert.ToInt32(command.ExecuteScalar());
            }

            FecharConexao();

            return IdRetorno;

        }

        public static Dictionary<string, string> ExecutarComandoLeituraSQL(string strComando)
        {
            Dictionary<string, string> registro = new Dictionary<string, string>();

            SqlConnection con = AbrirConexao();

            using (SqlCommand command = new SqlCommand(strComando, con))

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    { registro.Add(reader.GetName(i), reader[reader.GetName(i)].ToString()); }
                }
            }

            FecharConexao();

            return registro;
        }
    }
}
