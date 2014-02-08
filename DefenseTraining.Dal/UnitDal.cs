using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DefenseTraining.Model;

namespace DefenseTraining.Dal
{
    public class UnitDal : DalBase
    {
        public List<Unit> GetUnits()
        {
            List<Unit> units = new List<Unit>();
            OpenConnection();
            try
            {
                using (SqlDataReader reader = ExecuteReader("UnitGetAll", new SqlParameter[] { }))
                {
                    while (reader.Read())
                    {
                        Unit unit = new Unit();
                        unit.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        unit.Name = reader.GetString(reader.GetOrdinal("Name"));
                        unit.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                        units.Add(unit);
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
            return units;
        }

        public void DeleteUnit(int id)
        {
            OpenConnection();
            try
            {
                ExecuteQuery("UnitDelete", new SqlParameter[] { new SqlParameter("@Id", id) });
            }
            finally
            {
                CloseConnection();
            }
        }

        public int SaveUnit(Unit unit)
        {
            OpenConnection();
            try
            {
                SqlParameter[] parameters = { 
                                                new SqlParameter("@Id", unit.Id) 
                                               ,new SqlParameter("@Name", unit.Name)
                                            };
                parameters[0].Direction = System.Data.ParameterDirection.InputOutput;
                ExecuteQuery("UnitSave", parameters);
                return Convert.ToInt32(parameters[0].Value);
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool UnitExists(int id, string name)
        {
            OpenConnection();
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@Id", id), new SqlParameter("@Name", name) };
                return ExecuteScalar<bool>("UnitExists", parameters);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
