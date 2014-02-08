using System.Collections.Generic;
using DefenseTraining.Dal;
using DefenseTraining.Model;
using System;

namespace DefenseTraining.Bol
{
    public class PortalBol
    {
        private PortalDal _Dal;

        public PortalBol()
        {
            _Dal = new PortalDal();
        }

        public List<JointStatement> GetJointStatements(int year)
        {
            return _Dal.GetJointStatements(year);
        }
    }
}
