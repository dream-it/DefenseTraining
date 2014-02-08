using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DefenseTraining.Dal
{
    public class DalBase
    {
        private SqlConnection _Connection;

        public DalBase()
        {
            _Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NavyTraining"].ConnectionString);
        }

        protected void OpenConnection()
        {
            _Connection.Open();
        }

        protected void CloseConnection()
        {
            if (_Connection != null && _Connection.State == ConnectionState.Open)
            {
                _Connection.Close();
            }
        }

        protected SqlDataReader ExecuteReader(string spName, SqlParameter[] parameters)
        {
            SqlCommand command = CreateCommand(spName, parameters);
            return command.ExecuteReader();
        }

        protected void ExecuteQuery(string spName, SqlParameter[] parameters)
        {
            SqlCommand command = CreateCommand(spName, parameters);
            command.ExecuteNonQuery();
        }

        protected T ExecuteScalar<T>(string spName, SqlParameter[] parameters)
        {
            SqlCommand command = CreateCommand(spName, parameters);
            return (T)command.ExecuteScalar();
        }

        protected DataSet GetDataSet(string spName, SqlParameter[] parameters)
        {
            SqlCommand command = CreateCommand(spName, parameters);
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);
            return ds;
        }

        private SqlCommand CreateCommand(string spName, SqlParameter[] parameters)
        {
            SqlCommand command = _Connection.CreateCommand();
            command.CommandText = spName;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters);
            return command;
        }
    }
}
