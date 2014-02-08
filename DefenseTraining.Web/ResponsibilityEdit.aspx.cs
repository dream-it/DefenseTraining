using System;
using System.Web.Services;
using DefenseTraining.Bol;
using DefenseTraining.Model;

namespace DefenseTraining.Web
{
    public partial class ResponsibilityEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        public static AjaxData SaveResponsibility(Responsibility responsibility)
        {
            try
            {
                ResponsibilityBol bol = new ResponsibilityBol();
                return new AjaxData(true, bol.SaveResponsibility(responsibility), string.Empty);
            }
            catch (Exception ex)
            {
                return new AjaxData(false, null, ex.Message);
            }
        }
    }
}
