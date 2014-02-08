using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DefenseTraining.Model;

namespace DefenseTraining.Dal
{
    public class CountryDal : DalBase
    {
        public List<Country> GetCountries()
        {
            List<Country> countries = new List<Country>();
            OpenConnection();
            try
            {
                using (SqlDataReader reader = ExecuteReader("CountryGetAll", new SqlParameter[] { }))
                {
                    while (reader.Read())
                    {
                        Country country = new Country();
                        country.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        country.Name = reader.GetString(reader.GetOrdinal("Name"));
                        country.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                        countries.Add(country);
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
            return countries;
        }

        public void DeleteCountry(int id)
        {
            OpenConnection();
            try
            {
                ExecuteQuery("CountryDelete", new SqlParameter[] { new SqlParameter("@Id", id) });
            }
            finally
            {
                CloseConnection();
            }
        }

        public int SaveCountry(Country country)
        {
            OpenConnection();
            try
            {
                SqlParameter[] parameters = { 
                                                new SqlParameter("@Id", country.Id) 
                                               ,new SqlParameter("@Name", country.Name)
                                            };
                parameters[0].Direction = System.Data.ParameterDirection.InputOutput;
                ExecuteQuery("CountrySave", parameters);
                return Convert.ToInt32(parameters[0].Value);
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool CountryExists(int id, string name)
        {
            OpenConnection();
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@Id", id), new SqlParameter("@Name", name) };
                return ExecuteScalar<bool>("CountryExists", parameters);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
