using System.Collections.Generic;
using System.Linq;

namespace MonoDragons.Core.Common
{
    public abstract class Subject<T> : ISubject<T>
    {
        protected readonly List<ISubscription<T>> Observers = new List<ISubscription<T>>();

        public virtual void Subscribe(ISubscription<T> subscription)
        {
            Observers.Add(subscription);
        }

        public virtual void UnsubscribeAll()
        {
            Observers.Clear();
        }

        public virtual void Unsubscribe(ISubscription<T> subscription)
        {
            Observers.Remove(subscription);
        }

        protected void NotifySubscribers(T obj)
        {
            var observers = Observers.Select(x => x).ToList();
            observers.ForEach(x => x.Update(obj));
        }
    }

    public abstract class Subject<T1, T2> : ISubject<T1, T2>
    {
        protected readonly List<ISubscription<T1>> Observers1 = new List<ISubscription<T1>>();
        protected readonly List<ISubscription<T2>> Observers2 = new List<ISubscription<T2>>();

        public virtual void Subscribe(ISubscription<T1> subscription)
        {
            Observers1.Add(subscription);
        }

        public virtual void Subscribe(ISubscription<T2> subscription)
        {
            Observers2.Add(subscription);
        }

        public virtual void UnsubscribeAll()
        {
            Observers1.Clear();
            Observers2.Clear();
        }

        public virtual void Unsubscribe(ISubscription<T1> subscription)
        {
            Observers1.Remove(subscription);
        }

        public virtual void Unsubscribe(ISubscription<T2> subscription)
        {
            Observers2.Remove(subscription);
        }

        protected void NotifySubscribers(T1 obj)
        {
            var observers = Observers1.Select(x => x).ToList();
            observers.ForEach(x => x.Update(obj));
        }

        protected void NotifySubscribers(T2 obj)
        {
            var observers = Observers2.Select(x => x).ToList();
            observers.ForEach(x => x.Update(obj));
        }
    }
}
