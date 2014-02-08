using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DefenseTraining.Model;

namespace DefenseTraining.Dal
{
    public class ResponsibilityDal : DalBase
    {
        public List<Responsibility> GetResponsibilities()
        {
            List<Responsibility> responsibilities = new List<Responsibility>();
            OpenConnection();
            try
            {
                using (SqlDataReader reader = ExecuteReader("ResponsibilityGetAll", new SqlParameter[] { }))
                {
                    while (reader.Read())
                    {
                        Responsibility responsibility = new Responsibility();
                        responsibility.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        responsibility.Name = reader.GetString(reader.GetOrdinal("Name"));
                        responsibility.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                        responsibilities.Add(responsibility);
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
            return responsibilities;
        }

        public void DeleteResponsibility(int id)
        {
            OpenConnection();
            try
            {
                ExecuteQuery("ResponsibilityDelete", new SqlParameter[] { new SqlParameter("@Id", id) });
            }
            finally
            {
                CloseConnection();
            }
        }

        public int SaveResponsibility(Responsibility responsibility)
        {
            OpenConnection();
            try
            {
                SqlParameter[] parameters = { 
                                                new SqlParameter("@Id", responsibility.Id) 
                                               ,new SqlParameter("@Name", responsibility.Name)
                                            };
                parameters[0].Direction = System.Data.ParameterDirection.InputOutput;
                ExecuteQuery("ResponsibilitySave", parameters);
                return Convert.ToInt32(parameters[0].Value);
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool ResponsibilityExists(int id, string name)
        {
            OpenConnection();
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@Id", id), new SqlParameter("@Name", name) };
                return ExecuteScalar<bool>("ResponsibilityExists", parameters);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
