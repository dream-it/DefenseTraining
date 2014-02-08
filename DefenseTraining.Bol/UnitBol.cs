using System.Collections.Generic;
using DefenseTraining.Dal;
using DefenseTraining.Model;
using System;

namespace DefenseTraining.Bol
{
    public class UnitBol
    {
        private UnitDal _Dal;

        public UnitBol()
        {
            _Dal = new UnitDal();
        }

        public List<Unit> GetUnits()
        {
            return _Dal.GetUnits();
        }

        public void DeleteUnit(int id)
        {
            _Dal.DeleteUnit(id);
        }

        public Unit SaveUnit(Unit unit)
        {
            if (_Dal.UnitExists(unit.Id, unit.Name))
                throw new Exception("Unit with the same name already exists. Please enter unique unit name.");
            unit.Id = _Dal.SaveUnit(unit);
            return unit;
        }
    }
}
