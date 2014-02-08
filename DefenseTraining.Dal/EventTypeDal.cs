using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DefenseTraining.Model;

namespace DefenseTraining.Dal
{
    public class EventTypeDal : DalBase
    {
        public List<EventType> GetEventTypes()
        {
            List<EventType> evtTypes = new List<EventType>();
            OpenConnection();
            try
            {
                using (SqlDataReader reader = ExecuteReader("EventTypeGetAll", new SqlParameter[] { }))
                {
                    while (reader.Read())
                    {
                        EventType evtType = new EventType();
                        evtType.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        evtType.Name = reader.GetString(reader.GetOrdinal("Name"));
                        evtType.Category = (EventCategory)Enum.Parse(typeof(EventCategory), reader.GetValue(reader.GetOrdinal("EventCategory")).ToString());
                        evtType.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                        evtTypes.Add(evtType);
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
            return evtTypes;
        }

        public EventType GetEventType(int id)
        {
            EventType evtType = new EventType();
            OpenConnection();
            try
            {
                using (SqlDataReader reader = ExecuteReader("EventTypeGet", new SqlParameter[] { new SqlParameter("@Id", id) }))
                {
                    if (reader.Read())
                    {
                        evtType.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        evtType.Name = reader.GetString(reader.GetOrdinal("Name"));
                        evtType.Category = (EventCategory)Enum.Parse(typeof(EventCategory), reader.GetValue(reader.GetOrdinal("EventCategory")).ToString());
                        evtType.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
            return evtType;
        }

        public void DeleteEventType(int id)
        {
            OpenConnection();
            try
            {
                ExecuteQuery("EventTypeDelete", new SqlParameter[] { new SqlParameter("@Id", id) });
            }
            finally
            {
                CloseConnection();
            }
        }

        public int SaveEventType(EventType eventType)
        {
            OpenConnection();
            try
            {
                SqlParameter[] parameters = { 
                                                new SqlParameter("@Id", eventType.Id) 
                                               ,new SqlParameter("@Name", eventType.Name)
                                               ,new SqlParameter("@EventCategory", eventType.Category)
                                            };
                parameters[0].Direction = System.Data.ParameterDirection.InputOutput;
                ExecuteQuery("EventTypeSave", parameters);
                return Convert.ToInt32(parameters[0].Value);
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool EventTypeExists(int id, string name)
        {
            OpenConnection();
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@Id", id), new SqlParameter("@Name", name) };
                return ExecuteScalar<bool>("EventTypeExists", parameters);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
