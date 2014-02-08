using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DefenseTraining.Model;

namespace DefenseTraining.Dal
{
    public class GenreDal : DalBase
    {
        public List<Genre> GetGenres()
        {
            List<Genre> genres = new List<Genre>();
            OpenConnection();
            try
            {
                using (SqlDataReader reader = ExecuteReader("GenreGetAll", new SqlParameter[] { }))
                {
                    while (reader.Read())
                    {
                        Genre genre = new Genre();
                        genre.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        genre.Name = reader.GetString(reader.GetOrdinal("Name"));
                        genre.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                        genres.Add(genre);
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
            return genres;
        }

        public void DeleteGenre(int id)
        {
            OpenConnection();
            try
            {
                ExecuteQuery("GenreDelete", new SqlParameter[] { new SqlParameter("@Id", id) });
            }
            finally
            {
                CloseConnection();
            }
        }

        public int SaveGenre(Genre genre)
        {
            OpenConnection();
            try
            {
                SqlParameter[] parameters = { 
                                                new SqlParameter("@Id", genre.Id) 
                                               ,new SqlParameter("@Name", genre.Name)
                                            };
                parameters[0].Direction = System.Data.ParameterDirection.InputOutput;
                ExecuteQuery("GenreSave", parameters);
                return Convert.ToInt32(parameters[0].Value);
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool GenreExists(int id, string name)
        {
            OpenConnection();
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@Id", id), new SqlParameter("@Name", name) };
                return ExecuteScalar<bool>("GenreExists", parameters);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
