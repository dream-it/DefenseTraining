using System;
using System.Web.Services;
using DefenseTraining.Bol;

namespace DefenseTraining.Web
{
    public partial class EventTypeList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        public static AjaxData GetEventTypes()
        {
            try
            {
                EventTypeBol bol = new EventTypeBol();
                return new AjaxData(true, bol.GetEventTypes(), string.Empty);
            }
            catch (Exception ex)
            {
                return new AjaxData(false, null, ex.Message);
            }
        }

        [WebMethod]
        public static AjaxData DeleteEventType(int id)
        {
            try
            {
                EventTypeBol bol = new EventTypeBol();
                bol.DeleteEventType(id);
                return new AjaxData(true, null, string.Empty);
            }
            catch (Exception ex)
            {
                return new AjaxData(false, null, ex.Message);
            }
        }
    }
}
