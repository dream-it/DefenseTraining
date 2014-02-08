using System;
using System.Web.Services;
using DefenseTraining.Bol;
using DefenseTraining.Model;

namespace DefenseTraining.Web
{
    public partial class EventEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static AjaxData SaveEvent(Event evnt)
        {
            try
            {
                EventBol bol = new EventBol();
                return new AjaxData(true, bol.SaveEvent(evnt), string.Empty);
            }
            catch (Exception ex)
            {
                return new AjaxData(false, null, ex.Message);
            }
        }

        [WebMethod]
        public static AjaxData GetEvent(int id)
        {
            try
            {
                EventBol bol = new EventBol();
                return new AjaxData(true, bol.GetEvent(id), string.Empty);
            }
            catch (Exception ex)
            {
                return new AjaxData(false, null, ex.Message);
            }
        }
    }
}