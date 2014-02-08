using System;
using System.Collections.Generic;
using CrystalDecisions.CrystalReports.Engine;
using DefenseTraining.Bol;
using DefenseTraining.Model;

namespace DefenseTraining.Web
{
    public partial class TrainingReportViewer : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                int totalCount = 0;
                string id = Request.QueryString["id"];
                if (string.IsNullOrEmpty(id))
                {
                    EventFilter filter = new EventFilter();
                    filter.CountryId = int.Parse(Request.QueryString["cid"]);
                    filter.DateFrom = Request.QueryString["df"];
                    filter.DateTo = Request.QueryString["dt"];
                    filter.EventTypeId = int.Parse(Request.QueryString["etid"]);
                    filter.GenreId = int.Parse(Request.QueryString["gid"]);
                    filter.Id = int.Parse(Request.QueryString["eid"]);
                    filter.Name = Request.QueryString["name"];
                    filter.PersonalNo = Request.QueryString["pn"];
                    filter.RankId = int.Parse(Request.QueryString["rid"]);
                    filter.SpecialityId = int.Parse(Request.QueryString["sid"]);
                    EventBol bol = new EventBol();
                    List<Event> events = bol.GetEvents(filter, out totalCount);
                    rptViewer.RefreshReport();
                    ReportDocument doc = new ReportDocument();
                    doc.Load(Server.MapPath("~/Reports/EventListReportNew.rpt"));
                    doc.SetDataSource(ToEventReportsForList(events));
                    rptViewer.ReportSource = doc;
                    rptViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
                    rptViewer.BestFitPage = true;
                }
                else
                {
                    EventBol bol = new EventBol();
                    List<EventReport> reports = new List<EventReport>();
                    Event data = bol.GetEvent(int.Parse(id));
                    reports.Add(data.ToEventReport());
                    rptViewer.RefreshReport();
                    ReportDocument doc = new ReportDocument();
                    if (string.Compare(Request.QueryString["type"], "GO", true) == 0)
                    {
                        doc.Load(Server.MapPath("~/Reports/GovtOrderReport.rpt"));
                    }
                    else
                    {
                        doc.Load(Server.MapPath("~/Reports/EventReport.rpt"));
                    }
                    doc.SetDataSource(reports);
                    rptViewer.ReportSource = doc;
                    rptViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
                    rptViewer.BestFitPage = true;
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private List<EventReport> ToEventReports(List<Event> events)
        {
            List<EventReport> reports = new List<EventReport>();
            foreach (var item in events)
            {
                reports.Add(item.ToEventReport());
            }
            return reports;
        }

        private List<EventReportForList> ToEventReportsForList(List<Event> events)
        {
            List<EventReportForList> reports = new List<EventReportForList>();
            int slNo = 1;
            foreach (var item in events)
            {
                reports.AddRange(item.ToEventReportForList(ref slNo));
            }
            return reports;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}