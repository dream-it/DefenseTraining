using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DefenseTraining.Bol;
using DefenseTraining.Model;
using CrystalDecisions.CrystalReports.Engine;

namespace DefenseTraining.Web
{
    public partial class ReminderViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EventBol bol = new EventBol();
            rptViewer.RefreshReport();
            ReportDocument doc = new ReportDocument();
            string rptType = Request.QueryString["reportType"];            
            if(string.IsNullOrEmpty(rptType))
            {
                List<EventReminder> reminders = bol.GetReminders();
            
                doc.Load(Server.MapPath("~/Reports/EventReminderReport.rpt"));
                doc.SetDataSource(reminders);
            }
            else
            {
                string year = Request.QueryString["year"];
                List<JointStatement> alotments = new PortalBol().GetJointStatements(int.Parse(year));                
                doc.Load(Server.MapPath("~/Reports/JointStatementReport.rpt"));                
                doc.SetDataSource(alotments);
                doc.SetParameterValue("@Year", year);
            }
            rptViewer.ReportSource = doc;
            rptViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
            rptViewer.BestFitPage = true;
        }
    }
}