using System.Collections.Generic;
using DefenseTraining.Dal;
using DefenseTraining.Model;
using System;

namespace DefenseTraining.Bol
{
    public class BranchBol
    {
        private BranchDal _Dal;

        public BranchBol()
        {
            _Dal = new BranchDal();
        }

        public List<Branch> GetBranches()
        {
            return _Dal.GetBranches();
        }

        public void DeleteBranch(int id)
        {
            _Dal.DeleteBranch(id);
        }

        public Branch SaveBranch(Branch branch)
        {
            if (_Dal.BranchExists(branch.Id, branch.Name))
                throw new Exception("Branch with the same name already exists. Please enter unique branch name.");
            branch.Id = _Dal.SaveBranch(branch);
            return branch;
        }
    }
}
