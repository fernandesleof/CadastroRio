using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CadastroRio.Models
{
    public class ClienteModel : IDisposable
    {
        private SqlConnection connection;

        public ClienteModel()
        {
            string strConn = "Data Source=LEONOTE\\SQLEXPRESS;Initial Catalog=CLIENTEDB;Integrated Security=true";
            connection = new SqlConnection(strConn);
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }

        public List<Cliente> Listar()
        {
            List<Cliente> lista= new List<Cliente>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT IDCLIENTE,NOME,CPF,DATANASCIMENTO,CASE GENERO WHEN 'M' THEN 'Masculino' ELSE 'Feminino' END AS GENERO  FROM CLIENTE ORDER BY NOME";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                Cliente cliente = new Cliente();
                cliente.idCliente = (int)reader["IDCLIENTE"];
                cliente.nomeCliente = (String)reader["NOME"];
                cliente.cpfCliente = (String)reader["CPF"];
                cliente.dataNascimentoCliente = (DateTime)reader["DATANASCIMENTO"];
                cliente.generoCliente = (String)reader["GENERO"];

                lista.Add(cliente);
            }

            return lista;
        }

        public int CadastroCliente(Cliente cliente)
        {
            int idcliente = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"INSERT INTO CLIENTE(NOME,CPF,DATANASCIMENTO,GENERO) OUTPUT Inserted.IDCLIENTE "
                             + "VALUES (@NOME,@CPF,@DATANASCIMENTO,@GENERO) ";

            cmd.Parameters.AddWithValue("@NOME", cliente.nomeCliente);
            cmd.Parameters.AddWithValue("@CPF", cliente.cpfCliente);
            cmd.Parameters.AddWithValue("@DATANASCIMENTO", cliente.dataNascimentoCliente);
            cmd.Parameters.AddWithValue("@GENERO", cliente.generoCliente);

            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.HasRows)
            {
                dataReader.Read();
                idcliente = Convert.ToInt32(dataReader["IDCLIENTE"]);
            }
            dataReader.Close();
            return (idcliente);
        }

        public Cliente GetCliente(int? id)
        {
            Cliente cliente = new Cliente();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM CLIENTE WHERE IDCLIENTE= " + id;
            
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cliente.idCliente = (int)reader["IDCLIENTE"];
                cliente.nomeCliente = (String)reader["NOME"];
                cliente.cpfCliente = (String)reader["CPF"];
                cliente.dataNascimentoCliente = (DateTime)reader["DATANASCIMENTO"];
                cliente.generoCliente = (String)reader["GENERO"];
            }
            return cliente;
        }

        public void Excluir(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"DELETE FROM CLIENTE WHERE IDCLIENTE=@ID";

            cmd.Parameters.AddWithValue("@ID", id);

            cmd.ExecuteNonQuery();
        }

        public void Alterar(Cliente cliente)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE CLIENTE SET NOME=@NOME,CPF=@CPF,DATANASCIMENTO=@DATANASCIMENTO,GENERO=@GENERO WHERE IDCLIENTE=@IDCLIENTE";

            cmd.Parameters.AddWithValue("@IDCLIENTE", cliente.idCliente);
            cmd.Parameters.AddWithValue("@NOME", cliente.nomeCliente);
            cmd.Parameters.AddWithValue("@CPF", cliente.cpfCliente);
            cmd.Parameters.AddWithValue("@DATANASCIMENTO", cliente.dataNascimentoCliente);
            cmd.Parameters.AddWithValue("@GENERO", cliente.generoCliente);

            cmd.ExecuteNonQuery();
        }



    }
}