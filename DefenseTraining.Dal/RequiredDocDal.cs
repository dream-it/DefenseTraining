using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DefenseTraining.Model;

namespace DefenseTraining.Dal
{
    public class RequiredDocDal : DalBase
    {
        public List<RequiredDoc> GetRequiredDocs()
        {
            List<RequiredDoc> requiredDocs = new List<RequiredDoc>();
            OpenConnection();
            try
            {
                using (SqlDataReader reader = ExecuteReader("RequiredDocGetAll", new SqlParameter[] { }))
                {
                    while (reader.Read())
                    {
                        RequiredDoc requiredDoc = new RequiredDoc();
                        requiredDoc.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        requiredDoc.Name = reader.GetString(reader.GetOrdinal("Name"));
                        requiredDoc.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                        requiredDocs.Add(requiredDoc);
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
            return requiredDocs;
        }

        public void DeleteRequiredDoc(int id)
        {
            OpenConnection();
            try
            {
                ExecuteQuery("RequiredDocDelete", new SqlParameter[] { new SqlParameter("@Id", id) });
            }
            finally
            {
                CloseConnection();
            }
        }

        public int SaveRequiredDoc(RequiredDoc requiredDoc)
        {
            OpenConnection();
            try
            {
                SqlParameter[] parameters = { 
                                                new SqlParameter("@Id", requiredDoc.Id) 
                                               ,new SqlParameter("@Name", requiredDoc.Name)
                                            };
                parameters[0].Direction = System.Data.ParameterDirection.InputOutput;
                ExecuteQuery("RequiredDocSave", parameters);
                return Convert.ToInt32(parameters[0].Value);
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool RequiredDocExists(int id, string name)
        {
            OpenConnection();
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@Id", id), new SqlParameter("@Name", name) };
                return ExecuteScalar<bool>("RequiredDocExists", parameters);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
