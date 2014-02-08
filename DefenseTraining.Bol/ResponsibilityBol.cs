using System.Collections.Generic;
using DefenseTraining.Dal;
using DefenseTraining.Model;
using System;

namespace DefenseTraining.Bol
{
    public class ResponsibilityBol
    {
        private ResponsibilityDal _Dal;

        public ResponsibilityBol()
        {
            _Dal = new ResponsibilityDal();
        }

        public List<Responsibility> GetResponsibilities()
        {
            return _Dal.GetResponsibilities();
        }

        public void DeleteResponsibility(int id)
        {
            _Dal.DeleteResponsibility(id);
        }

        public Responsibility SaveResponsibility(Responsibility responsibility)
        {
            if (_Dal.ResponsibilityExists(responsibility.Id, responsibility.Name))
                throw new Exception("Responsibility with the same name already exists. Please enter unique responsibility name.");
            responsibility.Id = _Dal.SaveResponsibility(responsibility);
            return responsibility;
        }
    }
}
