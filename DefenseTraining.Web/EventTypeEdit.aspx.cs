using System;
using System.Web.Services;
using DefenseTraining.Bol;
using DefenseTraining.Model;

namespace DefenseTraining.Web
{
    public partial class EventTypeEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        public static AjaxData SaveEventType(EventType evtType)
        {
            try
            {
                EventTypeBol bol = new EventTypeBol();
                return new AjaxData(true, bol.SaveEventType(evtType), string.Empty);
            }
            catch (Exception ex)
            {
                return new AjaxData(false, null, ex.Message);
            }
        }

        [WebMethod]
        public static AjaxData GetEventType(int id)
        {
            try
            {
                EventTypeBol bol = new EventTypeBol();
                return new AjaxData(true, bol.GetEventType(id), string.Empty);
            }
            catch (Exception ex)
            {
                return new AjaxData(false, null, ex.Message);
            }
        }
    }
}
