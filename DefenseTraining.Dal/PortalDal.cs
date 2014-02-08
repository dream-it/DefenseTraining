using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DefenseTraining.Model;
using System.Data;

namespace DefenseTraining.Dal
{
    public class PortalDal : DalBase
    {
        public List<JointStatement> GetJointStatements(int year)
        {
            List<JointStatement> statements = new List<JointStatement>();
            List<TempAllotment> tmpAllotments = new List<TempAllotment>();
            List<TempAllotment> tmpReAllotments = new List<TempAllotment>();
            OpenConnection();
            try
            {
                using (DataSet ds = GetDataSet("AlotmentRealotmentGetAll", new SqlParameter[] { new SqlParameter("@Year", year) }))
                {
                    DataTable aTable = ds.Tables[0];
                    foreach (DataRow row in aTable.Rows)
                    {
                        tmpAllotments.Add(MapTempAllotment(row));
                    }
                    DataTable raTable = ds.Tables[1];
                    foreach (DataRow row in raTable.Rows)
                    {
                        tmpReAllotments.Add(MapTempAllotment(row));
                    }
                }
                List<TempAllotment> allotments = MergAllotments(tmpAllotments, tmpReAllotments);
                List<TempAllotment> regretteds = GetRegretteds(tmpAllotments, tmpReAllotments);
                statements = MakeJointStatements(allotments, regretteds);
            }
            finally
            {
                CloseConnection();
            }
            return statements;
        }

        private List<JointStatement> MakeJointStatements(List<TempAllotment> allotments, List<TempAllotment> regretteds)
        {
            List<JointStatement> statements = new List<JointStatement>();
            string[] alotmetnTypes = Enum.GetNames(typeof(AlotmentType));
            for (int i = 0; i < alotmetnTypes.Length; i++)
            {
                JointStatement statement = new JointStatement();
                statement.Service = alotmetnTypes[i];
                statement.CompetitionAvailed = GetTotal(allotments, (AlotmentType)Enum.Parse(typeof(AlotmentType), alotmetnTypes[i]), EventCategory.Competition);
                statement.CompetitionRegretted = GetTotal(regretteds, (AlotmentType)Enum.Parse(typeof(AlotmentType), alotmetnTypes[i]), EventCategory.Competition);

                statement.CourseAvailed = GetTotal(allotments, (AlotmentType)Enum.Parse(typeof(AlotmentType), alotmetnTypes[i]), EventCategory.Course);
                statement.CourseRegretted = GetTotal(regretteds, (AlotmentType)Enum.Parse(typeof(AlotmentType), alotmetnTypes[i]), EventCategory.Course);

                statement.ExcerciseAvailed = GetTotal(allotments, (AlotmentType)Enum.Parse(typeof(AlotmentType), alotmetnTypes[i]), EventCategory.Excercise);
                statement.ExcerciseRegretted = GetTotal(regretteds, (AlotmentType)Enum.Parse(typeof(AlotmentType), alotmetnTypes[i]), EventCategory.Excercise);

                statement.SeminarAvailed = GetTotal(allotments, (AlotmentType)Enum.Parse(typeof(AlotmentType), alotmetnTypes[i]), EventCategory.Seminar);
                statement.SeminarRegretted = GetTotal(regretteds, (AlotmentType)Enum.Parse(typeof(AlotmentType), alotmetnTypes[i]), EventCategory.Seminar);

                statement.SmeeAvailed = GetTotal(allotments, (AlotmentType)Enum.Parse(typeof(AlotmentType), alotmetnTypes[i]), EventCategory.SMEE);
                statement.SmeeRegretted = GetTotal(regretteds, (AlotmentType)Enum.Parse(typeof(AlotmentType), alotmetnTypes[i]), EventCategory.SMEE);

                statement.VisitAvailed = GetTotal(allotments, (AlotmentType)Enum.Parse(typeof(AlotmentType), alotmetnTypes[i]), EventCategory.Visit);
                statement.VisitRegretted = GetTotal(regretteds, (AlotmentType)Enum.Parse(typeof(AlotmentType), alotmetnTypes[i]), EventCategory.Visit);

                statements.Add(statement);
            }
            return statements;
        }


        private int GetTotal(List<TempAllotment> allotments, AlotmentType alotType, EventCategory eventCat)
        {
            int sum = 0;
            foreach (TempAllotment allotment in allotments)
            {
                if (allotment.AllotmentType == alotType && allotment.EventCategory == eventCat)
                {
                    sum += allotment.Quota;
                }
            }
            return sum;
        }



        private List<NewAlotmentStatement> MakeAllotmentStatements(List<TempAllotment> allotments, List<TempAllotment> regretteds)
        {
            List<NewAlotmentStatement> statements = new List<NewAlotmentStatement>();
            List<EventType> eventTypes = new EventTypeDal().GetEventTypes();
            foreach (EventType eventType in eventTypes)
            {
                foreach (AlotmentType item in Enum.GetValues(typeof(AlotmentType)))
                {
                    List<TempAllotment> availeds = allotments.FindAll(x => x.AllotmentType == item && x.EventTypeName.CompareTo(eventType.Name) == 0);
                    List<TempAllotment> regretts = regretteds.FindAll(x => x.AllotmentType == item && x.EventTypeName.CompareTo(eventType.Name) == 0);
                    NewAlotmentStatement statement = new NewAlotmentStatement();
                    statement.AlotmentType = item.ToString();
                    statement.EventType = eventType.Name;
                    statement.Availed = GetSumOfQuota(availeds);
                    statement.Regretted = GetSumOfQuota(regretts);
                    statements.Add(statement);
                }
            }
            return statements;
        }

        private int GetSumOfQuota(List<TempAllotment> tmpAllotments)
        {
            if (tmpAllotments == null) return 0;
            int quotas = 0;
            foreach (TempAllotment item in tmpAllotments)
            {
                quotas += item.Quota;
            }
            return quotas;
        }

        private List<TempAllotment> GetRegretteds(List<TempAllotment> tmpAllotments, List<TempAllotment> tmpReAllotments)
        {
            List<TempAllotment> regretteds = new List<TempAllotment>();
            foreach (TempAllotment item in tmpAllotments)
            {
                List<TempAllotment> reallots = tmpReAllotments.FindAll(x => x.EventId == item.EventId);
                if (reallots != null && reallots.Count > 0)
                {
                    regretteds.Add(item);
                }
            }
            return regretteds;
        }

        private List<TempAllotment> MergAllotments(List<TempAllotment> tmpAllotments, List<TempAllotment> tmpReAllotments)
        {
            List<TempAllotment> merged = new List<TempAllotment>();
            List<int> eventIds = new List<int>();
            foreach (TempAllotment item in tmpAllotments)
            {
                if (!eventIds.Exists(x => x == item.EventId))
                {
                    eventIds.Add(item.EventId);
                }
            }
            foreach (int eventId in eventIds)
            {
                List<TempAllotment> inReAllotment = tmpReAllotments.FindAll(x => x.EventId == eventId);
                if (inReAllotment != null && inReAllotment.Count != 0)
                {
                    merged.AddRange(inReAllotment);
                }
                else
                {
                    merged.AddRange(tmpAllotments.FindAll(x => x.EventId == eventId));
                }
            }
            return merged;
        }

        private TempAllotment MapTempAllotment(DataRow row)
        {
            TempAllotment ta = new TempAllotment();
            ta.AllotmentType = (AlotmentType)Enum.Parse(typeof(AlotmentType), row["AlotmentType"].ToString());
            ta.EventCategory = (EventCategory)Enum.Parse(typeof(EventCategory), row["EventCategory"].ToString());
            ta.EventId = Convert.ToInt32(row["EventId"]);
            ta.EventTypeName = Convert.ToString(row["EventTypeName"]);
            ta.Quota = Convert.ToInt32(row["Quota"]);
            return ta;
        }
    }
}
