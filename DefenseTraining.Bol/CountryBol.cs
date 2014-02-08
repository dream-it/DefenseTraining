using System.Collections.Generic;
using DefenseTraining.Dal;
using DefenseTraining.Model;
using System;

namespace DefenseTraining.Bol
{
    public class CountryBol
    {
        private CountryDal _Dal;

        public CountryBol()
        {
            _Dal = new CountryDal();
        }

        public List<Country> GetCountries()
        {
            return _Dal.GetCountries();
        }

        public void DeleteCountry(int id)
        {
            _Dal.DeleteCountry(id);
        }

        public Country SaveCountry(Country country)
        {
            if (_Dal.CountryExists(country.Id, country.Name))
                throw new Exception("Country with the same name already exists. Please enter unique country name.");
            country.Id = _Dal.SaveCountry(country);
            return country;
        }
    }
}
