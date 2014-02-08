using System;
using System.Web.Services;
using DefenseTraining.Bol;

namespace DefenseTraining.Web
{
    public partial class SpecialityList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        public static AjaxData GetSpecialities()
        {
            try
            {
                SpecialityBol bol = new SpecialityBol();
                return new AjaxData(true, bol.GetSpecialities(), string.Empty);
            }
            catch (Exception ex)
            {
                return new AjaxData(false, null, ex.Message);
            }
        }

        [WebMethod]
        public static AjaxData DeleteSpeciality(int id)
        {
            try
            {
                SpecialityBol bol = new SpecialityBol();
                bol.DeleteSpeciality(id);
                return new AjaxData(true, null, string.Empty);
            }
            catch (Exception ex)
            {
                return new AjaxData(false, null, ex.Message);
            }
        }
    }
}
