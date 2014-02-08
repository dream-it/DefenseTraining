using System.Collections.Generic;
using DefenseTraining.Dal;
using DefenseTraining.Model;
using System;

namespace DefenseTraining.Bol
{
    public class RequiredDocBol
    {
        private RequiredDocDal _Dal;

        public RequiredDocBol()
        {
            _Dal = new RequiredDocDal();
        }

        public List<RequiredDoc> GetRequiredDocs()
        {
            return _Dal.GetRequiredDocs();
        }

        public void DeleteRequiredDoc(int id)
        {
            _Dal.DeleteRequiredDoc(id);
        }

        public RequiredDoc SaveRequiredDoc(RequiredDoc requiredDoc)
        {
            if (_Dal.RequiredDocExists(requiredDoc.Id, requiredDoc.Name))
                throw new Exception("Required doc with the same name already exists. Please enter unique required doc name.");
            requiredDoc.Id = _Dal.SaveRequiredDoc(requiredDoc);
            return requiredDoc;
        }
    }
}
