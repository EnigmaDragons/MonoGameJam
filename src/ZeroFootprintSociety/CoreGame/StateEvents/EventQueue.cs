using System;
using System.Collections.Generic;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace ZeroFootPrintSociety.CoreGame.StateEvents
{
    public class EventQueue : IAutomaton
    {
        public static EventQueue Instance = new EventQueue();

        private List<object> _eventsToPublish = new List<object>();

        public void Add(object evt)
        {
            _eventsToPublish.Add(evt);
        }

        public void Update(TimeSpan delta)
        {
            var events = _eventsToPublish;
            _eventsToPublish = new List<object>();
            events.ForEach(Event.Publish);
        }
    }
}
