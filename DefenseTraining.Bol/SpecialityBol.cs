using System.Collections.Generic;
using DefenseTraining.Dal;
using DefenseTraining.Model;
using System;

namespace DefenseTraining.Bol
{
    public class SpecialityBol
    {
        private SpecialityDal _Dal;

        public SpecialityBol()
        {
            _Dal = new SpecialityDal();
        }

        public List<Speciality> GetSpecialities()
        {
            return _Dal.GetSpecialities();
        }

        public void DeleteSpeciality(int id)
        {
            _Dal.DeleteSpeciality(id);
        }

        public Speciality SaveSpeciality(Speciality speciality)
        {
            if (_Dal.SpecialityExists(speciality.Id, speciality.Name))
                throw new Exception("Speciality with the same name already exists. Please enter unique speciality name.");
            speciality.Id = _Dal.SaveSpeciality(speciality);
            return speciality;
        }
    }
}
