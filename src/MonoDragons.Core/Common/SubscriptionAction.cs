using System;

namespace MonoDragons.Core.Common
{
    public sealed class SubscriptionAction<T> : ISubscription<T>
    {
        private readonly Action<T> _action;

        public SubscriptionAction(Action<T> action)
        {
            _action = action;
        }

        public void Update(T change)
        {
            _action(change);
        }

        public static implicit operator SubscriptionAction<T>(Action<T> action)
        {
            return new SubscriptionAction<T>(action);
        }
    }
}
