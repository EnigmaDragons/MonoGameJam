using System;
using System.Collections.Generic;
using System.Linq;
using MonoDragons.Core.Common;
using MonoDragons.Core.Memory;

namespace MonoDragons.Core.EventSystem
{
    public static class Event
    {
        private static readonly List<EventSubscription> EventSubs = new List<EventSubscription>();
        private static readonly Events TransientEvents = new Events();
        private static readonly Events PersistentEvents = new Events();

        public static int SubscriptionCount => TransientEvents.SubscriptionCount + PersistentEvents.SubscriptionCount;
        
        public static void Publish(object payload)
        {
            Logger.WriteLine(payload.ToString());
            TransientEvents.Publish(payload);
            PersistentEvents.Publish(payload);
        }

        public static void SubscribeForever(EventSubscription subscription)
        {
            PersistentEvents.Subscribe(subscription);
        }

        public static void Subscribe<T>(Action<T> onEvent, object owner)
        {
            Subscribe(EventSubscription.Create<T>(onEvent, owner));
        }

        public static void Subscribe(EventSubscription subscription)
        {
            TransientEvents.Subscribe(subscription);
            EventSubs.Add(subscription);
            Resources.Put(subscription.GetHashCode().ToString(), subscription);
        }

        public static void Unsubscribe(object owner)
        {
            TransientEvents.Unsubscribe(owner);
            PersistentEvents.Unsubscribe(owner);
            EventSubs.Where(x => x.Owner.Equals(owner)).ForEach(x =>
            {
                Resources.NotifyDisposed(x);
                EventSubs.Remove(x);
            });
        }
    }
}
