using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DefenseTraining.Model;

namespace DefenseTraining.Dal
{
    public class BranchDal : DalBase
    {
        public List<Branch> GetBranches()
        {
            List<Branch> branches = new List<Branch>();
            OpenConnection();
            try
            {
                using (SqlDataReader reader = ExecuteReader("BranchGetAll", new SqlParameter[] { }))
                {
                    while (reader.Read())
                    {
                        Branch branch = new Branch();
                        branch.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        branch.Name = reader.GetString(reader.GetOrdinal("Name"));
                        branch.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                        branches.Add(branch);
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
            return branches;
        }

        public void DeleteBranch(int id)
        {
            OpenConnection();
            try
            {
                ExecuteQuery("BranchDelete", new SqlParameter[] { new SqlParameter("@Id", id) });
            }
            finally
            {
                CloseConnection();
            }
        }

        public int SaveBranch(Branch branch)
        {
            OpenConnection();
            try
            {
                SqlParameter[] parameters = { 
                                                new SqlParameter("@Id", branch.Id) 
                                               ,new SqlParameter("@Name", branch.Name)
                                            };
                parameters[0].Direction = System.Data.ParameterDirection.InputOutput;
                ExecuteQuery("BranchSave", parameters);
                return Convert.ToInt32(parameters[0].Value);
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool BranchExists(int id, string name)
        {
            OpenConnection();
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@Id", id), new SqlParameter("@Name", name) };
                return ExecuteScalar<bool>("BranchExists", parameters);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
