using System;
using System.Web.Services;
using DefenseTraining.Bol;

namespace DefenseTraining.Web
{
    public partial class ResponsibilityList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        public static AjaxData GetResponsibilities()
        {
            try
            {
                ResponsibilityBol bol = new ResponsibilityBol();
                return new AjaxData(true, bol.GetResponsibilities(), string.Empty);
            }
            catch (Exception ex)
            {
                return new AjaxData(false, null, ex.Message);
            }
        }

        [WebMethod]
        public static AjaxData DeleteResponsibility(int id)
        {
            try
            {
                ResponsibilityBol bol = new ResponsibilityBol();
                bol.DeleteResponsibility(id);
                return new AjaxData(true, null, string.Empty);
            }
            catch (Exception ex)
            {
                return new AjaxData(false, null, ex.Message);
            }
        }
    }
}
