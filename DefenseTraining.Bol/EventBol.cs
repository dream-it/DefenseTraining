using System.Collections.Generic;
using DefenseTraining.Dal;
using DefenseTraining.Model;
using System;

namespace DefenseTraining.Bol
{
    public class EventBol
    {
        private EventDal _Dal;

        public EventBol()
        {
            _Dal = new EventDal();
        }

        public List<Event> GetEvents(EventFilter filter, out int totalCount)
        {
            if (string.IsNullOrEmpty(filter.DateFrom))
            {
                filter.DateFrom = "01 Jan 1900";
            }
            if (string.IsNullOrEmpty(filter.DateTo))
            {
                filter.DateTo = "31 Dec 3000";
            }
            return _Dal.GetEvents(filter, out totalCount);
        }

        public void DeleteEvent(int id)
        {
            _Dal.DeleteEvent(id);
        }

        public Event SaveEvent(Event evnt)
        {
            if (_Dal.EventExists(evnt.Id, evnt.Name, evnt.StartsOn))
                throw new Exception("Event with the same name already exists in year training starts on. Please enter unique event name.");
            evnt.Id = _Dal.SaveEvent(evnt);
            return evnt;
        }

        public Event GetEvent(int id)
        {
            return _Dal.GetEvent(id);
        }

        public List<EventReminder> GetReminders()
        {
            return _Dal.GetReminders();
        }

        public void DismissReminder(int eventId, string remindFor)
        {
            _Dal.DismissReminder(eventId, remindFor);
        }

        public List<AlotmentStatement> GetAlotmentStatement()
        {
            return _Dal.GetAlotmentStatement();
        }
    }
}
