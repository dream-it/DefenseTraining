using System.Collections.Generic;
using DefenseTraining.Dal;
using DefenseTraining.Model;
using System;

namespace DefenseTraining.Bol
{
    public class GenreBol
    {
        private GenreDal _Dal;

        public GenreBol()
        {
            _Dal = new GenreDal();
        }

        public List<Genre> GetGenres()
        {
            return _Dal.GetGenres();
        }

        public void DeleteGenre(int id)
        {
            _Dal.DeleteGenre(id);
        }

        public Genre SaveGenre(Genre genre)
        {
            if (_Dal.GenreExists(genre.Id, genre.Name))
                throw new Exception("Genre with the same name already exists. Please enter unique genre name.");
            genre.Id = _Dal.SaveGenre(genre);
            return genre;
        }
    }
}
