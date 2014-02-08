using System;
using System.Web.Services;
using DefenseTraining.Bol;

namespace DefenseTraining.Web
{
    public partial class RequiredDocList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        public static AjaxData GetRequiredDocs()
        {
            try
            {
                RequiredDocBol bol = new RequiredDocBol();
                return new AjaxData(true, bol.GetRequiredDocs(), string.Empty);
            }
            catch (Exception ex)
            {
                return new AjaxData(false, null, ex.Message);
            }
        }

        [WebMethod]
        public static AjaxData DeleteRequiredDoc(int id)
        {
            try
            {
                RequiredDocBol bol = new RequiredDocBol();
                bol.DeleteRequiredDoc(id);
                return new AjaxData(true, null, string.Empty);
            }
            catch (Exception ex)
            {
                return new AjaxData(false, null, ex.Message);
            }
        }
    }
}
