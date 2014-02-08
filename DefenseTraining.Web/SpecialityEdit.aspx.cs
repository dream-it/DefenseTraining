using System;
using System.Web.Services;
using DefenseTraining.Bol;
using DefenseTraining.Model;

namespace DefenseTraining.Web
{
    public partial class SpecialityEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        public static AjaxData SaveSpeciality(Speciality speciality)
        {
            try
            {
                SpecialityBol bol = new SpecialityBol();
                return new AjaxData(true, bol.SaveSpeciality(speciality), string.Empty);
            }
            catch (Exception ex)
            {
                return new AjaxData(false, null, ex.Message);
            }
        }
    }
}
