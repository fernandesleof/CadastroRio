using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CadastroRio.Models
{
    public class TelefoneModel : IDisposable
    {
        private SqlConnection connection;

        public TelefoneModel()
        {
            string strConn = "Data Source=LEONOTE\\SQLEXPRESS;Initial Catalog=CLIENTEDB;Integrated Security=true";
            connection = new SqlConnection(strConn);
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }

        public List<Telefone> Listar(int? id)
        {
            List<Telefone> lista = new List<Telefone>();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM TELEFONE WHERE IDCLIENTE=@IDCLIENTE;";
            cmd.Parameters.AddWithValue("@IDCLIENTE", id);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Telefone telefone = new Telefone();
                telefone.idTelefone = (int)reader["IDTELEFONE"];
                telefone.ddd = (String)reader["DDD"];
                telefone.numero = (String)reader["NUMERO"];

                lista.Add(telefone);
            }

            return lista;
        }

        public void CadastroTelefone(Telefone telefone)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"INSERT INTO TELEFONE(IDCLIENTE,DDD,NUMERO)"
                             + "VALUES (@IDCLIENTE,@DDD,@NUMERO) ";

            cmd.Parameters.AddWithValue("@IDCLIENTE", telefone.idCliente);
            cmd.Parameters.AddWithValue("@DDD", telefone.ddd);
            cmd.Parameters.AddWithValue("@NUMERO", telefone.numero);

            cmd.ExecuteNonQuery();
        }

        public void Excluir(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"DELETE FROM TELEFONE WHERE IDTELEFONE=@ID";

            cmd.Parameters.AddWithValue("@ID", id);

            cmd.ExecuteNonQuery();
        }

        public void ExcluirTelCliente(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"DELETE FROM TELEFONE WHERE IDCLIENTE=@ID";

            cmd.Parameters.AddWithValue("@ID", id);

            cmd.ExecuteNonQuery();
        }

        public void AlterarTelefone(Telefone telefone)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE TELEFONE SET DDD=@DDD,NUMERO=@NUMERO WHERE IDCLIENTE=@IDCLIENTE AND IDTELEFONE=@IDTELEFONE";

            cmd.Parameters.AddWithValue("@IDTELEFONE",telefone.idTelefone);
            cmd.Parameters.AddWithValue("@IDCLIENTE", telefone.idCliente);
            cmd.Parameters.AddWithValue("@DDD", telefone.ddd);
            cmd.Parameters.AddWithValue("@NUMERO", telefone.numero);

            cmd.ExecuteNonQuery();
        }
    }
}