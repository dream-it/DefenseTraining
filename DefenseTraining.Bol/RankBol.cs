using System.Collections.Generic;
using DefenseTraining.Dal;
using DefenseTraining.Model;
using System;

namespace DefenseTraining.Bol
{
    public class RankBol
    {
        private RankDal _Dal;

        public RankBol()
        {
            _Dal = new RankDal();
        }

        public List<Rank> GetRanks()
        {
            return _Dal.GetRanks();
        }

        public void DeleteRank(int id)
        {
            _Dal.DeleteRank(id);
        }

        public Rank SaveRank(Rank rank)
        {
            if (_Dal.RankExists(rank.Id, rank.Name))
                throw new Exception("Rank with the same name already exists. Please enter unique rank name.");
            rank.Id = _Dal.SaveRank(rank);
            return rank;
        }
    }
}
