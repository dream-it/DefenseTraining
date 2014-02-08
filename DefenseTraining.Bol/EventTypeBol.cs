using System.Collections.Generic;
using DefenseTraining.Dal;
using DefenseTraining.Model;
using System;

namespace DefenseTraining.Bol
{
    public class EventTypeBol
    {
        private EventTypeDal _Dal;

        public EventTypeBol()
        {
            _Dal = new EventTypeDal();
        }

        public List<EventType> GetEventTypes()
        {
            return _Dal.GetEventTypes();
        }

        public EventType GetEventType(int id)
        {
            return _Dal.GetEventType(id);
        }

        public void DeleteEventType(int id)
        {
            _Dal.DeleteEventType(id);
        }

        public EventType SaveEventType(EventType eventType)
        {
            if (_Dal.EventTypeExists(eventType.Id, eventType.Name))
                throw new Exception("Event type with the same name already exists. Please enter unique event type name.");
            eventType.Id = _Dal.SaveEventType(eventType);
            return eventType;
        }
    }
}
