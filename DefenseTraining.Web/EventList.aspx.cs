using System;
using System.Web.Services;
using DefenseTraining.Bol;
using DefenseTraining.Model;

namespace DefenseTraining.Web
{
    public partial class EventList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static AjaxData GetEvents(EventFilter filter)
        {
            int totalCount = 0;
            try
            {
                EventBol bol = new EventBol();
                return new AjaxData(true, bol.GetEvents(filter, out totalCount), string.Empty, totalCount);
            }
            catch (Exception ex)
            {
                return new AjaxData(false, null, ex.Message);
            }
        }

        [WebMethod]
        public static AjaxData DeleteEvent(int id)
        {
            try
            {
                EventBol bol = new EventBol();
                bol.DeleteEvent(id);
                return new AjaxData(true, null, string.Empty);
            }
            catch (Exception ex)
            {
                return new AjaxData(false, null, ex.Message);
            }
        }
    }
}