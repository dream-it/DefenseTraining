using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DefenseTraining.Model;

namespace DefenseTraining.Dal
{
    public class SpecialityDal : DalBase
    {
        public List<Speciality> GetSpecialities()
        {
            List<Speciality> specialities = new List<Speciality>();
            OpenConnection();
            try
            {
                using (SqlDataReader reader = ExecuteReader("SpecialityGetAll", new SqlParameter[] { }))
                {
                    while (reader.Read())
                    {
                        Speciality speciality = new Speciality();
                        speciality.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        speciality.Name = reader.GetString(reader.GetOrdinal("Name"));
                        speciality.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                        specialities.Add(speciality);
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
            return specialities;
        }

        public void DeleteSpeciality(int id)
        {
            OpenConnection();
            try
            {
                ExecuteQuery("SpecialityDelete", new SqlParameter[] { new SqlParameter("@Id", id) });
            }
            finally
            {
                CloseConnection();
            }
        }

        public int SaveSpeciality(Speciality speciality)
        {
            OpenConnection();
            try
            {
                SqlParameter[] parameters = { 
                                                new SqlParameter("@Id", speciality.Id) 
                                               ,new SqlParameter("@Name", speciality.Name)
                                            };
                parameters[0].Direction = System.Data.ParameterDirection.InputOutput;
                ExecuteQuery("SpecialitySave", parameters);
                return Convert.ToInt32(parameters[0].Value);
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool SpecialityExists(int id, string name)
        {
            OpenConnection();
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@Id", id), new SqlParameter("@Name", name) };
                return ExecuteScalar<bool>("SpecialityExists", parameters);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
