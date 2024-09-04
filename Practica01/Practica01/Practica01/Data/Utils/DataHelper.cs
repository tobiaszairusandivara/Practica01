using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Data.Utils
{
    public class DataHelper
    {
        private static DataHelper _instance;
        private SqlConnection _connection;
        private string cnnString;
     
        private DataHelper()
        {
            cnnString = "server = DESKTOP-O0EN1VB; database = Practica01; Integrated Security=True; Encrypt=False";
            _connection = new SqlConnection(cnnString);
        }

        public SqlConnection GetConnection()
        {
            return _connection;
        }

        public static DataHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }

        public DataTable ExecuteSPQuery(string sp, List <SQLParameter>? parametros)
        {
            DataTable tabla = new DataTable();
            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (parametros != null)
                {
                    foreach (var param in parametros)
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                }
                tabla.Load(cmd.ExecuteReader());
                _connection.Close();
            }
            catch (SqlException)
            {
                tabla = null;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return tabla;
        }

        public bool ExecuteCRUDSPQuery(string sp, List <SQLParameter>? parametros)
        {
            bool retorno;
            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (parametros != null)
                {
                    foreach (var param in parametros)
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                }
                if (cmd.ExecuteNonQuery() != 0)
                {
                    retorno = true;
                }
                else
                {
                    retorno = false;
                }
            }
            catch (SqlException)
            {
                retorno = false;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return retorno;
        }
    }
}
