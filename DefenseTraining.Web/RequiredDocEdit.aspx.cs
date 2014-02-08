using System;
using System.Web.Services;
using DefenseTraining.Bol;
using DefenseTraining.Model;

namespace DefenseTraining.Web
{
    public partial class RequiredDocEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        public static AjaxData SaveRequiredDoc(RequiredDoc requiredDoc)
        {
            try
            {
                RequiredDocBol bol = new RequiredDocBol();
                return new AjaxData(true, bol.SaveRequiredDoc(requiredDoc), string.Empty);
            }
            catch (Exception ex)
            {
                return new AjaxData(false, null, ex.Message);
            }
        }
    }
}
