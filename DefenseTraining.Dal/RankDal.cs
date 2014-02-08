using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DefenseTraining.Model;

namespace DefenseTraining.Dal
{
    public class RankDal : DalBase
    {
        public List<Rank> GetRanks()
        {
            List<Rank> ranks = new List<Rank>();
            OpenConnection();
            try
            {
                using (SqlDataReader reader = ExecuteReader("RankGetAll", new SqlParameter[] { }))
                {
                    while (reader.Read())
                    {
                        Rank rank = new Rank();
                        rank.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        rank.Name = reader.GetString(reader.GetOrdinal("Name"));
                        rank.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                        ranks.Add(rank);
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
            return ranks;
        }

        public void DeleteRank(int id)
        {
            OpenConnection();
            try
            {
                ExecuteQuery("RankDelete", new SqlParameter[] { new SqlParameter("@Id", id) });
            }
            finally
            {
                CloseConnection();
            }
        }

        public int SaveRank(Rank rank)
        {
            OpenConnection();
            try
            {
                SqlParameter[] parameters = { 
                                                new SqlParameter("@Id", rank.Id) 
                                               ,new SqlParameter("@Name", rank.Name)
                                            };
                parameters[0].Direction = System.Data.ParameterDirection.InputOutput;
                ExecuteQuery("RankSave", parameters);
                return Convert.ToInt32(parameters[0].Value);
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool RankExists(int id, string name)
        {
            OpenConnection();
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@Id", id), new SqlParameter("@Name", name) };
                return ExecuteScalar<bool>("RankExists", parameters);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
